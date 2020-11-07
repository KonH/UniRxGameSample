using Game.Config;
using Game.ViewModel;
using NUnit.Framework;
using UnityEditor;

namespace Game.Tests.Unit {
	public sealed class GameViewModelTests {
		[Test]
		public void IsUnitBought() {
			var viewModel = CreateViewModel();

			viewModel.BuyUnit("wolf");

			Assert.AreEqual(1, viewModel.Units.Count);
		}

		[Test]
		public void IsUnitBoughtWithResources() {
			var viewModel = CreateViewModel();
			var config    = viewModel.GetUnitConfig("wolf");
			Assert.IsNotNull(config);
			var price   = config.Levels[0].Price;
			var initial = viewModel.Resources.Resources[price.Name].Amount.Value;

			viewModel.BuyUnit("wolf");

			var final = viewModel.Resources.Resources[price.Name].Amount.Value;
			Assert.AreEqual((initial - final), price.Amount);
		}

		[Test]
		public void CantBoughtUnitIfHaveNoResources() {
			var viewModel = CreateViewModel();

			viewModel.BuyUnit("dragon");

			Assert.AreEqual(0, viewModel.Units.Count);
		}

		[Test]
		public void IsUnitUpgraded() {
			var viewModel = CreateViewModel();

			var unit = viewModel.BuyUnit("wolf");
			Assert.IsNotNull(unit);
			viewModel.Resources.Add(unit.UpgradePrice.Value.Model);
			viewModel.UpgradeUnit(unit);

			Assert.AreEqual(1, unit.Level.Value);
		}

		[Test]
		public void IsUnitUpgradedWithResources() {
			var viewModel = CreateViewModel();
			var unit = viewModel.BuyUnit("wolf");
			Assert.IsNotNull(unit);

			var price = unit.UpgradePrice.Value.Model;
			viewModel.Resources.Add(price);
			var initial = viewModel.Resources.Resources[price.Name].Amount.Value;

			viewModel.UpgradeUnit(unit);

			var final = viewModel.Resources.Resources[price.Name].Amount.Value;
			Assert.AreEqual((initial - final), price.Amount);
		}

		[Test]
		public void CantUpgradeUnitIfHaveNoResources() {
			var viewModel = CreateViewModel();

			var unit = viewModel.BuyUnit("wolf");
			Assert.IsNotNull(unit);
			viewModel.UpgradeUnit(unit);

			Assert.AreEqual(0, unit.Level.Value);
		}

		[Test]
		public void CantUpgradeUnitOnMaxLevel() {
			var viewModel = CreateViewModel();

			var unit   = viewModel.BuyUnit("wolf");
			Assert.IsNotNull(unit);
			var config = viewModel.GetUnitConfig(unit.Type);
			Assert.IsNotNull(config);
			for ( var i = 0; i < 2; i++ ) {
				viewModel.Resources.Add(config.Levels[i + 1].Price);
				viewModel.UpgradeUnit(unit);
			}
			viewModel.UpgradeUnit(unit);

			Assert.AreEqual(2, unit.Level.Value);
		}

		GameViewModel CreateViewModel() {
			var config = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/GameConfig.asset");
			return GameViewModel.Create(config);
		}
	}
}
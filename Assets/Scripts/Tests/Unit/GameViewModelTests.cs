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
		public void CantBoughtUnitIfHaveNoResources() {
			var viewModel = CreateViewModel();

			viewModel.BuyUnit("dragon");

			Assert.AreEqual(0, viewModel.Units.Count);
		}

		GameViewModel CreateViewModel() {
			var config = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/GameConfig.asset");
			return GameViewModel.Create(config);
		}
	}
}
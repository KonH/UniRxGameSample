using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Integration {
	public sealed class UpgradeTests : IntegrationTest {
		[UnityTest]
		public IEnumerator IsUpgradeButtonIsAvailable() {
			Prepare();
			yield return null;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			var unitView = GetUnitView("wolf", 0);
			GameView.ViewModel.Resources.Add(unitView.ViewModel.UpgradePrice.Value.Model);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			var upgradeView   = GetUpgradeView();
			var upgradeButton = upgradeView.GetComponentInChildren<Button>();

			Assert.IsTrue(upgradeButton.interactable);
		}

		[UnityTest]
		public IEnumerator IsUnitUpgraded() {
			Prepare();
			yield return null;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			var unitView   = GetUnitView("wolf", 0);
			GameView.ViewModel.Resources.Add(unitView.ViewModel.UpgradePrice.Value.Model);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			var upgradeView   = GetUpgradeView();
			var upgradeButton = upgradeView.GetComponentInChildren<Button>();
			upgradeButton.onClick.Invoke();

			Assert.AreEqual(1, unitView.ViewModel.Level.Value);
		}

		[UnityTest]
		public IEnumerator IsUpgradeButtonIsNotAvailableIfHaveNoResources() {
			Prepare();
			yield return null;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			var unitView   = GetUnitView("wolf", 0);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			var upgradeView   = GetUpgradeView();
			var upgradeButton = upgradeView.GetComponentInChildren<Button>();

			Assert.IsFalse(upgradeButton.interactable);
		}

		[UnityTest]
		public IEnumerator CantUpgradeUnitIfHaveNoResources() {
			Prepare();
			yield return null;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			var unitView   = GetUnitView("wolf", 0);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			var upgradeView   = GetUpgradeView();
			var upgradeButton = upgradeView.GetComponentInChildren<Button>();
			upgradeButton.onClick.Invoke();

			Assert.AreEqual(0, unitView.ViewModel.Level.Value);
		}

		[UnityTest]
		public IEnumerator IsUpgradeButtonIsNotAvailableOnMaxLevel() {
			Prepare();
			yield return null;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			var unitView   = GetUnitView("wolf", 0);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			var upgradeView   = GetUpgradeView();
			var upgradeButton = upgradeView.GetComponentInChildren<Button>();
			for ( var i = 0; i < 2; i++ ) {
				GameView.ViewModel.Resources.Add(unitView.ViewModel.UpgradePrice.Value.Model);
				upgradeButton.onClick.Invoke();
			}

			Assert.IsFalse(upgradeButton.interactable);
		}

		[UnityTest]
		public IEnumerator CantUpgradeUnitOnMaxLevel() {
			Prepare();
			yield return null;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			var unitView   = GetUnitView("wolf", 0);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			var upgradeView   = GetUpgradeView();
			var upgradeButton = upgradeView.GetComponentInChildren<Button>();
			for ( var i = 0; i < 2; i++ ) {
				GameView.ViewModel.Resources.Add(unitView.ViewModel.UpgradePrice.Value.Model);
				upgradeButton.onClick.Invoke();
			}
			upgradeButton.onClick.Invoke();

			Assert.AreEqual(2, unitView.ViewModel.Level.Value);
		}
	}
}
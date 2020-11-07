using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Integration {
	public sealed class BuyTests : IntegrationTest {
		[UnityTest]
		public IEnumerator IsBuyButtonIsAvailable() {
			Prepare();
			yield return null;

			var button = GetBuyView("wolf").GetComponentInChildren<Button>();

			Assert.IsTrue(button.interactable);
		}

		[UnityTest]
		public IEnumerator IsUnitBought() {
			Prepare();
			yield return null;

			var button = GetBuyView("wolf").GetComponentInChildren<Button>();

			button.onClick.Invoke();

			Assert.AreEqual(1, GameView.ViewModel.Units.Count);
		}

		[UnityTest]
		public IEnumerator IsUnitBoughtWithResources() {
			Prepare();
			yield return null;

			var button = GetBuyView("wolf").GetComponentInChildren<Button>();

			button.onClick.Invoke();
			yield return new WaitForSeconds(3);

			Assert.AreEqual("100", GetCoinCounter());
		}

		[UnityTest]
		public IEnumerator IsBuyButtonIsNotAvailableIfHaveNoResources() {
			Prepare();
			yield return null;

			GameView.ViewModel.Resources.TakeAll();
			yield return null;

			var button = GetBuyView("wolf").GetComponentInChildren<Button>();

			Assert.IsFalse(button.interactable);
		}

		[UnityTest]
		public IEnumerator CantBoughtUnitIfHaveNoResources() {
			Prepare();
			yield return null;

			GameView.ViewModel.Resources.TakeAll();
			yield return null;

			var button = GetBuyView("wolf").GetComponentInChildren<Button>();

			button.onClick.Invoke();

			Assert.AreEqual(0, GameView.ViewModel.Units.Count);
		}
	}
}
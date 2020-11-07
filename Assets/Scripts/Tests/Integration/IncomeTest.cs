using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Integration {
	public sealed class IncomeTest : IntegrationTest {
		[UnityTest]
		public IEnumerator IsIncomeProduced() {
			Prepare();
			yield return null;

			var config = GameView.ViewModel.GetUnitConfig("wolf");
			Assert.IsNotNull(config);
			var time = config.Levels[0].IncomeTime;

			var button = GetBuyView("wolf").GetComponentInChildren<Button>();
			button.onClick.Invoke();

			yield return new WaitForSeconds(time + 1);

			var unitView   = GetUnitView("wolf", 0);
			var incomeView = unitView.transform.GetChild(1);

			Assert.IsTrue(incomeView.gameObject.activeInHierarchy);
		}

		[UnityTest]
		public IEnumerator IsIncomeNotProducedBeforeRequiredTime() {
			Prepare();
			yield return null;

			var config = GameView.ViewModel.GetUnitConfig("wolf");
			Assert.IsNotNull(config);
			var time = config.Levels[0].IncomeTime;

			var button = GetBuyView("wolf").GetComponentInChildren<Button>();
			button.onClick.Invoke();

			yield return new WaitForSeconds(time - 1);

			var unitView   = GetUnitView("wolf", 0);
			var incomeView = unitView.transform.GetChild(1);

			Assert.IsFalse(incomeView.gameObject.activeInHierarchy);
		}

		[UnityTest]
		public IEnumerator IsIncomeCollected() {
			Prepare();
			yield return null;

			var config = GameView.ViewModel.GetUnitConfig("wolf");
			Assert.IsNotNull(config);
			var time   = config.Levels[0].IncomeTime;
			var income = config.Levels[0].Income;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			yield return new WaitForSeconds(time);

			var unitView   = GetUnitView("wolf", 0);
			var incomeView = unitView.transform.GetChild(1);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			Assert.IsFalse(incomeView.gameObject.activeInHierarchy);
			Assert.AreEqual(income.Amount, GameView.ViewModel.Resources.Resources[income.Name].Amount.Value);
		}
	}
}
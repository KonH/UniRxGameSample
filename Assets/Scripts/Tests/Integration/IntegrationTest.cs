using System.Linq;
using Game.View;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests.Integration {
	public abstract class IntegrationTest {
		protected GameView GameView => Object.FindObjectOfType<GameView>();

		protected void Prepare() {
			GameView.UseSerialization = false;
			SceneManager.LoadScene(0);
		}

		protected UnitBuyView GetBuyView(string type) =>
			Object.FindObjectsOfType<UnitBuyView>()
				.First(v => v.UnitType == type);

		protected UnitView GetUnitView(string type, int index) =>
			Object.FindObjectsOfType<UnitView>()
				.Where(v => v.ViewModel.Type == type)
				.Skip(index)
				.First();

		protected UnitInfoView GetUnitInfoView() => Object.FindObjectOfType<UnitInfoView>();

		protected UpgradeView GetUpgradeView() => Object.FindObjectOfType<UpgradeView>();

		protected string GetCoinCounter() => GameObject.Find("CoinResourceCounter").GetComponent<TMP_Text>().text;
		protected string GetStickCounter() => GameObject.Find("StickResourceCounter").GetComponent<TMP_Text>().text;
	}
}
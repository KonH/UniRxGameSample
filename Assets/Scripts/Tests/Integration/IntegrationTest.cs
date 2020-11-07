using System.Linq;
using Game.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tests.Integration {
	public abstract class IntegrationTest {
		protected GameView    GameView => Object.FindObjectOfType<GameView>();

		protected void Prepare() {
			GameView.UseSerialization = false;
			SceneManager.LoadScene(0);
		}

		protected UnitBuyView GetBuyView(string type) =>
			Object.FindObjectsOfType<UnitBuyView>()
				.First(v => v.UnitType == type);
	}
}
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Integration {
	public sealed class UiTests : IntegrationTest {
		[UnityTest]
		public IEnumerator IsInfoWindowIsOpened() {
			Prepare();
			yield return null;

			var buyButton = GetBuyView("wolf").GetComponentInChildren<Button>();
			buyButton.onClick.Invoke();

			var unitView   = GetUnitView("wolf", 0);
			var unitButton = unitView.GetComponentInChildren<Button>();
			unitButton.onClick.Invoke();

			var infoView = GetUnitInfoView();

			Assert.IsTrue(infoView.gameObject.activeInHierarchy);
		}
	}
}
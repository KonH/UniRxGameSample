using Game.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitBuyView : MonoBehaviour {
		readonly DisposableOwner _owner = new DisposableOwner();

		[SerializeField] Image    _priceImage;
		[SerializeField] TMP_Text _priceText;
		[SerializeField] Button   _button;

		GameViewModel _game;

		long _priceAmount;

		public string UnitType { get; private set; }

		public void Init(GameViewModel game, string unitType) {
			_game    = game;
			UnitType = unitType;
			var price     = _game.GetBuyPrice(unitType);
			var sprite    = _game.GetResourceIcon(price.Name);
			var resources = _game.Resources;
			_priceAmount = price.Amount;
			_owner.SetupDisposables();
			var requiredResource = resources.Resources[price.Name];
			requiredResource.Amount
				.Select(GetAvailability)
				.SubscribeToInteractable(_button)
				.AddTo(_owner.Disposables);
			_button.onClick
				.AsObservable()
				.Subscribe(_ => OnBuy())
				.AddTo(_owner.Disposables);
			_priceImage.sprite = sprite;
			_priceText.text    = _priceAmount.ToString();
		}

		bool GetAvailability(long currentAmount) => (currentAmount >= _priceAmount);

		void OnBuy() => _game.BuyUnit(UnitType);

		void OnDestroy() => _owner.Dispose();
	}
}
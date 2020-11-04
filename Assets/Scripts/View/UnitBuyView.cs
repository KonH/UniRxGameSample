using System;
using Game.Shared;
using Game.ViewModel;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitBuyView : MonoBehaviour {
		[SerializeField] Image    _priceImage;
		[SerializeField] TMP_Text _priceText;
		[SerializeField] Button   _button;

		CompositeDisposable _disposables;

		GameViewModel _game;
		string        _unitType;

		long _priceAmount;

		public void Init(GameViewModel game, string unitType) {
			_game     = game;
			_unitType = unitType;
			var price     = _game.GetBuyPrice(unitType);
			var sprite    = _game.GetResourceIcon(price.Name);
			var resources = _game.Resources;
			_priceAmount = price.Amount;
			_disposables?.Dispose();
			_disposables = new CompositeDisposable();
			var requiredResource = resources.Resources[price.Name];
			requiredResource
				.Subscribe(UpdateAvailability)
				.AddTo(_disposables);
			_button.onClick
				.AsObservable()
				.Subscribe(_ => OnBuy())
				.AddTo(_disposables);
			_priceImage.sprite = sprite;
			_priceText.text    = _priceAmount.ToString();
		}

		void UpdateAvailability(long currentAmount) {
			var isEnough = (currentAmount >= _priceAmount);
			_button.interactable = isEnough;
		}

		void OnBuy() {
			_game.BuyUnit(_unitType);
		}

		void OnDestroy() {
			_disposables.Dispose();
		}
	}
}
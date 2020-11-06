using Game.ViewModel;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UpgradeView : MonoBehaviour {
		[SerializeField] DynamicResourceView _view;
		[SerializeField] Button              _button;

		GameViewModel _game;
		UnitViewModel _unit;

		CompositeDisposable _disposables;

		public void Init(GameViewModel game, UnitViewModel viewModel) {
			_game = game;
			_unit = viewModel;
			_disposables?.Dispose();
			_disposables = new CompositeDisposable();
			viewModel.UpgradePrice
				.Subscribe(OnPriceChanged)
				.AddTo(_disposables);
			viewModel.CanUpgrade
				.SubscribeToInteractable(_button)
				.AddTo(_disposables);
			_button.onClick.AsObservable()
				.Subscribe(_ => OnClick())
				.AddTo(_disposables);
		}

		void OnPriceChanged(ResourceViewModel viewModel) {
			var hasPrice = !viewModel.IsEmpty;
			gameObject.SetActive(hasPrice);
			if ( hasPrice ) {
				_view.Init(_game, viewModel);
			}
		}

		void OnClick() => _game.UpgradeUnit(_unit);
	}
}
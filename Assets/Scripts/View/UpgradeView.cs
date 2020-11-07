using Game.ViewModel;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UpgradeView : MonoBehaviour {
		readonly DisposableOwner _owner = new DisposableOwner();

		[SerializeField] DynamicResourceView _view;
		[SerializeField] Button              _button;

		GameViewModel _game;
		UnitViewModel _unit;

		public void Init(GameViewModel game, UnitViewModel viewModel) {
			_game = game;
			_unit = viewModel;
			_owner.SetupDisposables();
			viewModel.UpgradePrice
				.Subscribe(OnPriceChanged)
				.AddTo(_owner.Disposables);
			viewModel.CanUpgrade
				.SubscribeToInteractable(_button)
				.AddTo(_owner.Disposables);
			_button.onClick.AsObservable()
				.Subscribe(_ => OnClick())
				.AddTo(_owner.Disposables);
		}

		void OnDestroy() => _owner.Dispose();

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
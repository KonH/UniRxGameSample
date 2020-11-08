using Game.ViewModel;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UpgradeView : MonoBehaviour {
		readonly DisposableOwner _owner = new DisposableOwner();

		[SerializeField] DynamicResourceView _view;
		[SerializeField] Button              _button;

		GameViewModel _game;
		UnitViewModel _unit;

		void OnValidate() {
			Assert.IsNotNull(_view, nameof(_view));
			Assert.IsNotNull(_button, nameof(_button));
		}

		public void Init([NotNull] GameViewModel game, [NotNull] UnitViewModel viewModel) {
			Assert.IsNotNull(game, nameof(game));
			Assert.IsNotNull(viewModel, nameof(viewModel));
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
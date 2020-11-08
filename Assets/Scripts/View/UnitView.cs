using Game.ViewModel;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitView : MonoBehaviour {
		[SerializeField] Image      _image;
		[SerializeField] Button     _button;
		[SerializeField] IncomeView _incomeView;

		GameViewModel _game;
		UnitInfoView  _infoView;

		public UnitViewModel ViewModel { get; private set; }

		void OnValidate() {
			Assert.IsNotNull(_image, nameof(_image));
			Assert.IsNotNull(_button, nameof(_button));
			Assert.IsNotNull(_incomeView, nameof(_incomeView));
		}

		public void Init(
			[NotNull] GameViewModel game, [NotNull] UnitInfoView infoView, [NotNull] UnitViewModel viewModel) {
			Assert.IsNotNull(game, nameof(game));
			Assert.IsNotNull(infoView, nameof(infoView));
			Assert.IsNotNull(viewModel, nameof(viewModel));

			_game     = game;
			_infoView = infoView;
			ViewModel = viewModel;
			viewModel.Sprite
				.Subscribe(s => _image.sprite = s);
			_button.onClick.AsObservable()
				.Subscribe(_ => OnClick());

			_incomeView.Init(game, viewModel.Income);
		}

		void OnClick() {
			if ( !ViewModel.Income.IsEmpty ) {
				_game.Collect(ViewModel);
				return;
			}
			_infoView.Init(_game, ViewModel);
		}
	}
}
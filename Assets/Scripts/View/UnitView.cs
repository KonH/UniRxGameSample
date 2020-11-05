using Game.ViewModel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitView : MonoBehaviour {
		[SerializeField] Image  _image;
		[SerializeField] Button _button;

		GameViewModel _game;
		UnitInfoView  _infoView;
		UnitViewModel _viewModel;

		public void Init(GameViewModel game, UnitInfoView infoView, UnitViewModel viewModel) {
			_game         = game;
			_infoView     = infoView;
			_viewModel    = viewModel;
			_image.sprite = viewModel.Sprite;
			_button.onClick.AsObservable()
				.Subscribe(_ => OnClick());
		}

		void OnClick() {
			if ( !_viewModel.Income.IsEmpty ) {
				_game.Collect(_viewModel);
				return;
			}
			_infoView.Init(_viewModel);
		}
	}
}
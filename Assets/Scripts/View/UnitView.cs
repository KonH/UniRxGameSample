using Game.ViewModel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitView : MonoBehaviour {
		[SerializeField] Image  _image;
		[SerializeField] Button _button;

		UnitInfoView  _infoView;
		UnitViewModel _viewModel;

		public void Init(UnitInfoView infoView, UnitViewModel viewModel) {
			_infoView     = infoView;
			_viewModel    = viewModel;
			_image.sprite = viewModel.Sprite;
			_button.onClick.AsObservable()
				.Subscribe(_ => OnClick());
		}

		void OnClick() {
			_infoView.Init(_viewModel);
		}
	}
}
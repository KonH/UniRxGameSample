using Game.ViewModel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
	public sealed class UnitView : MonoBehaviour {
		[SerializeField] Image      _image;
		[SerializeField] Button     _button;
		[SerializeField] IncomeView _incomeView;

		GameViewModel _game;
		UnitInfoView  _infoView;

		public UnitViewModel ViewModel { get; private set; }

		public void Init(GameViewModel game, UnitInfoView infoView, UnitViewModel viewModel) {
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
using Game.Config;
using Game.ViewModel;
using UnityEngine;

namespace Game.View {
	public sealed class GameView : MonoBehaviour {
		public static bool UseSerialization = true;

		[SerializeField] GameConfig     _config;
		[SerializeField] ResourceView[] _resourceViews;
		[SerializeField] UnitsView      _unitsView;

		SerializableGameViewModel _serializable;
		GameViewModel             _viewModel;

		void Awake() => Init();

		void Init() {
			if ( UseSerialization ) {
				_serializable = new SerializableGameViewModel(_config);
				_viewModel    = _serializable.ViewModel;
			} else {
				_viewModel = GameViewModel.Create(_config);
			}
			var resources = _viewModel.Resources;
			foreach ( var view in _resourceViews ) {
				view.Init(resources);
			}
			_unitsView.Init(_viewModel);
		}

		void Update() => _serializable?.ViewModel.Update();

		void OnApplicationQuit() => _serializable?.Save();
	}
}
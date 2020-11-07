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

		public GameViewModel ViewModel { get; private set; }

		void Awake() => Init();

		void Init() {
			if ( UseSerialization ) {
				_serializable = new SerializableGameViewModel(_config);
				ViewModel     = _serializable.ViewModel;
			} else {
				ViewModel = GameViewModel.Create(_config);
			}
			var resources = ViewModel.Resources;
			foreach ( var view in _resourceViews ) {
				view.Init(resources);
			}
			_unitsView.Init(ViewModel);
		}

		void Update() => _serializable?.ViewModel.Update();

		void OnApplicationQuit() => _serializable?.Save();
	}
}
using Game.Config;
using Game.ViewModel;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.View {
	public sealed class GameView : MonoBehaviour {
		public static bool UseSerialization = true;

		[SerializeField] GameConfig     _config;
		[SerializeField] ResourceView[] _resourceViews;
		[SerializeField] UnitsView      _unitsView;

		SerializableGameViewModel _serializable;

		public GameViewModel ViewModel { get; private set; }

		void OnValidate() {
			Assert.IsNotNull(_config, nameof(_config));
			Assert.IsNotNull(_unitsView, nameof(_unitsView));
		}

		void Awake() => Init();

		void Init() {
			ViewModel = CreateViewModel();
			var resources = ViewModel.Resources;
			foreach ( var view in _resourceViews ) {
				view.Init(resources);
			}
			_unitsView.Init(ViewModel);
		}

		GameViewModel CreateViewModel() {
			if ( !UseSerialization ) {
				return GameViewModel.Create(_config);
			}
			_serializable = new SerializableGameViewModel(_config);
			return _serializable.ViewModel;
		}

		void Update() => ViewModel.Update();

		void OnApplicationQuit() => _serializable?.Save();
	}
}
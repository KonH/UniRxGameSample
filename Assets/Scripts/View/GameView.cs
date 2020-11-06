using Game.Config;
using Game.ViewModel;
using UnityEngine;

namespace Game.View {
	public sealed class GameView : MonoBehaviour {
		[SerializeField] GameConfig     _config;
		[SerializeField] ResourceView[] _resourceViews;
		[SerializeField] UnitsView      _unitsView;

		SerializableGameViewModel _serializable;

		void Awake() => Init();

		void Init() {
			_serializable = new SerializableGameViewModel(_config);
			var viewModel = _serializable.ViewModel;
			var resources = viewModel.Resources;
			foreach ( var view in _resourceViews ) {
				view.Init(resources);
			}
			_unitsView.Init(viewModel);
		}

		void Update() => _serializable.ViewModel.Update();
	}
}
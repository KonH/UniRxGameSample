using Game.Config;
using Game.ViewModel;
using UnityEngine;

namespace Game.View {
	public sealed class GameView : MonoBehaviour {
		[SerializeField] GameConfig     _config;
		[SerializeField] ResourceView[] _resourceViews;
		[SerializeField] UnitsView      _unitsView;

		SerializableGameViewModel _serializable;

		void Awake() {
			Init();
		}

		void Init() {
			_serializable = new SerializableGameViewModel(_config);
			var viewModel = _serializable.ViewModel;
			var resources = viewModel.Resources;
			foreach ( var view in _resourceViews ) {
				view.Init(resources);
			}
			var units = viewModel.Units;
			_unitsView.Init(viewModel, units);
		}

		[ContextMenu("AddResource")]
		public void AddResource() {
			var resources = _serializable.ViewModel.Resources.Resources;
			var x = 0;
			foreach ( var pair in resources ) {
				x++;
				pair.Value.Value += x;
			}
		}

		[ContextMenu("AddUnit")]
		public void AddUnit() {
			var type = GetNextUnitType();
			_serializable.ViewModel.AddUnit(type);
		}

		string GetNextUnitType() {
			switch ( _serializable.ViewModel.Units.Count % 4 ) {
				case 0: return "wolf";
				case 1: return "cocatrice";
				case 2: return "ghost";
				case 3: return "dragon";
			}
			return string.Empty;
		}
	}
}
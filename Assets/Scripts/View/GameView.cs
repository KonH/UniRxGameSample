using Game.ViewModel;
using UnityEngine;

namespace Game.View {
	public sealed class GameView : MonoBehaviour {
		[SerializeField] ResourceView[] _resourceViews;
		[SerializeField] UnitsView      _unitsView;

		SavableGameViewModel _viewModel;

		void Awake() {
			Init();
		}

		void Init() {
			_viewModel = new SavableGameViewModel();
			var resources = _viewModel.ViewModel.Resources;
			foreach ( var view in _resourceViews ) {
				view.Init(resources);
			}
			var units = _viewModel.ViewModel.Units;
			_unitsView.Init(units);
		}

		[ContextMenu("AddResource")]
		public void AddResource() {
			var resources = _viewModel.ViewModel.Resources.Resources;
			var x = 0;
			foreach ( var pair in resources ) {
				x++;
				pair.Value.Value += x;
			}
		}

		[ContextMenu("AddUnit")]
		public void AddUnit() {
			var type = GetNextUnitType();
			_viewModel.ViewModel.AddUnit(type);
		}

		string GetNextUnitType() {
			switch ( _viewModel.ViewModel.Units.Count % 4 ) {
				case 0: return "wolf";
				case 1: return "cocatrice";
				case 2: return "ghost";
				case 3: return "dragon";
			}
			return string.Empty;
		}
	}
}
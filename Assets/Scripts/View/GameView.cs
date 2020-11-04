using Game.ViewModel;
using UnityEngine;

namespace Game.View {
	public sealed class GameView : MonoBehaviour {
		[SerializeField] ResourceView[] _resourceViews = new ResourceView[0];

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
		}

		[ContextMenu("Add")]
		public void Add() {
			var resources = _viewModel.ViewModel.Resources.Resources;
			var x = 0;
			foreach ( var pair in resources ) {
				x++;
				pair.Value.Value += x;
			}
		}
	}
}
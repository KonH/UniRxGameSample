using Game.ViewModel;
using UniRx;
using UnityEngine;

namespace Game.View {
	public sealed class UnitsView : MonoBehaviour {
		[SerializeField] UnitRowView[] _rows;

		public void Init(ReactiveCollection<UnitViewModel> viewModel) {
			foreach ( var row in _rows ) {
				row.Init(viewModel);
			}
		}
	}
}
using Game.ViewModel;
using UniRx;
using UnityEngine;

namespace Game.View {
	public sealed class UnitsView : MonoBehaviour {
		[SerializeField] UnitRowView[] _rows;
		[SerializeField] UnitBuyView   _placeholderPrefab;

		public void Init(GameViewModel game, ReactiveCollection<UnitViewModel> viewModel) {
			foreach ( var row in _rows ) {
				row.Init(_placeholderPrefab, game, viewModel);
			}
		}
	}
}
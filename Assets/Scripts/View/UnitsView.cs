using Game.ViewModel;
using UnityEngine;

namespace Game.View {
	public sealed class UnitsView : MonoBehaviour {
		[SerializeField] UnitRowView[] _rows;
		[SerializeField] UnitView      _unitPrefab;
		[SerializeField] UnitBuyView   _placeholderPrefab;

		public void Init(GameViewModel game) {
			foreach ( var row in _rows ) {
				row.Init(game, _unitPrefab, _placeholderPrefab);
			}
		}
	}
}
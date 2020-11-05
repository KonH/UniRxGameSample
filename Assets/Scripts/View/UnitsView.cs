using Game.ViewModel;
using UnityEngine;

namespace Game.View {
	public sealed class UnitsView : MonoBehaviour {
		[SerializeField] UnitRowView[] _rows;
		[SerializeField] UnitView      _unitPrefab;
		[SerializeField] UnitBuyView   _placeholderPrefab;
		[SerializeField] UnitInfoView  _infoView;

		public void Init(GameViewModel game) {
			foreach ( var row in _rows ) {
				row.Init(game, _unitPrefab, _placeholderPrefab, _infoView);
			}
		}
	}
}
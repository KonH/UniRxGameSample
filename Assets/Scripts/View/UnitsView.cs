using Game.ViewModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.View {
	public sealed class UnitsView : MonoBehaviour {
		[SerializeField] UnitRowView[] _rows;
		[SerializeField] UnitView      _unitPrefab;
		[SerializeField] UnitBuyView   _placeholderPrefab;
		[SerializeField] UnitInfoView  _infoView;

		void OnValidate() {
			Assert.IsNotNull(_unitPrefab, nameof(_unitPrefab));
			Assert.IsNotNull(_placeholderPrefab, nameof(_placeholderPrefab));
			Assert.IsNotNull(_infoView, nameof(_infoView));
		}

		public void Init([NotNull] GameViewModel game) {
			Assert.IsNotNull(game, nameof(game));
			foreach ( var row in _rows ) {
				row.Init(game, _unitPrefab, _placeholderPrefab, _infoView);
			}
		}
	}
}
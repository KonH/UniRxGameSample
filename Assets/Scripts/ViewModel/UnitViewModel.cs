using Game.Model;

namespace Game.ViewModel {
	public sealed class UnitViewModel {
		readonly UnitModel _model;

		public string Type => _model.Type;

		public UnitViewModel(UnitModel model) {
			_model = model;
		}
	}
}
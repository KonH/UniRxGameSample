using System.Linq;
using Game.Model;
using UniRx;

namespace Game.ViewModel {
	public sealed class GameViewModel {
		readonly GameModel _model;

		public readonly ResourcePackViewModel             Resources;
		public readonly ReactiveCollection<UnitViewModel> Units;

		public GameViewModel(GameModel model) {
			_model    = model;
			Resources = new ResourcePackViewModel(model.Resources);
			Units     = new ReactiveCollection<UnitViewModel>(model.Units.Select(m => new UnitViewModel(m)));
		}

		public void AddUnit(string type) {
			var model = new UnitModel {
				Type = type
			};
			_model.Units.Add(model);
			Units.Add(new UnitViewModel(model));
		}
	}
}
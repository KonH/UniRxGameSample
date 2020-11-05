using Game.Config;
using Game.Model;
using UnityEngine;

namespace Game.ViewModel {
	public sealed class UnitViewModel {
		readonly UnitModel _model;

		public string Type => _model.Type;

		public Sprite Sprite { get; }

		public UnitViewModel(UnitConfig config, UnitModel model) {
			_model = model;
			Sprite = config.Sprites[model.Level];
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Shared;

namespace Game.Model {
	[Serializable]
	public sealed class GameModel {
		public ResourcePack    Resources = new ResourcePack();
		public List<UnitModel> Units     = new List<UnitModel>();

		public GameModel() {}

		public GameModel(ResourcePack resources, IEnumerable<UnitModel> units) {
			Resources = resources;
			Units     = units.ToList();
		}
	}
}
using System;
using System.Collections.Generic;
using Game.Model;
using Game.Shared;

namespace Game.Model {
	[Serializable]
	public sealed class GameModel {
		public ResourcePack    Resources = new ResourcePack();
		public List<UnitModel> Units     = new List<UnitModel>();
	}
}
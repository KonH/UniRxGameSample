using System;
using System.Collections.Generic;
using System.Linq;
using Game.Shared;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Game.Model {
	[Serializable]
	public sealed class GameModel {
		public ResourcePack    Resources = new ResourcePack();
		public List<UnitModel> Units     = new List<UnitModel>();

		public GameModel() {}

		public GameModel([NotNull] ResourcePack resources, [NotNull] IEnumerable<UnitModel> units) {
			Assert.IsNotNull(resources);
			Resources = resources;
			Units     = units.ToList();
		}
	}
}
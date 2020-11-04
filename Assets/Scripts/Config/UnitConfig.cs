using System;
using System.Collections.Generic;
using Game.Shared;

namespace Game.Config {
	[Serializable]
	public sealed class UnitConfig {
		public string              Type;
		public List<ResourceModel> Prices;
	}
}
using System;
using System.Collections.Generic;

namespace Game.Config {
	[Serializable]
	public sealed class UnitConfig {
		public string          Type;
		public List<UnitLevel> Levels;
	}
}
using System;
using System.Collections.Generic;
using Game.Shared;
using UnityEngine;

namespace Game.Config {
	[Serializable]
	public sealed class UnitConfig {
		public string              Type;
		public Sprite[]            Sprites;
		public List<ResourceModel> Prices;
	}
}
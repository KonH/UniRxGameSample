using System.Collections.Generic;
using Game.Shared;
using UnityEngine;

namespace Game.Config {
	[CreateAssetMenu]
	public sealed class GameConfig : ScriptableObject {
		public ResourceModel    InitialResource;
		public List<UnitConfig> Units;
	}
}
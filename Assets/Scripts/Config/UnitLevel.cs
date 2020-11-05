using System;
using Game.Shared;
using UnityEngine;

namespace Game.Config {
	[Serializable]
	public sealed class UnitLevel {
		public Sprite        Sprite;
		public ResourceModel Price;
		public ResourceModel Income;
		public float         IncomeTime;
	}
}
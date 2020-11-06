using System;
using Game.Shared;

namespace Game.Model {
	[Serializable]
	public sealed class UnitModel {
		public string        Type;
		public int           Level;
		public long          LastIncomeTime;
		public ResourceModel Income;

		public UnitModel() {}

		public UnitModel(string type, int level, long lastIncomeTime, ResourceModel income) {
			Type           = type;
			Level          = level;
			LastIncomeTime = lastIncomeTime;
			Income         = income;
		}
	}
}
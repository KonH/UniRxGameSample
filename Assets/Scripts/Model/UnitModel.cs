using System;
using Game.Shared;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace Game.Model {
	[Serializable]
	public sealed class UnitModel {
		public string        Type;
		public int           Level;
		public long          LastIncomeTime;
		public ResourceModel Income;

		public UnitModel() {}

		public UnitModel(string type, int level, long lastIncomeTime, [NotNull] ResourceModel income) {
			Assert.IsNotNull(income, nameof(income));
			Type           = type;
			Level          = level;
			LastIncomeTime = lastIncomeTime;
			Income         = income;
		}
	}
}
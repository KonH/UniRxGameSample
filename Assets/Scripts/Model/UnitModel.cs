using System;

namespace Game.Model {
	[Serializable]
	public sealed class UnitModel {
		public string       Type;
		public int          Level;
		public long         LastIncomeTime;
		public ResourcePack Income;
	}
}
using System;

namespace Game.Service {
	public sealed class TimeProvider {
		public DateTimeOffset Now       { get; private set; }
		public DateTimeOffset FixedTime { get; set; }

		public void Update() {
			Now = (FixedTime == default) ? DateTimeOffset.UtcNow : FixedTime;
		}
	}
}
using System;
using UniRx;

namespace Game.View {
	public sealed class DisposableOwner : IDisposable {
		public CompositeDisposable Disposables { get; private set; }

		public void SetupDisposables() {
			Dispose();
			Disposables = new CompositeDisposable();
		}

		public void Dispose() => Disposables?.Dispose();
	}
}
using System;
using Game.Config;
using Game.Service;
using UniRx;

namespace Game.ViewModel {
	public sealed class SerializableGameViewModel {
		readonly GameSerializer _serializer;

		public readonly GameViewModel ViewModel;

		public SerializableGameViewModel(GameConfig config) {
			_serializer = new GameSerializer();
			var model = _serializer.TryLoad();
			ViewModel = GameViewModel.Create(config, model);
			Observable.Timer(TimeSpan.FromSeconds(30))
				.Subscribe(_ => Save());
		}

		public void Save() => _serializer.Save(ViewModel.Model);
	}
}
using Game.Config;
using Game.Service;

namespace Game.ViewModel {
	public sealed class SerializableGameViewModel {
		readonly GameSerializer _serializer;

		public readonly GameViewModel ViewModel;

		public SerializableGameViewModel(GameConfig config) {
			_serializer = new GameSerializer(config);
			var model = _serializer.LoadOrCreate();
			ViewModel = new GameViewModel(model);
		}
	}
}
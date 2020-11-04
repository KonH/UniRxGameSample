using Game.Service;

namespace Game.ViewModel {
	public sealed class SavableGameViewModel {
		readonly GameSerializer _serializer = new GameSerializer();

		public readonly GameViewModel ViewModel;

		public SavableGameViewModel() {
			var model = _serializer.LoadOrCreate();
			ViewModel = new GameViewModel(model);
		}
	}
}
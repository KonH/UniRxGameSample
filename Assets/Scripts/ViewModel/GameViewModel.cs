using Game.Model;

namespace Game.ViewModel {
	public sealed class GameViewModel {
		public readonly ResourcePackViewModel Resources;

		public GameViewModel(GameModel model) {
			Resources = new ResourcePackViewModel(model.Resources);
		}
	}
}
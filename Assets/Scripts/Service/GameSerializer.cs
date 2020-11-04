using System.Linq;
using Game.Model;
using Game.Shared;

namespace Game.Service {
	public sealed class GameSerializer {
		static readonly string[] ResourceNames = {
			"coin",
			"stick",
			"book",
			"bottle",
			"gold"
		};

		public GameModel LoadOrCreate() {
			// TODO: load
			return Create();
		}

		GameModel Create() {
			return new GameModel {
				Resources = new ResourcePack {
					Content = ResourceNames
						.Select(name => new ResourceModel {
							Name = name
						})
						.ToList()
				}
			};
		}

		public void Save(GameModel model) {
			// TODO: impl & usages
		}
	}
}
using System.Linq;
using Game.Config;
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

		readonly GameConfig _config;

		public GameSerializer(GameConfig config) {
			_config = config;
		}

		public GameModel LoadOrCreate() {
			// TODO: load
			return Create();
		}

		GameModel Create() {
			var initResource = _config.InitialResource;
			return new GameModel {
				Resources = new ResourcePack {
					Content = ResourceNames
						.Select(name => {
							var amount = (initResource.Name == name) ? initResource.Amount : 0;
							return new ResourceModel {
								Name   = name,
								Amount = amount
							};
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
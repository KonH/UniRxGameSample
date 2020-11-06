using System.IO;
using System.Linq;
using Game.Config;
using Game.Model;
using Game.Shared;
using UnityEngine;

namespace Game.Service {
	public sealed class GameSerializer {
		readonly GameConfig _config;

		public GameSerializer(GameConfig config) {
			_config = config;
		}

		public GameModel LoadOrCreate() {
			var path = GetPath();
			if ( File.Exists(path) ) {
				return Load(path);
			}
			return Create();
		}

		GameModel Load(string path) {
			var json = File.ReadAllText(path);
			return JsonUtility.FromJson<GameModel>(json);
		}

		GameModel Create() {
			var resourceNames = _config.Resources.Select(r => r.Name);
			var initResource  = _config.InitialResource;
			return new GameModel {
				Resources = new ResourcePack(
					resourceNames
						.Select(name => {
							var amount = (initResource.Name == name) ? initResource.Amount : 0;
							return new ResourceModel {
								Name   = name,
								Amount = amount
							};
						}))
			};
		}

		public void Save(GameModel model) {
			var path = GetPath();
			var json = JsonUtility.ToJson(model);
			File.WriteAllText(path, json);
		}

		string GetPath() => $"{Application.persistentDataPath}/save.json";
	}
}
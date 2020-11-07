using System.IO;
using Game.Config;
using Game.Model;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Service {
	public sealed class GameSerializer {
		[CanBeNull]
		public GameModel TryLoad() {
			var path = GetPath();
			if ( File.Exists(path) ) {
				return Load(path);
			}
			return null;
		}

		GameModel Load(string path) {
			var json = File.ReadAllText(path);
			return JsonUtility.FromJson<GameModel>(json);
		}

		public void Save(GameModel model) {
			var path = GetPath();
			var json = JsonUtility.ToJson(model);
			File.WriteAllText(path, json);
		}

		string GetPath() => $"{Application.persistentDataPath}/save.json";
	}
}
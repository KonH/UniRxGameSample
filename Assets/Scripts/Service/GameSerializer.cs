using System.IO;
using Game.Model;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Service {
	public sealed class GameSerializer {
		[CanBeNull]
		public GameModel TryLoad() {
			var path = GetPath();
			return File.Exists(path) ? Load(path) : null;
		}

		GameModel Load(string path) {
			var json = File.ReadAllText(path);
			return JsonUtility.FromJson<GameModel>(json);
		}

		public void Save([NotNull] GameModel model) {
			Assert.IsNotNull(model, nameof(model));
			var path = GetPath();
			var json = JsonUtility.ToJson(model);
			File.WriteAllText(path, json);
		}

		string GetPath() => $"{Application.persistentDataPath}/save.json";
	}
}
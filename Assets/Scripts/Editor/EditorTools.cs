using System.IO;
using UnityEditor;
using UnityEngine;

namespace Game.Editor {
	public static class EditorTools {
		[MenuItem("Game/DeleteSave")]
		public static void DeleteSave() {
			var path = $"{Application.persistentDataPath}/save.json";
			if ( File.Exists(path) ) {
				File.Delete(path);
			}
		}
	}
}
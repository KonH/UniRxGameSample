using System.IO;
using Game.Service;
using UnityEditor;

namespace Game.Editor {
	public static class EditorTools {
		[MenuItem("Game/DeleteSave")]
		public static void DeleteSave() {
			var path = GameSerializer.GetPath();
			if ( File.Exists(path) ) {
				File.Delete(path);
			}
		}
	}
}
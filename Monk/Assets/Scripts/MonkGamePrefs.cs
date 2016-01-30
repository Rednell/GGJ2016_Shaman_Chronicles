using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class MonkGamePrefs : ScriptableObject {

	private static MonkGamePrefs _current;
	public static MonkGamePrefs current {
		get {
			if (!_current) 
			{
				_current = Resources.Load<MonkGamePrefs>("MonkGamePrefs");
			}

			return _current;
		}
	}

	public float mazeFloorSpeed;
	public static float MazeFloorSpeed {
		get {
			return current.mazeFloorSpeed;
		}
	}


#if UNITY_EDITOR

	[UnityEditor.MenuItem("Edit/Monk Game Prefs")]
	public static void SelectAsset () {
		var path = "Assets/Resources/MonkGamePrefs.asset";
		var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<MonkGamePrefs>(path);
		if (!asset) {
			asset = UnityEditor.AssetDatabase.LoadAssetAtPath<MonkGamePrefs>(path);

			if (asset) { 
				UnityEditor.Selection.activeObject = asset;
				return;
			}

			var sounds = ScriptableObject.CreateInstance<MonkGamePrefs>();
			UnityEditor.AssetDatabase.CreateAsset (sounds, path);
			UnityEditor.AssetDatabase.Refresh();

			// load it again
			asset = UnityEditor.AssetDatabase.LoadAssetAtPath<MonkGamePrefs>(path);
		}
		UnityEditor.Selection.activeObject = asset;
	}

#endif
}

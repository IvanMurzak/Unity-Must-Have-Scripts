using UnityEngine;
using System.Collections;

public static class GameObjectExtantions {

	private static string StringPattern(GameObject component, string message)
	{
		return "[" + Application.loadedLevelName + "]:" + component.gameObject.name + ":" + message;
	}

	public static void LogError(this GameObject component, string message)
	{
		Debug.LogError (StringPattern(component, message));
	}

	public static void Log(this GameObject component, string message)
	{
		Debug.Log (StringPattern(component, message));
	}
}

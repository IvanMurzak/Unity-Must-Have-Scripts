using UnityEngine;

public static class GameObjectExtantions
{

    public static string SceneName()
    {
#if UNITY_5_3
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
#else
    return Application.loadedLevelName;
#endif
    }

    private static string StringPattern(GameObject component, string message)
    {
        return "[" + SceneName() + "]:" + component.gameObject.name + ":" + message;
    }

    public static void LogError(this GameObject component, string message)
    {
        Debug.LogError(StringPattern(component, message));
    }

    public static void Log(this GameObject component, string message)
    {
        Debug.Log(StringPattern(component, message));
    }
}
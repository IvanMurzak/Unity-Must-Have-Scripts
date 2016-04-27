using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string LOADING_SCENE_NAME = "AsyncLoadingScene";

    public UnityEngine.UI.Button.ButtonClickedEvent preLoading, onLoading;

    public float delay = 0;
    public bool showLoading = false;

    public void Load(Scenes scene)
    {
        if (preLoading != null) preLoading.Invoke();

        if (delay == 0)
            ExecuteLoading(scene, showLoading);
        else
            StartCoroutine(CreateRoutine(delay, scene, showLoading));
    }

    private IEnumerator CreateRoutine(float waitTime, Scenes scene, bool showLoading)
    {
        yield return new WaitForSeconds(waitTime);
        ExecuteLoading(scene, showLoading);
    }

    private void ExecuteLoading(Scenes scene, bool showLoading)
    {
        if (onLoading != null) onLoading.Invoke();

        if (showLoading)
        {
#if UNITY_5_3
            SceneManager.LoadScene(LOADING_SCENE_NAME);
#else
            Application.LoadLevel(LOADING_SCENE_NAME);
#endif
        }
        else
        {
            string sceneName = EnumExtantions.GetDescription<Scenes>(scene);
#if UNITY_5_3
            SceneManager.LoadScene(LOADING_SCENE_NAME);
#else
            Application.LoadLevel(sceneName);
#endif
        }
    }

    private void DestroyOldData()
    {

        SceneTransferData transferData = FindObjectOfType<SceneTransferData>();
        if (transferData != null) Destroy(transferData.gameObject);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnKey : MonoBehaviour 
{
    public KeyCode key;
    public Button.ButtonClickedEvent onClick;
    void Update()
    {
        if (Input.GetKeyDown(key) && onClick != null) onClick.Invoke();
    }
}
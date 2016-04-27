using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Killable : MonoBehaviour
{
    public Button.ButtonClickedEvent onKill;

    public void Kill()
    {
        if (onKill != null) onKill.Invoke();
    }

}

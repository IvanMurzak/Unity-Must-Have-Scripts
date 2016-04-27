using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game.Package
{
    public class Killable : MonoBehaviour
    {
        public Button.ButtonClickedEvent onKill;

        public void Kill()
        {
            if (onKill != null) onKill.Invoke();
        }

    }
}

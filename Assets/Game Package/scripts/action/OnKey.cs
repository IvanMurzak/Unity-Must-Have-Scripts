using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Game.Package
{
    public class OnKey : MonoBehaviour
    {
        public KeyCode key;
        public UnityEvent onClick;

        void Update()
        {
            if (Input.GetKeyDown(key) && onClick != null) onClick.Invoke();
        }
    }
}
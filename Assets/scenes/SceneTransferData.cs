using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneTransferData : MonoBehaviour
{
    public Dictionary<string, object> Data { get { return data; } }

    private Dictionary<string, object> data = new Dictionary<string, object>();

    public static class Key
    {
        public const string NEXT_SCENE = "NEXT_SCENE";
        public const string PREVIOUS_SCENE = "PREVIOUS_SCENE";
    }
}

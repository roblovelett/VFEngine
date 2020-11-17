using System;
using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools
{
    [Serializable]
    public class KeyCodeGameObjectListDictionary : UnitySerializedDictionary<KeyCode, List<GameObject>>
    {
    }
}
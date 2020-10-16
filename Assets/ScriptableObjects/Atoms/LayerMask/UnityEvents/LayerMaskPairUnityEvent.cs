using System;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using UnityEngine.Events;

namespace ScriptableObjects.Atoms.LayerMask.UnityEvents
{
    /// <summary>
    /// None generic Unity Event of type `LayerMaskPair`. Inherits from `UnityEvent&lt;LayerMaskPair&gt;`.
    /// </summary>
    [Serializable]
    public sealed class LayerMaskPairUnityEvent : UnityEvent<LayerMaskPair> { }
}

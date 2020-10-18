using System;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using UnityEngine.Events;

namespace ScriptableObjects.Atoms.RaycastHit2D.UnityEvents
{
    /// <summary>
    /// None generic Unity Event of type `RaycastHit2DPair`. Inherits from `UnityEvent&lt;RaycastHit2DPair&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RaycastHit2DPairUnityEvent : UnityEvent<RaycastHit2DPair> { }
}

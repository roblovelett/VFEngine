using System;
using UnityEngine.Events;

namespace ScriptableObjects.Atoms.RaycastHit2D.UnityEvents
{
    /// <summary>
    /// None generic Unity Event of type `RaycastHit2D`. Inherits from `UnityEvent&lt;RaycastHit2D&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RaycastHit2DUnityEvent : UnityEvent<UnityEngine.RaycastHit2D> { }
}

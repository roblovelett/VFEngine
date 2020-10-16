using System;
using UnityEngine.Events;

namespace ScriptableObjects.Atoms.LayerMask.UnityEvents
{
    /// <summary>
    ///     None generic Unity Event of type `LayerMask`. Inherits from `UnityEvent&lt;LayerMask&gt;`.
    /// </summary>
    [Serializable]
    public sealed class LayerMaskUnityEvent : UnityEvent<UnityEngine.LayerMask>
    {
    }
}
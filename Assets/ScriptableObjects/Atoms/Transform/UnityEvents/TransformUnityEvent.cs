using System;
using UnityEngine.Events;

namespace ScriptableObjects.Atoms.Transform.UnityEvents
{
    /// <summary>
    ///     None generic Unity Event of type `Transform`. Inherits from `UnityEvent&lt;Transform&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TransformUnityEvent : UnityEvent<UnityEngine.Transform>
    {
    }
}
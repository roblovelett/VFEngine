using System;
using ScriptableObjects.Atoms.Transform.Pairs;
using UnityEngine.Events;

namespace ScriptableObjects.Atoms.Transform.UnityEvents
{
    /// <summary>
    ///     None generic Unity Event of type `TransformPair`. Inherits from `UnityEvent&lt;TransformPair&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TransformPairUnityEvent : UnityEvent<TransformPair>
    {
    }
}
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Functions;
using ScriptableObjects.Atoms.Transform.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.Variables
{
    /// <summary>
    ///     Variable of type `Transform`. Inherits from `AtomVariable&lt;Transform, TransformPair, TransformEvent,
    ///     TransformPairEvent, TransformTransformFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Transform", fileName = "TransformVariable")]
    public sealed class TransformVariable : AtomVariable<UnityEngine.Transform, TransformPair, TransformEvent,
        TransformPairEvent, TransformTransformFunction>
    {
        protected override bool ValueEquals(UnityEngine.Transform other)
        {
            return _value == null && other == null ||
                   _value != null && other != null /* && _value.GetInstanceID() == other.GetInstanceID()*/;
        }
    }
}
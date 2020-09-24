using ScriptableObjects.Atoms.Transform.Events;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.ValueLists
{
    /// <summary>
    ///     Value List of type `Transform`. Inherits from `AtomValueList&lt;Transform, TransformEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Transform", fileName = "TransformValueList")]
    public sealed class TransformValueList : AtomValueList<UnityEngine.Transform, TransformEvent>
    {
    }
}
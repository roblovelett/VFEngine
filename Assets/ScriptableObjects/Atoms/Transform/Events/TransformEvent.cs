using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.Events
{
    /// <summary>
    /// Event of type `Transform`. Inherits from `AtomEvent&lt;Transform&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Transform", fileName = "TransformEvent")]
    public sealed class TransformEvent : AtomEvent<UnityEngine.Transform> { }
}

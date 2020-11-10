using ScriptableObjects.Atoms.Raycast.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Raycast.Events
{
    /// <summary>
    /// Event of type `RaycastPair`. Inherits from `AtomEvent&lt;RaycastPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/RaycastPair", fileName = "RaycastPairEvent")]
    public sealed class RaycastPairEvent : AtomEvent<RaycastPair> { }
}

using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.Events
{
    /// <summary>
    /// Event of type `RaycastHit2DPair`. Inherits from `AtomEvent&lt;RaycastHit2DPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/RaycastHit2DPair", fileName = "RaycastHit2DPairEvent")]
    public sealed class RaycastHit2DPairEvent : AtomEvent<RaycastHit2DPair> { }
}

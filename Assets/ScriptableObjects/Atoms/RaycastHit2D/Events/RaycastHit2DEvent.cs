using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.Events
{
    /// <summary>
    /// Event of type `RaycastHit2D`. Inherits from `AtomEvent&lt;RaycastHit2D&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/RaycastHit2D", fileName = "RaycastHit2DEvent")]
    public sealed class RaycastHit2DEvent : AtomEvent<UnityEngine.RaycastHit2D> { }
}

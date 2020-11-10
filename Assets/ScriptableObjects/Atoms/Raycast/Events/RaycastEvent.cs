using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Raycast.Events
{
    /// <summary>
    /// Event of type `Raycast`. Inherits from `AtomEvent&lt;Raycast&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Raycast", fileName = "RaycastEvent")]
    public sealed class RaycastEvent : AtomEvent<Raycast> { }
}

using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Mask.Events
{
    /// <summary>
    /// Event of type `Mask`. Inherits from `AtomEvent&lt;Mask&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Mask", fileName = "MaskEvent")]
    public sealed class MaskEvent : AtomEvent<Mask> { }
}

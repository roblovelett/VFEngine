using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.Events
{
    /// <summary>
    /// Event of type `LayerMask`. Inherits from `AtomEvent&lt;LayerMask&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/LayerMask", fileName = "LayerMaskEvent")]
    public sealed class LayerMaskEvent : AtomEvent<UnityEngine.LayerMask> { }
}

using ScriptableObjects.Atoms.LayerMask.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.Events
{
    /// <summary>
    /// Event of type `LayerMaskPair`. Inherits from `AtomEvent&lt;LayerMaskPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/LayerMaskPair", fileName = "LayerMaskPairEvent")]
    public sealed class LayerMaskPairEvent : AtomEvent<LayerMaskPair> { }
}

using ScriptableObjects.Atoms.Mask.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Mask.Events
{
    /// <summary>
    /// Event of type `MaskPair`. Inherits from `AtomEvent&lt;MaskPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/MaskPair", fileName = "MaskPairEvent")]
    public sealed class MaskPairEvent : AtomEvent<MaskPair> { }
}

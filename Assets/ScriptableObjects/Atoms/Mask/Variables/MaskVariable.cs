using ScriptableObjects.Atoms.Mask.Events;
using ScriptableObjects.Atoms.Mask.Functions;
using ScriptableObjects.Atoms.Mask.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Mask.Variables
{
    /// <summary>
    /// Variable of type `Mask`. Inherits from `EquatableAtomVariable&lt;Mask, MaskPair, MaskEvent, MaskPairEvent, MaskMaskFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Mask", fileName = "MaskVariable")]
    public sealed class MaskVariable : EquatableAtomVariable<Mask, MaskPair, MaskEvent, MaskPairEvent, MaskMaskFunction> { }
}

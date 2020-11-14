using ScriptableObjects.Atoms.Mask.Events;
using ScriptableObjects.Atoms.Mask.Functions;
using ScriptableObjects.Atoms.Mask.Pairs;
using ScriptableObjects.Atoms.Mask.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Mask.VariableInstancers
{
    /// <summary>
    /// Variable Instancer of type `Mask`. Inherits from `AtomVariableInstancer&lt;MaskVariable, MaskPair, Mask, MaskEvent, MaskPairEvent, MaskMaskFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Mask Variable Instancer")]
    public class MaskVariableInstancer : AtomVariableInstancer<
        MaskVariable,
        MaskPair,
        Mask,
        MaskEvent,
        MaskPairEvent,
        MaskMaskFunction>
    { }
}

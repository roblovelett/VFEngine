using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Functions;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.VariableInstancers
{
    /// <summary>
    /// Variable Instancer of type `LayerMask`. Inherits from `AtomVariableInstancer&lt;LayerMaskVariable, LayerMaskPair, LayerMask, LayerMaskEvent, LayerMaskPairEvent, LayerMaskLayerMaskFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/LayerMask Variable Instancer")]
    public class LayerMaskVariableInstancer : AtomVariableInstancer<
        LayerMaskVariable,
        LayerMaskPair,
        UnityEngine.LayerMask,
        LayerMaskEvent,
        LayerMaskPairEvent,
        LayerMaskLayerMaskFunction>
    { }
}

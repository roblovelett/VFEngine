using ScriptableObjects.Atoms.LayerMask.Constants;
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Functions;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.LayerMask.VariableInstancers;
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.Actions.SetVariableValue
{
    /// <summary>
    ///     Set variable value Action of type `LayerMask`. Inherits from `SetVariableValue&lt;LayerMask, LayerMaskPair,
    ///     LayerMaskVariable, LayerMaskConstant, LayerMaskReference, LayerMaskEvent, LayerMaskPairEvent,
    ///     LayerMaskVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/LayerMask",
        fileName = "SetLayerMaskVariableValue")]
    public sealed class SetLayerMaskVariableValue : SetVariableValue<UnityEngine.LayerMask, LayerMaskPair,
        LayerMaskVariable, LayerMaskConstant, LayerMaskReference, LayerMaskEvent, LayerMaskPairEvent,
        LayerMaskLayerMaskFunction, LayerMaskVariableInstancer>
    {
    }
}
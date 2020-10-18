using ScriptableObjects.Atoms.RaycastHit2D.Constants;
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Functions;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.RaycastHit2D.VariableInstancers;
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.Actions.SetVariableValue
{
    /// <summary>
    /// Set variable value Action of type `RaycastHit2D`. Inherits from `SetVariableValue&lt;RaycastHit2D, RaycastHit2DPair, RaycastHit2DVariable, RaycastHit2DConstant, RaycastHit2DReference, RaycastHit2DEvent, RaycastHit2DPairEvent, RaycastHit2DVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/RaycastHit2D", fileName = "SetRaycastHit2DVariableValue")]
    public sealed class SetRaycastHit2DVariableValue : SetVariableValue<
        UnityEngine.RaycastHit2D,
        RaycastHit2DPair,
        RaycastHit2DVariable,
        RaycastHit2DConstant,
        RaycastHit2DReference,
        RaycastHit2DEvent,
        RaycastHit2DPairEvent,
        RaycastHit2DRaycastHit2DFunction,
        RaycastHit2DVariableInstancer>
    { }
}

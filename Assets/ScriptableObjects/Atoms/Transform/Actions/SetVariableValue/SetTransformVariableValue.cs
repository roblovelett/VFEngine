using ScriptableObjects.Atoms.Transform.Constants;
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Functions;
using ScriptableObjects.Atoms.Transform.Pairs;
using ScriptableObjects.Atoms.Transform.References;
using ScriptableObjects.Atoms.Transform.VariableInstancers;
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.Actions.SetVariableValue
{
    /// <summary>
    ///     Set variable value Action of type `Transform`. Inherits from `SetVariableValue&lt;Transform, TransformPair,
    ///     TransformVariable, TransformConstant, TransformReference, TransformEvent, TransformPairEvent,
    ///     TransformVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Transform",
        fileName = "SetTransformVariableValue")]
    public sealed class SetTransformVariableValue : SetVariableValue<UnityEngine.Transform, TransformPair, TransformVariable,
        TransformConstant, TransformReference, TransformEvent, TransformPairEvent, TransformTransformFunction,
        TransformVariableInstancer>
    {
    }
}
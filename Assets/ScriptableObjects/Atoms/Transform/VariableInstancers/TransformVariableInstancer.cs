using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Functions;
using ScriptableObjects.Atoms.Transform.Pairs;
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.VariableInstancers
{
    /// <summary>
    ///     Variable Instancer of type `Transform`. Inherits from `AtomVariableInstancer&lt;TransformVariable, TransformPair,
    ///     Transform, TransformEvent, TransformPairEvent, TransformTransformFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Transform Variable Instancer")]
    public class TransformVariableInstancer : AtomVariableInstancer<TransformVariable, TransformPair, UnityEngine.Transform,
        TransformEvent, TransformPairEvent, TransformTransformFunction>
    {
    }
}
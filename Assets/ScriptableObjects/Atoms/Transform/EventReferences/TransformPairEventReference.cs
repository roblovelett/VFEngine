using System;
using ScriptableObjects.Atoms.Transform.EventInstancers;
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Pairs;
using ScriptableObjects.Atoms.Transform.VariableInstancers;
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.Transform.EventReferences
{
    /// <summary>
    ///     Event Reference of type `TransformPair`. Inherits from `AtomEventReference&lt;TransformPair, TransformVariable,
    ///     TransformPairEvent, TransformVariableInstancer, TransformPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TransformPairEventReference : AtomEventReference<TransformPair, TransformVariable,
        TransformPairEvent, TransformVariableInstancer, TransformPairEventInstancer>
    {
    }
}
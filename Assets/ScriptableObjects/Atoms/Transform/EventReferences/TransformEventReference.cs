using System;
using ScriptableObjects.Atoms.Transform.EventInstancers;
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.VariableInstancers;
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.Transform.EventReferences
{
    /// <summary>
    ///     Event Reference of type `Transform`. Inherits from `AtomEventReference&lt;Transform, TransformVariable,
    ///     TransformEvent, TransformVariableInstancer, TransformEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TransformEventReference : AtomEventReference<UnityEngine.Transform, TransformVariable, TransformEvent,
        TransformVariableInstancer, TransformEventInstancer>
    {
    }
}
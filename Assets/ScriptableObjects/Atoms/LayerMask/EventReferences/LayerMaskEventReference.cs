using System;
using ScriptableObjects.Atoms.LayerMask.EventInstancers;
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.VariableInstancers;
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.LayerMask.EventReferences
{
    /// <summary>
    ///     Event Reference of type `LayerMask`. Inherits from `AtomEventReference&lt;LayerMask, LayerMaskVariable,
    ///     LayerMaskEvent, LayerMaskVariableInstancer, LayerMaskEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class LayerMaskEventReference : AtomEventReference<UnityEngine.LayerMask, LayerMaskVariable,
        LayerMaskEvent, LayerMaskVariableInstancer, LayerMaskEventInstancer>, IGetEvent
    {
    }
}
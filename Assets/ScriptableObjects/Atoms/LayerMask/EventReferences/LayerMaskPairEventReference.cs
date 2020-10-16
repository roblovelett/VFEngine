using System;
using ScriptableObjects.Atoms.LayerMask.EventInstancers;
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using ScriptableObjects.Atoms.LayerMask.VariableInstancers;
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.LayerMask.EventReferences
{
    /// <summary>
    /// Event Reference of type `LayerMaskPair`. Inherits from `AtomEventReference&lt;LayerMaskPair, LayerMaskVariable, LayerMaskPairEvent, LayerMaskVariableInstancer, LayerMaskPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class LayerMaskPairEventReference : AtomEventReference<
        LayerMaskPair,
        LayerMaskVariable,
        LayerMaskPairEvent,
        LayerMaskVariableInstancer,
        LayerMaskPairEventInstancer>, IGetEvent 
    { }
}

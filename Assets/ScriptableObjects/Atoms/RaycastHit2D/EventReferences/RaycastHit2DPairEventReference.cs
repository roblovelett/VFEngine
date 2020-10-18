using System;
using ScriptableObjects.Atoms.RaycastHit2D.EventInstancers;
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using ScriptableObjects.Atoms.RaycastHit2D.VariableInstancers;
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.RaycastHit2D.EventReferences
{
    /// <summary>
    /// Event Reference of type `RaycastHit2DPair`. Inherits from `AtomEventReference&lt;RaycastHit2DPair, RaycastHit2DVariable, RaycastHit2DPairEvent, RaycastHit2DVariableInstancer, RaycastHit2DPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RaycastHit2DPairEventReference : AtomEventReference<
        RaycastHit2DPair,
        RaycastHit2DVariable,
        RaycastHit2DPairEvent,
        RaycastHit2DVariableInstancer,
        RaycastHit2DPairEventInstancer>, IGetEvent 
    { }
}

using System;
using ScriptableObjects.Atoms.RaycastHit2D.EventInstancers;
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.VariableInstancers;
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.RaycastHit2D.EventReferences
{
    /// <summary>
    /// Event Reference of type `RaycastHit2D`. Inherits from `AtomEventReference&lt;RaycastHit2D, RaycastHit2DVariable, RaycastHit2DEvent, RaycastHit2DVariableInstancer, RaycastHit2DEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RaycastHit2DEventReference : AtomEventReference<
        UnityEngine.RaycastHit2D,
        RaycastHit2DVariable,
        RaycastHit2DEvent,
        RaycastHit2DVariableInstancer,
        RaycastHit2DEventInstancer>, IGetEvent 
    { }
}

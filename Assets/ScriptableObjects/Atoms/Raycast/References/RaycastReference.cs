using System;
using ScriptableObjects.Atoms.Raycast.Constants;
using ScriptableObjects.Atoms.Raycast.Events;
using ScriptableObjects.Atoms.Raycast.Functions;
using ScriptableObjects.Atoms.Raycast.Pairs;
using ScriptableObjects.Atoms.Raycast.VariableInstancers;
using ScriptableObjects.Atoms.Raycast.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.Raycast.References
{
    /// <summary>
    /// Reference of type `Raycast`. Inherits from `EquatableAtomReference&lt;Raycast, RaycastPair, RaycastConstant, RaycastVariable, RaycastEvent, RaycastPairEvent, RaycastRaycastFunction, RaycastVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RaycastReference : EquatableAtomReference<
        Raycast,
        RaycastPair,
        RaycastConstant,
        RaycastVariable,
        RaycastEvent,
        RaycastPairEvent,
        RaycastRaycastFunction,
        RaycastVariableInstancer>, IEquatable<RaycastReference>
    {
        public RaycastReference() : base() { }
        public RaycastReference(Raycast value) : base(value) { }
        public bool Equals(RaycastReference other) { return base.Equals(other); }
    }
}

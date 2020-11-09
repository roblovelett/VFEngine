using System;
using ScriptableObjects.Atoms.RaycastHit2D.Constants;
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Functions;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using ScriptableObjects.Atoms.RaycastHit2D.VariableInstancers;
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.RaycastHit2D.References
{
    /// <summary>
    ///     Reference of type `RaycastHit2D`. Inherits from `AtomReference&lt;RaycastHit2D, RaycastHit2DPair,
    ///     RaycastHit2DConstant, RaycastHit2DVariable, RaycastHit2DEvent, RaycastHit2DPairEvent,
    ///     RaycastHit2DRaycastHit2DFunction, RaycastHit2DVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RaycastHit2DReference : AtomReference<
            UnityEngine.RaycastHit2D, RaycastHit2DPair, RaycastHit2DConstant, RaycastHit2DVariable, RaycastHit2DEvent,
            RaycastHit2DPairEvent, RaycastHit2DRaycastHit2DFunction, RaycastHit2DVariableInstancer>,
        IEquatable<RaycastHit2DReference>
    {
        public RaycastHit2DReference()
        {
        }

        public RaycastHit2DReference(UnityEngine.RaycastHit2D value) : base(value)
        {
        }

        public bool Equals(RaycastHit2DReference other)
        {
            return base.Equals(other);
        }

        protected override bool ValueEquals(UnityEngine.RaycastHit2D other)
        {
            return other;
        }
    }
}
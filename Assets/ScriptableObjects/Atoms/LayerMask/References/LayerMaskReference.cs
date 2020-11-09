using System;
using ScriptableObjects.Atoms.LayerMask.Constants;
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Functions;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using ScriptableObjects.Atoms.LayerMask.VariableInstancers;
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.LayerMask.References
{
    /// <summary>
    ///     Reference of type `RaycastHit2D`. Inherits from `AtomReference&lt;RaycastHit2D, RaycastHit2DPair,
    ///     RaycastHit2DConstant, RaycastHit2DVariable, RaycastHit2DEvent, RaycastHit2DPairEvent,
    ///     RaycastHit2DRaycastHit2DFunction, RaycastHit2DVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class LayerMaskReference : AtomReference<
            UnityEngine.LayerMask, LayerMaskPair, LayerMaskConstant, LayerMaskVariable, LayerMaskEvent,
            LayerMaskPairEvent, LayerMaskLayerMaskFunction, LayerMaskVariableInstancer>,
        IEquatable<LayerMaskReference>
    {
        public LayerMaskReference()
        {
        }

        public LayerMaskReference(UnityEngine.LayerMask value) : base(value)
        {
        }

        public bool Equals(LayerMaskReference other)
        {
            return base.Equals(other);
        }

        protected override bool ValueEquals(UnityEngine.LayerMask other)
        {
            return true;
        }
    }
}
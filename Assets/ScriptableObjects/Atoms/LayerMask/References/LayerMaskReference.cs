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
    ///     Reference of type `LayerMask`. Inherits from `EquatableAtomReference&lt;LayerMask, LayerMaskPair,
    ///     LayerMaskConstant, LayerMaskVariable, LayerMaskEvent, LayerMaskPairEvent, LayerMaskLayerMaskFunction,
    ///     LayerMaskVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class LayerMaskReference : EquatableAtomReference<
        UnityEngine.LayerMask, LayerMaskPair, LayerMaskConstant, LayerMaskVariable, LayerMaskEvent, LayerMaskPairEvent,
        LayerMaskLayerMaskFunction, LayerMaskVariableInstancer>, IEquatable<LayerMaskReference>
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
    }
}
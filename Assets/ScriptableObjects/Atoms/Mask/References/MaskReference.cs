using System;
using ScriptableObjects.Atoms.Mask.Constants;
using ScriptableObjects.Atoms.Mask.Events;
using ScriptableObjects.Atoms.Mask.Functions;
using ScriptableObjects.Atoms.Mask.Pairs;
using ScriptableObjects.Atoms.Mask.VariableInstancers;
using ScriptableObjects.Atoms.Mask.Variables;
using UnityAtoms;

// ReSharper disable RedundantBaseConstructorCall
namespace ScriptableObjects.Atoms.Mask.References
{
    /// <summary>
    ///     Reference of type `Mask`. Inherits from `EquatableAtomReference&lt;Mask, MaskPair, MaskConstant, MaskVariable,
    ///     MaskEvent, MaskPairEvent, MaskMaskFunction, MaskVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class MaskReference : EquatableAtomReference<
            Mask, MaskPair, MaskConstant, MaskVariable, MaskEvent, MaskPairEvent, MaskMaskFunction,
            MaskVariableInstancer>,
        IEquatable<MaskReference>
    {
        public MaskReference() : base()
        {
        }

        public MaskReference(Mask value) : base(value)
        {
        }

        public bool Equals(MaskReference other)
        {
            return base.Equals(other);
        }
    }
}
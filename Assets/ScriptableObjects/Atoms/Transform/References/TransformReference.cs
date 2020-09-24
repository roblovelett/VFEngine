using System;
using ScriptableObjects.Atoms.Transform.Constants;
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Functions;
using ScriptableObjects.Atoms.Transform.Pairs;
using ScriptableObjects.Atoms.Transform.VariableInstancers;
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;

namespace ScriptableObjects.Atoms.Transform.References
{
    /// <summary>
    ///     Reference of type `Transform`. Inherits from `AtomReference&lt;Transform, TransformPair, TransformConstant,
    ///     TransformVariable, TransformEvent, TransformPairEvent, TransformTransformFunction, TransformVariableInstancer,
    ///     AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class TransformReference : AtomReference<
        UnityEngine.Transform, TransformPair, TransformConstant, TransformVariable, TransformEvent, TransformPairEvent,
        TransformTransformFunction, TransformVariableInstancer>, IEquatable<TransformReference>
    {
        public TransformReference()
        {
        }

        public TransformReference(UnityEngine.Transform value) : base(value)
        {
        }

        public bool Equals(TransformReference other)
        {
            return base.Equals(other);
        }

        protected override bool ValueEquals(UnityEngine.Transform other)
        {
            throw new NotImplementedException();
        }
    }
}
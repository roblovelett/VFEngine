using System;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Mask.Pairs
{
    /// <summary>
    ///     IPair of type `&lt;Mask&gt;`. Inherits from `IPair&lt;Mask&gt;`.
    /// </summary>
    [Serializable]
    public struct MaskPair : IPair<Mask>
    {
        public Mask Item1
        {
            get => i1;
            set => i1 = value;
        }

        public Mask Item2
        {
            get => i2;
            set => i2 = value;
        }

        [SerializeField] private Mask i1;
        [SerializeField] private Mask i2;

        public void Deconstruct(out Mask item1, out Mask item2)
        {
            item1 = Item1;
            item2 = Item2;
        }
    }
}
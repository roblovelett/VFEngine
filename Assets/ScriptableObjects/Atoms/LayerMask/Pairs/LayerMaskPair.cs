using System;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.Pairs
{
    /// <summary>
    /// IPair of type `&lt;LayerMask&gt;`. Inherits from `IPair&lt;LayerMask&gt;`.
    /// </summary>
    [Serializable]
    public struct LayerMaskPair : IPair<UnityEngine.LayerMask>
    {
        public UnityEngine.LayerMask Item1 { get => _item1; set => _item1 = value; }
        public UnityEngine.LayerMask Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private UnityEngine.LayerMask _item1;
        [SerializeField]
        private UnityEngine.LayerMask _item2;

        public void Deconstruct(out UnityEngine.LayerMask item1, out UnityEngine.LayerMask item2) { item1 = Item1; item2 = Item2; }
    }
}
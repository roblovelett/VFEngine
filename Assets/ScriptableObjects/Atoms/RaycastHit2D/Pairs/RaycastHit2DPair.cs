using System;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.Pairs
{
    /// <summary>
    /// IPair of type `&lt;RaycastHit2D&gt;`. Inherits from `IPair&lt;RaycastHit2D&gt;`.
    /// </summary>
    [Serializable]
    public struct RaycastHit2DPair : IPair<UnityEngine.RaycastHit2D>
    {
        public UnityEngine.RaycastHit2D Item1 { get => _item1; set => _item1 = value; }
        public UnityEngine.RaycastHit2D Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private UnityEngine.RaycastHit2D _item1;
        [SerializeField]
        private UnityEngine.RaycastHit2D _item2;

        public void Deconstruct(out UnityEngine.RaycastHit2D item1, out UnityEngine.RaycastHit2D item2) { item1 = Item1; item2 = Item2; }
    }
}
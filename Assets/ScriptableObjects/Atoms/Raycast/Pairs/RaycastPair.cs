using System;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Raycast.Pairs
{
    /// <summary>
    /// IPair of type `&lt;Raycast&gt;`. Inherits from `IPair&lt;Raycast&gt;`.
    /// </summary>
    [Serializable]
    public struct RaycastPair : IPair<Raycast>
    {
        public Raycast Item1 { get => _item1; set => _item1 = value; }
        public Raycast Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Raycast _item1;
        [SerializeField]
        private Raycast _item2;

        public void Deconstruct(out Raycast item1, out Raycast item2) { item1 = Item1; item2 = Item2; }
    }
}
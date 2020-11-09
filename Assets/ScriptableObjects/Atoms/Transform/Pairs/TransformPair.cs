using System;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.Pairs
{
    /// <summary>
    /// IPair of type `&lt;Transform&gt;`. Inherits from `IPair&lt;Transform&gt;`.
    /// </summary>
    [Serializable]
    public struct TransformPair : IPair<UnityEngine.Transform>
    {
        public UnityEngine.Transform Item1 { get => _item1; set => _item1 = value; }
        public UnityEngine.Transform Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private UnityEngine.Transform _item1;
        [SerializeField]
        private UnityEngine.Transform _item2;

        public void Deconstruct(out UnityEngine.Transform item1, out UnityEngine.Transform item2) { item1 = Item1; item2 = Item2; }
    }
}
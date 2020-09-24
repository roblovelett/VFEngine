using System;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Atoms.Transform.Pairs
{
    /// <summary>
    ///     IPair of type `&lt;Transform&gt;`. Inherits from `IPair&lt;Transform&gt;`.
    /// </summary>
    [Serializable]
    public struct TransformPair : IPair<UnityEngine.Transform>
    {
        public UnityEngine.Transform Item1
        {
            get => item1;
            set => item1 = value;
        }

        public UnityEngine.Transform Item2
        {
            get => item2;
            set => item2 = value;
        }

        [FormerlySerializedAs("_item1")] [SerializeField] private UnityEngine.Transform item1;
        [FormerlySerializedAs("_item2")] [SerializeField] private UnityEngine.Transform item2;

        public void Deconstruct(out UnityEngine.Transform item01, out UnityEngine.Transform item02)
        {
            item01 = Item1;
            item02 = Item2;
        }
    }
}
using System;
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Functions;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.Variables
{
    /// <summary>
    /// Variable of type `RaycastHit2D`. Inherits from `AtomVariable&lt;RaycastHit2D, RaycastHit2DPair, RaycastHit2DEvent, RaycastHit2DPairEvent, RaycastHit2DRaycastHit2DFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/RaycastHit2D", fileName = "RaycastHit2DVariable")]
    public sealed class RaycastHit2DVariable : AtomVariable<UnityEngine.RaycastHit2D, RaycastHit2DPair, RaycastHit2DEvent, RaycastHit2DPairEvent, RaycastHit2DRaycastHit2DFunction>
    {
        protected override bool ValueEquals(UnityEngine.RaycastHit2D other)
        {
            return other;
        }
    }
}

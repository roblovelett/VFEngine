using System;
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Functions;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.Variables
{
    /// <summary>
    /// Variable of type `LayerMask`. Inherits from `AtomVariable&lt;LayerMask, LayerMaskPair, LayerMaskEvent, LayerMaskPairEvent, LayerMaskLayerMaskFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/LayerMask", fileName = "LayerMaskVariable")]
    public sealed class LayerMaskVariable : AtomVariable<UnityEngine.LayerMask, LayerMaskPair, LayerMaskEvent, LayerMaskPairEvent, LayerMaskLayerMaskFunction>
    {
        protected override bool ValueEquals(UnityEngine.LayerMask other)
        {
            return (_value == null && other == null) || _value != null && other != null/* && _value.GetInstanceID() == other.GetInstanceID()*/;
        }
    }
}

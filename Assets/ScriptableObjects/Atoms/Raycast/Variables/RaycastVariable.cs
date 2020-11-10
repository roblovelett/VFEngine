using ScriptableObjects.Atoms.Raycast.Events;
using ScriptableObjects.Atoms.Raycast.Functions;
using ScriptableObjects.Atoms.Raycast.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Raycast.Variables
{
    /// <summary>
    /// Variable of type `Raycast`. Inherits from `EquatableAtomVariable&lt;Raycast, RaycastPair, RaycastEvent, RaycastPairEvent, RaycastRaycastFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Raycast", fileName = "RaycastVariable")]
    public sealed class RaycastVariable : EquatableAtomVariable<Raycast, RaycastPair, RaycastEvent, RaycastPairEvent, RaycastRaycastFunction> { }
}

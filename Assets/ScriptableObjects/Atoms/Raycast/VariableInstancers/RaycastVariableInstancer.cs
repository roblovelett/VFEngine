using ScriptableObjects.Atoms.Raycast.Events;
using ScriptableObjects.Atoms.Raycast.Functions;
using ScriptableObjects.Atoms.Raycast.Pairs;
using ScriptableObjects.Atoms.Raycast.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Raycast.VariableInstancers
{
    /// <summary>
    /// Variable Instancer of type `Raycast`. Inherits from `AtomVariableInstancer&lt;RaycastVariable, RaycastPair, Raycast, RaycastEvent, RaycastPairEvent, RaycastRaycastFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Raycast Variable Instancer")]
    public class RaycastVariableInstancer : AtomVariableInstancer<
        RaycastVariable,
        RaycastPair,
        Raycast,
        RaycastEvent,
        RaycastPairEvent,
        RaycastRaycastFunction>
    { }
}

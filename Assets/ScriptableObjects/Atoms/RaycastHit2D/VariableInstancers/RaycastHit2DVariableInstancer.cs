using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Functions;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.VariableInstancers
{
    /// <summary>
    /// Variable Instancer of type `RaycastHit2D`. Inherits from `AtomVariableInstancer&lt;RaycastHit2DVariable, RaycastHit2DPair, RaycastHit2D, RaycastHit2DEvent, RaycastHit2DPairEvent, RaycastHit2DRaycastHit2DFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/RaycastHit2D Variable Instancer")]
    public class RaycastHit2DVariableInstancer : AtomVariableInstancer<
        RaycastHit2DVariable,
        RaycastHit2DPair,
        UnityEngine.RaycastHit2D,
        RaycastHit2DEvent,
        RaycastHit2DPairEvent,
        RaycastHit2DRaycastHit2DFunction>
    { }
}

using ScriptableObjects.Atoms.RaycastHit2D.Events;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.ValueLists
{
    /// <summary>
    /// Value List of type `RaycastHit2D`. Inherits from `AtomValueList&lt;RaycastHit2D, RaycastHit2DEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/RaycastHit2D", fileName = "RaycastHit2DValueList")]
    public sealed class RaycastHit2DValueList : AtomValueList<UnityEngine.RaycastHit2D, RaycastHit2DEvent> { }
}

using ScriptableObjects.Atoms.LayerMask.Events;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.ValueLists
{
    /// <summary>
    /// Value List of type `LayerMask`. Inherits from `AtomValueList&lt;LayerMask, LayerMaskEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/LayerMask", fileName = "LayerMaskValueList")]
    public sealed class LayerMaskValueList : AtomValueList<UnityEngine.LayerMask, LayerMaskEvent> { }
}

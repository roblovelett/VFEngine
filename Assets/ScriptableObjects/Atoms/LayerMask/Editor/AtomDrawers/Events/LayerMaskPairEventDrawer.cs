#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.LayerMask.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomDrawers.Events
{
    /// <summary>
    /// Event property drawer of type `LayerMaskPair`. Inherits from `AtomDrawer&lt;LayerMaskPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerMaskPairEvent))]
    public class LayerMaskPairEventDrawer : AtomDrawer<LayerMaskPairEvent> { }
}
#endif

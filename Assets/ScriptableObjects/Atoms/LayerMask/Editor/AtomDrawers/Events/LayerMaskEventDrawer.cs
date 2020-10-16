#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.LayerMask.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomDrawers.Events
{
    /// <summary>
    /// Event property drawer of type `LayerMask`. Inherits from `AtomDrawer&lt;LayerMaskEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerMaskEvent))]
    public class LayerMaskEventDrawer : AtomDrawer<LayerMaskEvent> { }
}
#endif

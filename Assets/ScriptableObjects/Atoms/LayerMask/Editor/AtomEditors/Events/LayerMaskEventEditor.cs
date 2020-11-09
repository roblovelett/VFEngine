#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.LayerMask.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `LayerMask`. Inherits from `AtomEventEditor&lt;LayerMask, LayerMaskEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(LayerMaskEvent))]
    public sealed class LayerMaskEventEditor : AtomEventEditor<UnityEngine.LayerMask, LayerMaskEvent> { }
}
#endif

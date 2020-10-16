#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `LayerMaskPair`. Inherits from `AtomEventEditor&lt;LayerMaskPair, LayerMaskPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(LayerMaskPairEvent))]
    public sealed class LayerMaskPairEventEditor : AtomEventEditor<LayerMaskPair, LayerMaskPairEvent> { }
}
#endif

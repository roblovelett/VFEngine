#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Mask.Events;
using ScriptableObjects.Atoms.Mask.Pairs;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Mask.Editor.AtomEditors.Events
{
    /// <summary>
    ///     Event property drawer of type `MaskPair`. Inherits from `AtomEventEditor&lt;MaskPair, MaskPairEvent&gt;`. Only
    ///     availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(MaskPairEvent))]
    public sealed class MaskPairEventEditor : AtomEventEditor<MaskPair, MaskPairEvent>
    {
    }
}
#endif
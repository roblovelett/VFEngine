#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Mask.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Mask.Editor.AtomEditors.Events
{
    /// <summary>
    ///     Event property drawer of type `Mask`. Inherits from `AtomEventEditor&lt;Mask, MaskEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(MaskEvent))]
    public sealed class MaskEventEditor : AtomEventEditor<Mask, MaskEvent>
    {
    }
}
#endif
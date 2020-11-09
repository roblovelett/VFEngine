#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Transform.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Transform.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `Transform`. Inherits from `AtomEventEditor&lt;Transform, TransformEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(TransformEvent))]
    public sealed class TransformEventEditor : AtomEventEditor<UnityEngine.Transform, TransformEvent> { }
}
#endif

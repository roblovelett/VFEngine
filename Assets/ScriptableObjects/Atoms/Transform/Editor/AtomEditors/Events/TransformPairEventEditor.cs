#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Pairs;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Transform.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `TransformPair`. Inherits from `AtomEventEditor&lt;TransformPair, TransformPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(TransformPairEvent))]
    public sealed class TransformPairEventEditor : AtomEventEditor<TransformPair, TransformPairEvent> { }
}
#endif

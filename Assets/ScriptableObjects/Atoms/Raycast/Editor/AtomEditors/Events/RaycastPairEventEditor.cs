#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Raycast.Events;
using ScriptableObjects.Atoms.Raycast.Pairs;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Raycast.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `RaycastPair`. Inherits from `AtomEventEditor&lt;RaycastPair, RaycastPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RaycastPairEvent))]
    public sealed class RaycastPairEventEditor : AtomEventEditor<RaycastPair, RaycastPairEvent> { }
}
#endif

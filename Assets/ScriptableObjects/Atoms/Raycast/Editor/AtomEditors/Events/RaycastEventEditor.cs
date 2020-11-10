#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Raycast.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Raycast.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `Raycast`. Inherits from `AtomEventEditor&lt;Raycast, RaycastEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RaycastEvent))]
    public sealed class RaycastEventEditor : AtomEventEditor<Raycast, RaycastEvent> { }
}
#endif

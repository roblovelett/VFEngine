#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `RaycastHit2D`. Inherits from `AtomEventEditor&lt;RaycastHit2D, RaycastHit2DEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RaycastHit2DEvent))]
    public sealed class RaycastHit2DEventEditor : AtomEventEditor<UnityEngine.RaycastHit2D, RaycastHit2DEvent> { }
}
#endif

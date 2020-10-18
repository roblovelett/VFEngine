#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomDrawers.Events
{
    /// <summary>
    /// Event property drawer of type `RaycastHit2D`. Inherits from `AtomDrawer&lt;RaycastHit2DEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastHit2DEvent))]
    public class RaycastHit2DEventDrawer : AtomDrawer<RaycastHit2DEvent> { }
}
#endif

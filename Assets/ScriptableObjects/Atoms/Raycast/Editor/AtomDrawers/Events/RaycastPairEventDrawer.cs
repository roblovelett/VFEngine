#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Raycast.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Raycast.Editor.AtomDrawers.Events
{
    /// <summary>
    /// Event property drawer of type `RaycastPair`. Inherits from `AtomDrawer&lt;RaycastPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastPairEvent))]
    public class RaycastPairEventDrawer : AtomDrawer<RaycastPairEvent> { }
}
#endif

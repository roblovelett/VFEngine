#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Raycast.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Raycast.Editor.AtomDrawers.Events
{
    /// <summary>
    ///     Event property drawer of type `Raycast`. Inherits from `AtomDrawer&lt;RaycastEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastEvent))]
    public class RaycastEventDrawer : AtomDrawer<RaycastEvent>
    {
    }
}
#endif
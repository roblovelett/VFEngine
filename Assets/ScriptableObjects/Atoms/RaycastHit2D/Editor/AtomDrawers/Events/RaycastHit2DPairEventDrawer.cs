#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomDrawers.Events
{
    /// <summary>
    /// Event property drawer of type `RaycastHit2DPair`. Inherits from `AtomDrawer&lt;RaycastHit2DPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastHit2DPairEvent))]
    public class RaycastHit2DPairEventDrawer : AtomDrawer<RaycastHit2DPairEvent> { }
}
#endif

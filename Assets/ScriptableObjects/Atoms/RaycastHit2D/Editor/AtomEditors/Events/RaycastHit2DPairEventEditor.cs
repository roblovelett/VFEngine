#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomEditors.Events
{
    /// <summary>
    /// Event property drawer of type `RaycastHit2DPair`. Inherits from `AtomEventEditor&lt;RaycastHit2DPair, RaycastHit2DPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RaycastHit2DPairEvent))]
    public sealed class RaycastHit2DPairEventEditor : AtomEventEditor<RaycastHit2DPair, RaycastHit2DPairEvent> { }
}
#endif

#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Mask.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Mask.Editor.AtomDrawers.Events
{
    /// <summary>
    ///     Event property drawer of type `MaskPair`. Inherits from `AtomDrawer&lt;MaskPairEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaskPairEvent))]
    public class MaskPairEventDrawer : AtomDrawer<MaskPairEvent>
    {
    }
}
#endif
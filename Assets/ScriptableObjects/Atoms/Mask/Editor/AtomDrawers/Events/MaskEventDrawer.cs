#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Mask.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Mask.Editor.AtomDrawers.Events
{
    /// <summary>
    ///     Event property drawer of type `Mask`. Inherits from `AtomDrawer&lt;MaskEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaskEvent))]
    public class MaskEventDrawer : AtomDrawer<MaskEvent>
    {
    }
}
#endif
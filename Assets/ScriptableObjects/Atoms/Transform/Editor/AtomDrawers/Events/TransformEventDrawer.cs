#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Transform.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Transform.Editor.AtomDrawers.Events
{
    /// <summary>
    ///     Event property drawer of type `Transform`. Inherits from `AtomDrawer&lt;TransformEvent&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TransformEvent))]
    public class TransformEventDrawer : AtomDrawer<TransformEvent>
    {
    }
}
#endif
#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Transform.Events;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Transform.Editor.AtomDrawers.Events
{
    /// <summary>
    ///     Event property drawer of type `TransformPair`. Inherits from `AtomDrawer&lt;TransformPairEvent&gt;`. Only availble
    ///     in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TransformPairEvent))]
    public class TransformPairEventDrawer : AtomDrawer<TransformPairEvent>
    {
    }
}
#endif
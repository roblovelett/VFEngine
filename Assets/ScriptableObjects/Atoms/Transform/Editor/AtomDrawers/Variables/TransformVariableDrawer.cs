#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Transform.Editor.AtomDrawers.Variables
{
    /// <summary>
    /// Variable property drawer of type `Transform`. Inherits from `AtomDrawer&lt;TransformVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TransformVariable))]
    public class TransformVariableDrawer : VariableDrawer<TransformVariable> { }
}
#endif

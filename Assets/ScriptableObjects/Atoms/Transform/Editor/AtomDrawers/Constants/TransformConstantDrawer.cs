#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Transform.Constants;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Transform.Editor.AtomDrawers.Constants
{
    /// <summary>
    ///     Constant property drawer of type `Transform`. Inherits from `AtomDrawer&lt;TransformConstant&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TransformConstant))]
    public class TransformConstantDrawer : VariableDrawer<TransformConstant>
    {
    }
}
#endif
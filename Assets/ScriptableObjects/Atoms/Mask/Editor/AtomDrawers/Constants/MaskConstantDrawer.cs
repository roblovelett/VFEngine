#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Mask.Constants;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Mask.Editor.AtomDrawers.Constants
{
    /// <summary>
    /// Constant property drawer of type `Mask`. Inherits from `AtomDrawer&lt;MaskConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaskConstant))]
    public class MaskConstantDrawer : VariableDrawer<MaskConstant> { }
}
#endif

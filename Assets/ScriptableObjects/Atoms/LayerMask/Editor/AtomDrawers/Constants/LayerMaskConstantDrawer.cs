#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.LayerMask.Constants;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomDrawers.Constants
{
    /// <summary>
    /// Constant property drawer of type `LayerMask`. Inherits from `AtomDrawer&lt;LayerMaskConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerMaskConstant))]
    public class LayerMaskConstantDrawer : VariableDrawer<LayerMaskConstant> { }
}
#endif

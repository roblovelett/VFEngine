#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.RaycastHit2D.Constants;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomDrawers.Constants
{
    /// <summary>
    /// Constant property drawer of type `RaycastHit2D`. Inherits from `AtomDrawer&lt;RaycastHit2DConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastHit2DConstant))]
    public class RaycastHit2DConstantDrawer : VariableDrawer<RaycastHit2DConstant> { }
}
#endif

#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Raycast.Constants;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Raycast.Editor.AtomDrawers.Constants
{
    /// <summary>
    /// Constant property drawer of type `Raycast`. Inherits from `AtomDrawer&lt;RaycastConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastConstant))]
    public class RaycastConstantDrawer : VariableDrawer<RaycastConstant> { }
}
#endif

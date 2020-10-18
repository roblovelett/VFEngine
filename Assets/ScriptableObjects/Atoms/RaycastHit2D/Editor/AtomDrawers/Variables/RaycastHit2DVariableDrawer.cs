#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomDrawers.Variables
{
    /// <summary>
    /// Variable property drawer of type `RaycastHit2D`. Inherits from `AtomDrawer&lt;RaycastHit2DVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastHit2DVariable))]
    public class RaycastHit2DVariableDrawer : VariableDrawer<RaycastHit2DVariable> { }
}
#endif

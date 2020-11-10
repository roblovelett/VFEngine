#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Raycast.Variables;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Raycast.Editor.AtomDrawers.Variables
{
    /// <summary>
    /// Variable property drawer of type `Raycast`. Inherits from `AtomDrawer&lt;RaycastVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastVariable))]
    public class RaycastVariableDrawer : VariableDrawer<RaycastVariable> { }
}
#endif

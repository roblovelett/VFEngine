#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomDrawers.Variables
{
    /// <summary>
    /// Variable property drawer of type `LayerMask`. Inherits from `AtomDrawer&lt;LayerMaskVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerMaskVariable))]
    public class LayerMaskVariableDrawer : VariableDrawer<LayerMaskVariable> { }
}
#endif

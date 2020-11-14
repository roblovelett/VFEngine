#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.Mask.Variables;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Mask.Editor.AtomDrawers.Variables
{
    /// <summary>
    ///     Variable property drawer of type `Mask`. Inherits from `AtomDrawer&lt;MaskVariable&gt;`. Only availble in
    ///     `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaskVariable))]
    public class MaskVariableDrawer : VariableDrawer<MaskVariable>
    {
    }
}
#endif
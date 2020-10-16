#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.LayerMask.ValueLists;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomDrawers.ValueLists
{
    /// <summary>
    /// Value List property drawer of type `LayerMask`. Inherits from `AtomDrawer&lt;LayerMaskValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerMaskValueList))]
    public class LayerMaskValueListDrawer : AtomDrawer<LayerMaskValueList> { }
}
#endif

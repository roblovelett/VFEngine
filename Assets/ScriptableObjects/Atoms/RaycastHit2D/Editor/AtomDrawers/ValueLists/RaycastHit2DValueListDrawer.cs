#if UNITY_2019_1_OR_NEWER
using ScriptableObjects.Atoms.RaycastHit2D.ValueLists;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomDrawers.ValueLists
{
    /// <summary>
    /// Value List property drawer of type `RaycastHit2D`. Inherits from `AtomDrawer&lt;RaycastHit2DValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RaycastHit2DValueList))]
    public class RaycastHit2DValueListDrawer : AtomDrawer<RaycastHit2DValueList> { }
}
#endif

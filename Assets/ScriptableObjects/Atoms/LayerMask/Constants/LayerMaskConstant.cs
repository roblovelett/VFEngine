using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.Constants
{
    /// <summary>
    /// Constant of type `LayerMask`. Inherits from `AtomBaseVariable&lt;LayerMask&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/LayerMask", fileName = "LayerMaskConstant")]
    public sealed class LayerMaskConstant : AtomBaseVariable<UnityEngine.LayerMask> { }
}

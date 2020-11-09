using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.Constants
{
    /// <summary>
    /// Constant of type `Transform`. Inherits from `AtomBaseVariable&lt;Transform&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/Transform", fileName = "TransformConstant")]
    public sealed class TransformConstant : AtomBaseVariable<UnityEngine.Transform> { }
}

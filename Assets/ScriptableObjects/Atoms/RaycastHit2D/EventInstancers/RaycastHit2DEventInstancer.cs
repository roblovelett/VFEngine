using ScriptableObjects.Atoms.RaycastHit2D.Events;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.EventInstancers
{
    /// <summary>
    /// Event Instancer of type `RaycastHit2D`. Inherits from `AtomEventInstancer&lt;RaycastHit2D, RaycastHit2DEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/RaycastHit2D Event Instancer")]
    public class RaycastHit2DEventInstancer : AtomEventInstancer<UnityEngine.RaycastHit2D, RaycastHit2DEvent> { }
}

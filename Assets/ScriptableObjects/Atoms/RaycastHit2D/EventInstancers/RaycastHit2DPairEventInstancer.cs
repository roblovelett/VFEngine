using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.EventInstancers
{
    /// <summary>
    /// Event Instancer of type `RaycastHit2DPair`. Inherits from `AtomEventInstancer&lt;RaycastHit2DPair, RaycastHit2DPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/RaycastHit2DPair Event Instancer")]
    public class RaycastHit2DPairEventInstancer : AtomEventInstancer<RaycastHit2DPair, RaycastHit2DPairEvent> { }
}

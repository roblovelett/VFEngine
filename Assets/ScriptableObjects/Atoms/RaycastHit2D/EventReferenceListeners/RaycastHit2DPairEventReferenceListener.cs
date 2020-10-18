using ScriptableObjects.Atoms.RaycastHit2D.EventReferences;
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using ScriptableObjects.Atoms.RaycastHit2D.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.EventReferenceListeners
{
    /// <summary>
    /// Event Reference Listener of type `RaycastHit2DPair`. Inherits from `AtomEventReferenceListener&lt;RaycastHit2DPair, RaycastHit2DPairEvent, RaycastHit2DPairEventReference, RaycastHit2DPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/RaycastHit2DPair Event Reference Listener")]
    public sealed class RaycastHit2DPairEventReferenceListener : AtomEventReferenceListener<
        RaycastHit2DPair,
        RaycastHit2DPairEvent,
        RaycastHit2DPairEventReference,
        RaycastHit2DPairUnityEvent>
    { }
}

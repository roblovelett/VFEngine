using ScriptableObjects.Atoms.RaycastHit2D.EventReferences;
using ScriptableObjects.Atoms.RaycastHit2D.Events;
using ScriptableObjects.Atoms.RaycastHit2D.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.EventReferenceListeners
{
    /// <summary>
    /// Event Reference Listener of type `RaycastHit2D`. Inherits from `AtomEventReferenceListener&lt;RaycastHit2D, RaycastHit2DEvent, RaycastHit2DEventReference, RaycastHit2DUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/RaycastHit2D Event Reference Listener")]
    public sealed class RaycastHit2DEventReferenceListener : AtomEventReferenceListener<
        UnityEngine.RaycastHit2D,
        RaycastHit2DEvent,
        RaycastHit2DEventReference,
        RaycastHit2DUnityEvent>
    { }
}

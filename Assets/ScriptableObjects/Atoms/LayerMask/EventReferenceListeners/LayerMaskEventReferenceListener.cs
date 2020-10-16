using ScriptableObjects.Atoms.LayerMask.EventReferences;
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.EventReferenceListeners
{
    /// <summary>
    /// Event Reference Listener of type `LayerMask`. Inherits from `AtomEventReferenceListener&lt;LayerMask, LayerMaskEvent, LayerMaskEventReference, LayerMaskUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/LayerMask Event Reference Listener")]
    public sealed class LayerMaskEventReferenceListener : AtomEventReferenceListener<
        UnityEngine.LayerMask,
        LayerMaskEvent,
        LayerMaskEventReference,
        LayerMaskUnityEvent>
    { }
}

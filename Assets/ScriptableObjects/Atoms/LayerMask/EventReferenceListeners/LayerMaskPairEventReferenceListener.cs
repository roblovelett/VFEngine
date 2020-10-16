using ScriptableObjects.Atoms.LayerMask.EventReferences;
using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using ScriptableObjects.Atoms.LayerMask.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.EventReferenceListeners
{
    /// <summary>
    /// Event Reference Listener of type `LayerMaskPair`. Inherits from `AtomEventReferenceListener&lt;LayerMaskPair, LayerMaskPairEvent, LayerMaskPairEventReference, LayerMaskPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/LayerMaskPair Event Reference Listener")]
    public sealed class LayerMaskPairEventReferenceListener : AtomEventReferenceListener<
        LayerMaskPair,
        LayerMaskPairEvent,
        LayerMaskPairEventReference,
        LayerMaskPairUnityEvent>
    { }
}

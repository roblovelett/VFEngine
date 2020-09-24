using ScriptableObjects.Atoms.Transform.EventReferences;
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.EventReferenceListeners
{
    /// <summary>
    ///     Event Reference Listener of type `Transform`. Inherits from `AtomEventReferenceListener&lt;Transform,
    ///     TransformEvent, TransformEventReference, TransformUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Transform Event Reference Listener")]
    public sealed class TransformEventReferenceListener : AtomEventReferenceListener<UnityEngine.Transform, TransformEvent,
        TransformEventReference, TransformUnityEvent>
    {
    }
}
using ScriptableObjects.Atoms.Transform.EventReferences;
using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Pairs;
using ScriptableObjects.Atoms.Transform.UnityEvents;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.EventReferenceListeners
{
    /// <summary>
    ///     Event Reference Listener of type `TransformPair`. Inherits from `AtomEventReferenceListener&lt;TransformPair,
    ///     TransformPairEvent, TransformPairEventReference, TransformPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/TransformPair Event Reference Listener")]
    public sealed class TransformPairEventReferenceListener : AtomEventReferenceListener<TransformPair,
        TransformPairEvent, TransformPairEventReference, TransformPairUnityEvent>
    {
    }
}
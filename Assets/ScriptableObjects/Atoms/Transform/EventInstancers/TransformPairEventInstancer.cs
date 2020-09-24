using ScriptableObjects.Atoms.Transform.Events;
using ScriptableObjects.Atoms.Transform.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.EventInstancers
{
    /// <summary>
    ///     Event Instancer of type `TransformPair`. Inherits from `AtomEventInstancer&lt;TransformPair, TransformPairEvent&gt;
    ///     `.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/TransformPair Event Instancer")]
    public class TransformPairEventInstancer : AtomEventInstancer<TransformPair, TransformPairEvent>
    {
    }
}
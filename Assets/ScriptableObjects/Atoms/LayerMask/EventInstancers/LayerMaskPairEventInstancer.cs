using ScriptableObjects.Atoms.LayerMask.Events;
using ScriptableObjects.Atoms.LayerMask.Pairs;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.EventInstancers
{
    /// <summary>
    /// Event Instancer of type `LayerMaskPair`. Inherits from `AtomEventInstancer&lt;LayerMaskPair, LayerMaskPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/LayerMaskPair Event Instancer")]
    public class LayerMaskPairEventInstancer : AtomEventInstancer<LayerMaskPair, LayerMaskPairEvent> { }
}

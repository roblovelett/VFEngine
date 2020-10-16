using ScriptableObjects.Atoms.LayerMask.Events;
using UnityAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.EventInstancers
{
    /// <summary>
    /// Event Instancer of type `LayerMask`. Inherits from `AtomEventInstancer&lt;LayerMask, LayerMaskEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/LayerMask Event Instancer")]
    public class LayerMaskEventInstancer : AtomEventInstancer<UnityEngine.LayerMask, LayerMaskEvent> { }
}

using ScriptableObjects.Atoms.LayerMask.VariableInstancers;
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.LayerMask.SyncVariableInstancerToCollection
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type LayerMask to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync LayerMask Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncLayerMaskVariableInstancerToCollection : SyncVariableInstancerToCollection<UnityEngine.LayerMask, LayerMaskVariable, LayerMaskVariableInstancer> { }
}

using ScriptableObjects.Atoms.RaycastHit2D.VariableInstancers;
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.RaycastHit2D.SyncVariableInstancerToCollection
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type RaycastHit2D to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync RaycastHit2D Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncRaycastHit2DVariableInstancerToCollection : SyncVariableInstancerToCollection<UnityEngine.RaycastHit2D, RaycastHit2DVariable, RaycastHit2DVariableInstancer> { }
}

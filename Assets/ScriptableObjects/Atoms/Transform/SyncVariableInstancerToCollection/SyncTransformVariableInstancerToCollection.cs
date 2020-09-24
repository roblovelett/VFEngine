using ScriptableObjects.Atoms.Transform.VariableInstancers;
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace ScriptableObjects.Atoms.Transform.SyncVariableInstancerToCollection
{
    /// <summary>
    ///     Adds Variable Instancer's Variable of type Transform to a Collection or List on OnEnable and removes it on
    ///     OnDestroy.
    /// </summary>
    [AddComponentMenu(
        "Unity Atoms/Sync Variable Instancer to Collection/Sync Transform Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncTransformVariableInstancerToCollection : SyncVariableInstancerToCollection<UnityEngine.Transform,
        TransformVariable, TransformVariableInstancer>
    {
    }
}
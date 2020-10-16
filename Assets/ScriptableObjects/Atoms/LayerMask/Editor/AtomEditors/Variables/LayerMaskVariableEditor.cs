using ScriptableObjects.Atoms.LayerMask.Pairs;
using ScriptableObjects.Atoms.LayerMask.Variables;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.LayerMask.Editor.AtomEditors.Variables
{
    /// <summary>
    ///     Variable Inspector of type `LayerMask`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(LayerMaskVariable))]
    public sealed class LayerMaskVariableEditor : AtomVariableEditor<UnityEngine.LayerMask, LayerMaskPair>
    {
    }
}
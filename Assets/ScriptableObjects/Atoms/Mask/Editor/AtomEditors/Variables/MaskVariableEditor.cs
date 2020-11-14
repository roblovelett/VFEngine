using ScriptableObjects.Atoms.Mask.Pairs;
using ScriptableObjects.Atoms.Mask.Variables;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Mask.Editor.AtomEditors.Variables
{
    /// <summary>
    ///     Variable Inspector of type `Mask`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(MaskVariable))]
    public sealed class MaskVariableEditor : AtomVariableEditor<Mask, MaskPair>
    {
    }
}
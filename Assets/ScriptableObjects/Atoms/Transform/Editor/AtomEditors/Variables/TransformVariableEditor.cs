using ScriptableObjects.Atoms.Transform.Pairs;
using ScriptableObjects.Atoms.Transform.Variables;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Transform.Editor.AtomEditors.Variables
{
    /// <summary>
    /// Variable Inspector of type `Transform`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(TransformVariable))]
    public sealed class TransformVariableEditor : AtomVariableEditor<UnityEngine.Transform, TransformPair> { }
}

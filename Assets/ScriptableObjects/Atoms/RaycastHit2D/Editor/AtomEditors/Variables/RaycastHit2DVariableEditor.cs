using ScriptableObjects.Atoms.RaycastHit2D.Pairs;
using ScriptableObjects.Atoms.RaycastHit2D.Variables;
using UnityAtoms;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.RaycastHit2D.Editor.AtomEditors.Variables
{
    /// <summary>
    /// Variable Inspector of type `RaycastHit2D`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(RaycastHit2DVariable))]
    public sealed class RaycastHit2DVariableEditor : AtomVariableEditor<UnityEngine.RaycastHit2D, RaycastHit2DPair> { }
}

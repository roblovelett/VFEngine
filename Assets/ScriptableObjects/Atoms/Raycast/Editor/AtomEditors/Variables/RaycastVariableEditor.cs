using ScriptableObjects.Atoms.Raycast.Pairs;
using ScriptableObjects.Atoms.Raycast.Variables;
using UnityAtoms.Editor;
using UnityEditor;

namespace ScriptableObjects.Atoms.Raycast.Editor.AtomEditors.Variables
{
    /// <summary>
    ///     Variable Inspector of type `Raycast`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(RaycastVariable))]
    public sealed class RaycastVariableEditor : AtomVariableEditor<Raycast, RaycastPair>
    {
    }
}
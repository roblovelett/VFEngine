using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.ScriptableObjects
{
    internal class TreeViewStateSO : ScriptableObject
    {
        internal TreeViewState TreeViewState { get; } = new TreeViewState();
    }
}
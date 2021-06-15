using UnityEditor;
using UnityEngine;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data
{
    using static EditorStyles;
    using static FontStyle;

    internal class ReplacePrefabSearchPopUpStyles
    {
        internal readonly GUIStyle HeaderLabel;

        internal ReplacePrefabSearchPopUpStyles()
        {
            HeaderLabel = new GUIStyle(centeredGreyMiniLabel) {fontSize = 11, fontStyle = Bold};
        }
    }
}
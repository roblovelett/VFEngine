using UnityEditor;
using UnityEngine;

namespace VFEngine.Tools.Prefab.Editor.ReplacePrefabSearchPopUp.Data
{
    using static EditorStyles;
    using static FontStyle;

    public class Styles
    {
        internal readonly GUIStyle HeaderLabel;

        internal Styles()
        {
            HeaderLabel = new GUIStyle(centeredGreyMiniLabel) {fontSize = 11, fontStyle = Bold};
        }
    }
}
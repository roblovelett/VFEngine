using UnityEditor;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor
{
    using static EditorGUIUtility;
    using static FontStyle;
    using static TextAnchor;
    using static EditorSkin;

    internal static class ContentStyle
    {
        internal static Color DarkGray { get; private set; }
        internal static Color LightGray { get; private set; }
        internal static Color Focused { get; private set; }
        internal static Color ZebraDark { get; private set; }
        internal static Color ZebraLight { get; private set; }
        internal static RectOffset Padding { get; private set; }
        internal static RectOffset LeftPadding { get; private set; }
        internal static RectOffset Margin { get; private set; }
        internal static GUIStyle BoldCentered { get; private set; }
        internal static GUIStyle StateListStyle { get; private set; }
        internal static GUIStyle WithPadding { get; private set; }
        internal static GUIStyle WithPaddingAndMargins { get; private set; }
        private static bool _initialized;

        [InitializeOnLoadMethod]
        internal static void Initialize()
        {
            var guiStyleStateNormal = GetBuiltinSkin(Inspector).label.normal;
            if (_initialized) return;
            _initialized = true;
            DarkGray = isProSkin ? new Color(0.283f, 0.283f, 0.283f) : new Color(0.7f, 0.7f, 0.7f);
            LightGray = isProSkin ? new Color(0.33f, 0.33f, 0.33f) : new Color(0.8f, 0.8f, 0.8f);
            ZebraDark = new Color(0.4f, 0.4f, 0.4f, 0.1f);
            ZebraLight = new Color(0.8f, 0.8f, 0.8f, 0.1f);
            Focused = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Padding = new RectOffset(5, 5, 5, 5);
            LeftPadding = new RectOffset(10, 0, 0, 0);
            Margin = new RectOffset(8, 8, 8, 8);
            WithPadding = new GUIStyle {padding = Padding};
            WithPaddingAndMargins = new GUIStyle {padding = Padding, margin = Margin};
            guiStyleStateNormal.textColor = isProSkin ? new Color(.85f, .85f, .85f) : new Color(0.337f, 0.337f, 0.337f);
            BoldCentered = new GUIStyle {fontStyle = Bold, alignment = MiddleCenter};
            StateListStyle = new GUIStyle
            {
                alignment = MiddleLeft,
                padding = LeftPadding,
                fontStyle = Bold,
                fontSize = 12,
                margin = Margin,
                normal = guiStyleStateNormal
            };
        }
    }
}
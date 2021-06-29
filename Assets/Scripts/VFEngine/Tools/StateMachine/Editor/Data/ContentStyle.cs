using UnityEditor;
using UnityEngine;

namespace VFEngine.Tools.StateMachine.Editor.Data
{
    using static EditorSkin;
    using static FontStyle;
    using static TextAnchor;
    using static EditorGUIUtility;

    internal static class ContentStyle
    {
        private static bool _initialised;
        private static RectOffset _padding;
        private static RectOffset _leftPadding;
        private static RectOffset _margin;
        private static GUIStyleState _guiStyleStateNormal;
        internal static Color DarkGray { get; private set; }
        internal static Color LightGray { get; private set; }
        internal static Color Focused { get; private set; }
        internal static Color ZebraDark { get; private set; }
        internal static Color ZebraLight { get; private set; }
        internal static GUIStyle StateListStyle { get; private set; }
        internal static GUIStyle WithPaddingAndMargins { get; private set; }

        [InitializeOnLoadMethod]
        internal static void Initialize()
        {
            if (_initialised) return;
            _initialised = !_initialised;
            DarkGray = isProSkin ? new Color(0.283f, 0.283f, 0.283f) : new Color(0.7f, 0.7f, 0.7f);
            LightGray = isProSkin ? new Color(0.33f, 0.33f, 0.33f) : new Color(0.8f, 0.8f, 0.8f);
            ZebraDark = new Color(0.4f, 0.4f, 0.4f, 0.1f);
            ZebraLight = new Color(0.8f, 0.8f, 0.8f, 0.1f);
            Focused = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            _padding = new RectOffset(5, 5, 5, 5);
            _leftPadding = new RectOffset(10, 0, 0, 0);
            _margin = new RectOffset(8, 8, 8, 8);
            WithPaddingAndMargins = new GUIStyle {padding = _padding, margin = _margin};
            _guiStyleStateNormal = GetBuiltinSkin(Inspector).label.normal;
            _guiStyleStateNormal.textColor =
                isProSkin ? new Color(.85f, .85f, .85f) : new Color(0.337f, 0.337f, 0.337f);
            StateListStyle = new GUIStyle
            {
                alignment = MiddleLeft,
                padding = _leftPadding,
                fontStyle = Bold,
                fontSize = 12,
                margin = _margin,
                normal = _guiStyleStateNormal
            };
        }
    }
}
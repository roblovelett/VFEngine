using UnityEditor;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects.Menu
{
    using static EditorGUIUtility;
    using static EditorSkin;
    using static FontStyle;
    using static TextAnchor;

    internal static class ContentStyle
    {
        private static bool _initialised;
        private static RectOffset _padding;
        private static RectOffset _leftPadding;
        private static RectOffset _margin;
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
            var guiStyleStateNormal = GetBuiltinSkin(Inspector).label.normal;
            _initialised = true;
            _padding = new RectOffset(5, 5, 5, 5);
            _leftPadding = new RectOffset(10, 0, 0, 0);
            _margin = new RectOffset(8, 8, 8, 8);
            guiStyleStateNormal.textColor = isProSkin ? new Color(.85f, .85f, .85f) : new Color(0.337f, 0.337f, 0.337f);
            DarkGray = isProSkin ? new Color(0.283f, 0.283f, 0.283f) : new Color(0.7f, 0.7f, 0.7f);
            LightGray = isProSkin ? new Color(0.33f, 0.33f, 0.33f) : new Color(0.8f, 0.8f, 0.8f);
            ZebraDark = new Color(0.4f, 0.4f, 0.4f, 0.1f);
            ZebraLight = new Color(0.8f, 0.8f, 0.8f, 0.1f);
            Focused = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            WithPaddingAndMargins = new GUIStyle {padding = _padding, margin = _margin};
            StateListStyle = new GUIStyle
            {
                alignment = MiddleLeft,
                padding = _leftPadding,
                fontStyle = Bold,
                fontSize = 12,
                margin = _margin,
                normal = guiStyleStateNormal
            };
        }
    }
}
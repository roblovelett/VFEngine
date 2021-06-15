using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;

namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Types
{
    using static EditorApplication;
    using static EditorGUI;
    using static EditorGUIUtility;
    using static MessageType;
    using static GUI;

    //using static VFEngine.Tools.StateMachine.Editor.Types;
    [CustomPropertyDrawer(typeof(InitOnlyAttribute))]
    public class InitOnlyAttributeDrawer : PropertyDrawer
    {
        private const string Text =
            "Changes to this parameter during Play mode won't be reflected on existing StateMachines";

        private static readonly GUIStyle Style = new GUIStyle(skin.GetStyle("helpbox"))
        {
            padding = new RectOffset(5, 5, 5, 5)
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (isPlaying)
            {
                position.height = Style.CalcHeight(new GUIContent(Text), currentViewWidth);
                HelpBox(position, Text, Info);
                position.y += position.height + standardVerticalSpacing;
                position.height = EditorGUI.GetPropertyHeight(property, label);
            }

            PropertyField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property, label);
            if (isPlaying)
                height += Style.CalcHeight(new GUIContent(Text), currentViewWidth) + standardVerticalSpacing * 4;
            return height;
        }
    }
}
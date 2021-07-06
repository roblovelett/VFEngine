using UnityEditor;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.Editor.Data;

namespace VFEngine.Tools.StateMachine.ScriptableObjects.Editor
{
    using static MessageType;
    using static EditorGUI;
    using static EditorGUIUtility;
    using static GUI;
    using static EditorText;
    using static EditorApplication;

    [CustomPropertyDrawer(typeof(InitOnlyAttribute))]
    public class InitOnlyAttributeDrawer : PropertyDrawer
    {
        private static readonly GUIStyle Style = new GUIStyle(skin.GetStyle(HelpBoxField))
        {
            padding = new RectOffset(5, 5, 5, 5)
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (isPlaying)
            {
                position.height = Style.CalcHeight(new GUIContent(InitOnlyAttributeMessage), currentViewWidth);
                HelpBox(position, InitOnlyAttributeMessage, Info);
                position.y += position.height + standardVerticalSpacing;
                position.height = EditorGUI.GetPropertyHeight(property, label);
            }

            PropertyField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property, label);
            if (isPlaying)
                height += Style.CalcHeight(new GUIContent(InitOnlyAttributeMessage), currentViewWidth) +
                          standardVerticalSpacing * 4;
            return height;
        }
    }
}
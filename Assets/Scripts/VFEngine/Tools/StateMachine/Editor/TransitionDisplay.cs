using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;
using UnityObject = UnityEngine.Object;

// ReSharper disable UnusedParameter.Local
namespace VFEngine.Tools.StateMachine.Editor
{
    using static EditorGUI;
    using static EditorGUIUtility;
    using static EditorStyles;
    using static GUI;
    using static GUIContent;
    using static GUILayoutUtility;
    using static ContentStyle;
    using static EditorText;

    internal class TransitionDisplay
    {
        internal SerializedTransition SerializedTransition { get; }
        private readonly ReorderableList reorderableList;
        private readonly TransitionTableEditor editor;

        internal TransitionDisplay(SerializedTransition serializedTransition, TransitionTableEditor editorInternal)
        {
            SerializedTransition = serializedTransition;
            reorderableList = new ReorderableList(SerializedTransition.Transition.serializedObject,
                SerializedTransition.Conditions, true, false, true, true);
            reorderableList.elementHeight *= 2.3f;
            reorderableList.headerHeight = 1f;
            reorderableList.onAddCallback += list =>
            {
                var count = list.count;
                list.serializedProperty.InsertArrayElementAtIndex(count);
                var prop = list.serializedProperty.GetArrayElementAtIndex(count);
                prop.FindPropertyRelative(Condition).objectReferenceValue = null;
                prop.FindPropertyRelative(ExpectedResult).enumValueIndex = 0;
                prop.FindPropertyRelative(Operator).enumValueIndex = 0;
            };
            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var prop = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                rect = new Rect(rect.x, rect.y + 2.5f, rect.width, singleLineHeight);
                var condition = prop.FindPropertyRelative(Condition);
                if (condition.objectReferenceValue != null)
                {
                    var label = condition.objectReferenceValue.name;
                    Label(rect, If);
                    var r = rect;
                    r.x += 20;
                    r.width = 35;
                    PropertyField(r, condition, none);
                    r.x += 40;
                    r.width = rect.width - 120;
                    Label(r, label, boldLabel);
                }
                else
                {
                    PropertyField(new Rect(rect.x, rect.y, 150, rect.height), condition, none);
                }

                LabelField(new Rect(rect.x + rect.width - 80, rect.y, 20, rect.height), Is);
                PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height),
                    prop.FindPropertyRelative(ExpectedResult), none);
                if (index < reorderableList.count - 1)
                    PropertyField(new Rect(rect.x + 20, rect.y + singleLineHeight + 5, 60, rect.height),
                        prop.FindPropertyRelative(Operator), none);
            };
            reorderableList.onChangedCallback +=
                list => list.serializedProperty.serializedObject.ApplyModifiedProperties();
            reorderableList.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
            {
                if (isFocused) DrawRect(rect, Focused);
                DrawRect(rect, index % 2 != 0 ? ZebraDark : ZebraLight);
            };
            editor = editorInternal;
        }

        internal bool Display(ref Rect position)
        {
            var rect = position;
            var listHeight = reorderableList.GetHeight();
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            rect.height = singleLineHeight + 10 + listHeight;
            GetRect(rect.width, rect.height);
            position.y += rect.height + 5;
            rect.x += 5;
            rect.width -= 10;
            rect.height -= listHeight;
            DrawRect(rect, DarkGray);
            rect.x += 3;
            LabelField(rect, To);
            rect.x += 20;
            LabelField(rect, SerializedTransition.ToState.objectReferenceValue.name, boldLabel);
            var buttonRect = new Rect(rect.width - 25, rect.y + 5, 30, 18);
            var transitions = editor.GetStateTransitions(SerializedTransition.FromState.objectReferenceValue);
            var transitionsIndex = transitions.Count - 1;
            var serializedTransitionIndex = transitions.FindIndex(t => t.Index == SerializedTransition.Index);
            if (GUIButton(buttonRect, ToolbarMinus))
            {
                editor.RemoveTransition(SerializedTransition);
                return true;
            }

            buttonRect.x -= 35;
            if (serializedTransitionIndex < transitionsIndex)
            {
                if (GUIButton(buttonRect, ScrollDown))
                {
                    editor.ReorderTransition(SerializedTransition, false);
                    return true;
                }

                buttonRect.x -= 35;
            }

            if (serializedTransitionIndex > 0)
            {
                if (GUIButton(buttonRect, ScrollUp))
                {
                    editor.ReorderTransition(SerializedTransition, true);
                    return true;
                }

                buttonRect.x -= 35;
            }

            if (GUIButton(buttonRect, SceneViewTools))
            {
                editor.DisplayStateEditor(SerializedTransition.ToState.objectReferenceValue);
                return true;
            }

            rect.x = position.x + 5;
            rect.y += rect.height;
            rect.width = position.width - 10;
            rect.height = listHeight;
            reorderableList.DoList(rect);
            return false;
        }

        private static bool GUIButton(Rect pos, string icon)
        {
            return Button(pos, IconContent(icon));
        }
    }
}
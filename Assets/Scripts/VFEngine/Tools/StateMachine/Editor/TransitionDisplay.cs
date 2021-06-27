using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;

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
        private int transitionsLength;
        private int transitionsIndex;
        private float listHeight;
        private float singleLineHeight;
        private Rect displayRect;
        private Rect buttonRect;
        private List<SerializedTransition> transitions;
        private readonly ReorderableList reorderableList;
        private readonly TransitionTableEditor editor;
        internal SerializedTransition SerializedTransition { get; }

        internal TransitionDisplay(SerializedTransition serializedTransition, TransitionTableEditor editorInternal)
        {
            SerializedTransition = serializedTransition;
            reorderableList = new ReorderableList(SerializedTransition.Transition.serializedObject,
                SerializedTransition.Conditions, true, false, true, true);
            reorderableList.elementHeight *= 2.3f;
            reorderableList.headerHeight = 1f;
            reorderableList.onAddCallback += list =>
            {
                var reorderableListItemsAmount = list.count;
                list.serializedProperty.InsertArrayElementAtIndex(reorderableListItemsAmount);
                var reorderableListProperty =
                    list.serializedProperty.GetArrayElementAtIndex(reorderableListItemsAmount);
                reorderableListProperty.FindPropertyRelative(Condition).objectReferenceValue = null;
                reorderableListProperty.FindPropertyRelative(ExpectedResult).enumValueIndex = 0;
                reorderableListProperty.FindPropertyRelative(Operator).enumValueIndex = 0;
            };
            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var serializedProperty = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                rect = new Rect(rect.x, rect.y + 2.5f, rect.width, EditorGUIUtility.singleLineHeight);
                var condition = serializedProperty.FindPropertyRelative(Condition);
                if (condition.objectReferenceValue != null)
                {
                    var conditionLabel = condition.objectReferenceValue.name;
                    Label(rect, If);
                    var conditionRect = rect;
                    conditionRect.x += 20;
                    conditionRect.width = 35;
                    PropertyField(conditionRect, condition, none);
                    conditionRect.x += 40;
                    conditionRect.width = rect.width - 120;
                    Label(conditionRect, conditionLabel, boldLabel);
                }
                else
                {
                    PropertyField(new Rect(rect.x, rect.y, 150, rect.height), condition, none);
                }

                LabelField(new Rect(rect.x + rect.width - 80, rect.y, 20, rect.height), Is);
                PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height),
                    serializedProperty.FindPropertyRelative(ExpectedResult), none);
                if (index < reorderableList.count - 1)
                    PropertyField(
                        new Rect(rect.x + 20, rect.y + EditorGUIUtility.singleLineHeight + 5, 60, rect.height),
                        serializedProperty.FindPropertyRelative(Operator), none);
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
            displayRect = position;
            listHeight = reorderableList.GetHeight();
            singleLineHeight = EditorGUIUtility.singleLineHeight;
            displayRect.height = singleLineHeight + 10 + listHeight;
            GetRect(displayRect.width, displayRect.height);
            position.y += displayRect.height + 5;
            displayRect.x += 5;
            displayRect.width -= 10;
            displayRect.height -= listHeight;
            DrawRect(displayRect, DarkGray);
            displayRect.x += 3;
            LabelField(displayRect, To);
            displayRect.x += 20;
            LabelField(displayRect, SerializedTransition.ToState.objectReferenceValue.name, boldLabel);
            buttonRect = new Rect(displayRect.width - 25, displayRect.y + 5, 30, 18);
            transitions = editor
                .TransitionsByFromStates[editor.FromStates.IndexOf(SerializedTransition.FromState.objectReferenceValue)]
                .Select(transitionDisplay => transitionDisplay.SerializedTransition).ToList();
            transitionsLength = transitions.Count - 1;
            transitionsIndex = transitions.FindIndex(t => t.Index == SerializedTransition.Index);
            if (ButtonPressed(ToolbarMinus, false, false, false, true)) return true;
            if (transitionsIndex < transitionsLength)
                if (ButtonPressed(ScrollDown, true, false, false, false))
                    return true;
            if (transitionsIndex > 0)
                if (ButtonPressed(ScrollUp, true, true, false, false))
                    return true;
            if (ButtonPressed(SceneViewTools, false, false, true, false)) return true;
            displayRect.x = position.x + 5;
            displayRect.y += displayRect.height;
            displayRect.width = position.width - 10;
            displayRect.height = listHeight;
            reorderableList.DoList(displayRect);
            return false;
        }

        private bool ButtonPressed(string icon, bool reorderTransition, bool up, bool displayStateEditor,
            bool removeTransition)
        {
            if (!Button(buttonRect, IconContent(icon))) return false;
            if (displayStateEditor)
            {
                editor.DisplayStateEditor(SerializedTransition.ToState.objectReferenceValue);
                return true;
            }

            if (reorderTransition)
            {
                editor.ReorderTransition(SerializedTransition, up);
                return true;
            }

            if (removeTransition)
            {
                editor.RemoveTransition(SerializedTransition);
                return true;
            }

            buttonRect.x -= 35;
            return false;
        }
    }
}
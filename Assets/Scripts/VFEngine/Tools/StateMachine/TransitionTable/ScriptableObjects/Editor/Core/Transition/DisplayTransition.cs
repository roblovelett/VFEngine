using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core;

// ReSharper disable LocalFunctionCanBeMadeStatic
// ReSharper disable UnusedParameter.Local
namespace VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor
{
    using static EditorGUI;
    using static GUILayoutUtility;
    using static EditorGUIUtility;
    using static EditorStyles;
    using static GUI;
    using static GUIContent;
    using static ContentStyle;

    public class DisplayTransition
    {
        internal SerializedTransition SerializedTransition { get; set; }
        private readonly ReorderableList reorderableList;
        private readonly TransitionTableEditor editor;

        internal DisplayTransition(SerializedTransition serializedTransition, TransitionTableEditor editorInternal)
        {
            SerializedTransition = serializedTransition;
            reorderableList = new ReorderableList(SerializedTransition.Transition.serializedObject,
                SerializedTransition.Conditions, true, false, true, true);
            SetupConditionsList(reorderableList);
            editor = editorInternal;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal bool Display(ref Rect position)
        {
            var rect = position;
            var listHeight = reorderableList.GetHeight();
            var singleLineHeight = EditorGUIUtility.singleLineHeight;

            // Reserve space
            {
                rect.height = singleLineHeight + 10 + listHeight;
                GetRect(rect.width, rect.height);
                position.y += rect.height + 5;
            }

            // Background
            {
                rect.x += 5;
                rect.width -= 10;
                rect.height -= listHeight;
                DrawRect(rect, DarkGray);
            }

            // Transition Header
            {
                rect.x += 3;
                LabelField(rect, "To");
                rect.x += 20;
                LabelField(rect, SerializedTransition.ToState?.objectReferenceValue.name, boldLabel);
            }

            // Buttons
            {
                bool Button(Rect pos, string icon)
                {
                    return GUI.Button(pos, IconContent(icon));
                }

                var buttonRect = new Rect(rect.width - 25, rect.y + 5, 30, 18);
                //int i, l;
                {
                    /*var transitions = editor.GetTransitions(SerializedTransition.FromState.objectReferenceValue);
                    l = transitions.Count - 1;
                    i = transitions.FindIndex(t => t.Index == SerializedTransition.Index);*/
                }

                // Remove transition
                if (Button(buttonRect, "Toolbar Minus"))
                {
                    //editor.RemoveTransition(SerializedTransition);
                    return true;
                }

                buttonRect.x -= 35;

                // Move transition down
                /*if (i < l)
                {
                    if (Button(buttonRect, "scrolldown"))
                    {
                        editor.ReorderTransition(SerializedTransition, false);
                        return true;
                    }

                    buttonRect.x -= 35;
                }*/

                // Move transition up
                /*if (i > 0)
                {
                    if (Button(buttonRect, "scrollup"))
                    {
                        editor.ReorderTransition(SerializedTransition, true);
                        return true;
                    }

                    buttonRect.x -= 35;
                }

                // State editor
                if (Button(buttonRect, "SceneViewTools"))
                {
                    editor.DisplayStateEditor(SerializedTransition.ToState.objectReferenceValue);
                    return true;
                }*/
            }
            rect.x = position.x + 5;
            rect.y += rect.height;
            rect.width = position.width - 10;
            rect.height = listHeight;

            // Display conditions
            reorderableList.DoList(rect);
            return false;
        }

        private static void SetupConditionsList(ReorderableList reorderableList)
        {
            reorderableList.elementHeight *= 2.3f;
            reorderableList.headerHeight = 1f;
            reorderableList.onAddCallback += list =>
            {
                var count = list.count;
                list.serializedProperty.InsertArrayElementAtIndex(count);
                var prop = list.serializedProperty.GetArrayElementAtIndex(count);
                prop.FindPropertyRelative("Condition").objectReferenceValue = null;
                prop.FindPropertyRelative("ExpectedResult").enumValueIndex = 0;
                prop.FindPropertyRelative("Operator").enumValueIndex = 0;
            };
            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var prop = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                rect = new Rect(rect.x, rect.y + 2.5f, rect.width, singleLineHeight);
                var condition = prop.FindPropertyRelative("Condition");

                // Draw the picker for the Condition SO
                if (condition.objectReferenceValue != null)
                {
                    var label = condition.objectReferenceValue.name;
                    Label(rect, "If");
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

                // Draw the boolean value expected by the condition (i.e. "Is True", "Is False")
                LabelField(new Rect(rect.x + rect.width - 80, rect.y, 20, rect.height), "Is");
                PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height),
                    prop.FindPropertyRelative("ExpectedResult"), none);

                // Only display the logic condition if there's another one after this
                if (index < reorderableList.count - 1)
                    PropertyField(new Rect(rect.x + 20, rect.y + singleLineHeight + 5, 60, rect.height),
                        prop.FindPropertyRelative("Operator"), none);
            };
            reorderableList.onChangedCallback +=
                list => list.serializedProperty.serializedObject.ApplyModifiedProperties();
            reorderableList.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
            {
                if (isFocused) DrawRect(rect, Focused);
                DrawRect(rect, index % 2 != 0 ? ZebraDark : ZebraLight);
            };
        }
    }
}
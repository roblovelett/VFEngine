using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachineSO.Editor.Data;
using VFEngine.Tools.StateMachineSO.Editor.Data.ScriptableObjects;
using UnityObject = UnityEngine.Object;

// ReSharper disable UnusedParameter.Local
namespace VFEngine.Tools.StateMachineSO.Editor
{
    using static GC;
    using static EditorGUI;
    using static EditorGUIUtility;
    using static EditorStyles;
    using static Debug;
    using static GUI;
    using static GUIContent;
    using static GUILayoutUtility;
    using static UnityObject;
    using static ContentStyle;
    using static EditorText;
    using static ScriptableObject;

    internal class AddTransition : IDisposable
    {
        private SerializedTransition SerializedTransition { get; }
        private readonly SerializedObject transition;
        private readonly ReorderableList list;
        private readonly TransitionTableEditor editor;
        private bool toggle;

        internal AddTransition(TransitionTableEditor editorInternal)
        {
            editor = editorInternal;
            transition = new SerializedObject(CreateInstance<TransitionItemSO>());
            SerializedTransition = new SerializedTransition(transition.FindProperty(Item));
            list = new ReorderableList(transition, SerializedTransition.Conditions);
            list.elementHeight *= 2.3f;
            list.drawHeaderCallback += rect => Label(rect, ConditionsProperty);
            list.onAddCallback += l =>
            {
                var count = l.count;
                l.serializedProperty.InsertArrayElementAtIndex(count);
                var prop = l.serializedProperty.GetArrayElementAtIndex(count);
                prop.FindPropertyRelative(Condition).objectReferenceValue = null;
                prop.FindPropertyRelative(ExpectedResult).enumValueIndex = 0;
                prop.FindPropertyRelative(Operator).enumValueIndex = 0;
            };
            list.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var prop = list.serializedProperty.GetArrayElementAtIndex(index);
                rect = new Rect(rect.x, rect.y + 2.5f, rect.width, singleLineHeight);
                var condition = prop.FindPropertyRelative(Condition);
                if (condition.objectReferenceValue != null)
                {
                    var label = condition.objectReferenceValue.name;
                    Label(rect, If);
                    Label(new Rect(rect.x + 20, rect.y, rect.width, rect.height), label, boldLabel);
                    PropertyField(new Rect(rect.x + rect.width - 180, rect.y, 20, rect.height), condition, none);
                }
                else
                {
                    PropertyField(new Rect(rect.x, rect.y, 150, rect.height), condition, none);
                }

                LabelField(new Rect(rect.x + rect.width - 120, rect.y, 20, rect.height), Is);
                PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height),
                    prop.FindPropertyRelative(ExpectedResult), none);
                PropertyField(new Rect(rect.x + 20, rect.y + singleLineHeight + 5, 60, rect.height),
                    prop.FindPropertyRelative(Operator), none);
            };
            list.onChangedCallback += _ => list.serializedProperty.serializedObject.ApplyModifiedProperties();
            list.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
            {
                if (isFocused) DrawRect(rect, Focused);
                DrawRect(rect, index % 2 != 0 ? ZebraDark : ZebraLight);
            };
        }

        // ReSharper disable Unity.PerformanceAnalysis
        internal void Display(Rect position)
        {
            position.x += 8;
            position.width -= 16;
            var rect = position;
            var listHeight = list.GetHeight();
            var singleLineHeight = EditorGUIUtility.singleLineHeight;
            if (!toggle)
            {
                position.height = singleLineHeight;
                GetRect(position.width, position.height);
                if (!Button(position, AddTransitionButton)) return;
                toggle = true;
                SerializedTransition.ClearProperties();
                return;
            }

            position.height = listHeight + singleLineHeight * 4;
            DrawRect(position, LightGray);
            GetRect(position.width, position.height);
            position.y += 10;
            position.x += 20;
            StatePropField(position, From, SerializedTransition.FromState);
            position.x = rect.width / 2 + 20;
            StatePropField(position, To, SerializedTransition.ToState);
            position.y += 30;
            position.x = rect.x + 5;
            position.height = listHeight;
            position.width -= 10;
            list.DoList(position);
            position.y += position.height + 5;
            position.height = singleLineHeight;
            position.width = rect.width / 2 - 20;
            if (Button(position, AddTransitionButton))
            {
                if (SerializedTransition.FromState.objectReferenceValue == null)
                {
                    LogException(new ArgumentNullException(FromStateProperty));
                }
                else if (SerializedTransition.ToState.objectReferenceValue == null)
                {
                    LogException(new ArgumentNullException(ToStateProperty));
                }
                else if (SerializedTransition.FromState.objectReferenceValue ==
                         SerializedTransition.ToState.objectReferenceValue)
                {
                    LogException(new InvalidOperationException(SameStateError));
                }
                else
                {
                    editor.AddTransition(SerializedTransition);
                    toggle = false;
                }
            }

            position.x += rect.width / 2;
            if (Button(position, Cancel)) toggle = false;
        }

        private static void StatePropField(Rect pos, string label, SerializedProperty prop)
        {
            pos.height = singleLineHeight;
            LabelField(pos, label);
            pos.x += 40;
            pos.width /= 4;
            PropertyField(pos, prop, none);
        }

        public void Dispose()
        {
            DestroyImmediate(transition.targetObject);
            transition.Dispose();
            SuppressFinalize(this);
        }
    }
}
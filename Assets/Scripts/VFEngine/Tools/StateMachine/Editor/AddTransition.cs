using System;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;
using VFEngine.Tools.StateMachine.Editor.ScriptableObjects;
using UnityObject = UnityEngine.Object;

// ReSharper disable RedundantLambdaParameterType
// ReSharper disable UnusedParameter.Local
namespace VFEngine.Tools.StateMachine.Editor
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

    internal class AddTransition : IDisposable
    {
        private bool toggle;
        private float reorderableListHeight;
        private Rect transitionRect;
        private static float _singleLineHeight;
        private readonly SerializedObject transition;
        private readonly ReorderableList reorderableList;
        private readonly SerializedTransition serializedTransition;
        private readonly TransitionTableEditor editor;

        internal AddTransition(TransitionTableEditor editorInternal)
        {
            var serializedProperty = default(SerializedProperty);
            editor = editorInternal;
            transition = new SerializedObject(ScriptableObject.CreateInstance<TransitionTableItemSO>());
            serializedTransition = new SerializedTransition(transition.FindProperty(Item));
            reorderableList = new ReorderableList(transition, serializedTransition.Conditions);
            reorderableList.elementHeight *= 2.3f;
            reorderableList.drawHeaderCallback += rect => Label(rect, ConditionsProperty);
            reorderableList.onAddCallback += list =>
            {
                var reorderableListItemsAmount = list.count;
                list.serializedProperty.InsertArrayElementAtIndex(reorderableListItemsAmount);
                serializedProperty = list.serializedProperty.GetArrayElementAtIndex(reorderableListItemsAmount);
                serializedProperty.FindPropertyRelative(Condition).objectReferenceValue = null;
                serializedProperty.FindPropertyRelative(ExpectedResult).enumValueIndex = 0;
                serializedProperty.FindPropertyRelative(Operator).enumValueIndex = 0;
            };
            reorderableList.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                serializedProperty = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                rect = new Rect(rect.x, rect.y + 2.5f, rect.width, _singleLineHeight);
                var transitionCondition = serializedProperty.FindPropertyRelative(Condition);
                if (transitionCondition.objectReferenceValue != null)
                {
                    var transitionConditionLabel = transitionCondition.objectReferenceValue.name;
                    Label(rect, If);
                    Label(new Rect(rect.x + 20, rect.y, rect.width, rect.height), transitionConditionLabel, boldLabel);
                    PropertyField(new Rect(rect.x + rect.width - 180, rect.y, 20, rect.height), transitionCondition,
                        none);
                }
                else
                {
                    PropertyField(new Rect(rect.x, rect.y, 150, rect.height), transitionCondition, none);
                }

                LabelField(new Rect(rect.x + rect.width - 120, rect.y, 20, rect.height), Is);
                PropertyField(new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height),
                    serializedProperty.FindPropertyRelative(ExpectedResult), none);
                PropertyField(new Rect(rect.x + 20, rect.y + _singleLineHeight + 5, 60, rect.height),
                    serializedProperty.FindPropertyRelative(Operator), none);
            };
            reorderableList.onChangedCallback += list =>
                reorderableList.serializedProperty.serializedObject.ApplyModifiedProperties();
            reorderableList.drawElementBackgroundCallback += (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                if (isFocused) DrawRect(rect, Focused);
                DrawRect(rect, index % 2 != 0 ? ZebraDark : ZebraLight);
            };
        }

        [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
        internal void Display(Rect position)
        {
            position.x += 8;
            position.width -= 16;
            transitionRect = position;
            reorderableListHeight = reorderableList.GetHeight();
            _singleLineHeight = singleLineHeight;
            if (!toggle)
            {
                position.height = _singleLineHeight;
                GetRect(position.width, position.height);
                if (!Button(position, AddTransitionButton)) return;
                toggle = true;
                serializedTransition.ClearProperties();
                return;
            }

            position.height = reorderableListHeight + _singleLineHeight * 4;
            DrawRect(position, LightGray);
            GetRect(position.width, position.height);
            position.y += 10;
            position.x += 20;
            StatePropField(position, From, serializedTransition.FromState);
            position.x = transitionRect.width / 2 + 20;
            StatePropField(position, To, serializedTransition.ToState);
            position.y += 30;
            position.x = transitionRect.x + 5;
            position.height = reorderableListHeight;
            position.width -= 10;
            reorderableList.DoList(position);
            position.y += position.height + 5;
            position.height = _singleLineHeight;
            position.width = transitionRect.width / 2 - 20;
            if (Button(position, AddTransitionButton))
            {
                if (serializedTransition.FromState.objectReferenceValue == null)
                {
                    LogException(new ArgumentNullException(FromStateProperty));
                }
                else if (serializedTransition.ToState.objectReferenceValue == null)
                {
                    LogException(new ArgumentNullException(ToStateProperty));
                }
                else if (serializedTransition.FromState.objectReferenceValue ==
                         serializedTransition.ToState.objectReferenceValue)
                {
                    LogException(new InvalidOperationException(SameStateError));
                }
                else
                {
                    editor.AddTransition(serializedTransition);
                    toggle = false;
                }
            }

            position.x += transitionRect.width / 2;
            if (Button(position, Cancel)) toggle = false;
        }

        private static void StatePropField(Rect pos, string label, SerializedProperty prop)
        {
            pos.height = _singleLineHeight;
            LabelField(pos, label);
            pos.x += 40;
            pos.width /= 4;
            PropertyField(pos, prop, none);
        }

        void IDisposable.Dispose()
        {
            DestroyImmediate(transition.targetObject);
            transition.Dispose();
            SuppressFinalize(this);
        }
    }
}
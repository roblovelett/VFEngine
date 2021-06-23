/*using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;
using Object = UnityEngine.Object;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable UnusedParameter.Local
namespace VFEngine.Tools.StateMachine.TransitionTable.ScriptableObjects.Editor.Core.Transition
{
    using static ContentStyle;
    using static EditorGUI;
    using static EditorGUIUtility;
    using static EditorStyles;
    using static GC;
    using static GUI;
    using static GUIContent;
    using static Object;
    using static AddTransition.Text.Label;
    using static AddTransition.Text.Property;
    using static AddTransition.Text.Button;
    using static GUILayoutUtility;
    using static ReorderableList;
    using static ScriptableObject;
    public class AddTransition : IDisposable
    {
        private SerializedTransition SerializedTransition { get; }
        private TransitionTableEditor editor;
        private bool toggle;
        private readonly SerializedObject transition;
        private readonly ReorderableList list;
        private readonly AddTransition helper;

        internal AddTransition(TransitionTableEditor editor)
        {
            helper = this;
            helper.editor = editor;
            transition = new SerializedObject(CreateInstance<TransitionItemSO>());
            SerializedTransition = new SerializedTransition(transition.FindProperty(Item));
            list = new ReorderableList(transition, SerializedTransition.Conditions);
            SetConditionsList(list);

            static void SetConditionsList(ReorderableList reorderableList)
            {
                reorderableList.elementHeight *= 2.3f;
                reorderableList.drawHeaderCallback += DrawHeader();
                reorderableList.onAddCallback += AddList();
                reorderableList.drawElementCallback += DrawList();
                reorderableList.onChangedCallback += ChangeList();
                reorderableList.drawElementBackgroundCallback += DrawBackground();

                static HeaderCallbackDelegate DrawHeader()
                {
                    return headerRect => Label(headerRect, Conditions);
                }

                static AddCallbackDelegate AddList()
                {
                    return list =>
                    {
                        var count = list.count;
                        list.serializedProperty.InsertArrayElementAtIndex(count);
                        var prop = list.serializedProperty.GetArrayElementAtIndex(count);
                        prop.FindPropertyRelative(ConditionProperty).objectReferenceValue = null;
                        prop.FindPropertyRelative(ExpectedResultProperty).enumValueIndex = 0;
                        prop.FindPropertyRelative(OperatorProperty).enumValueIndex = 0;
                    };
                }

                ElementCallbackDelegate DrawList()
                {
                    return (itemRect, index, isActive, isFocused) =>
                    {
                        itemRect = new Rect(itemRect.x, itemRect.y + 2.5f, itemRect.width, singleLineHeight);
                        var property = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                        var condition = property.FindPropertyRelative(ConditionProperty);
                        var drawConditionLabel = condition.objectReferenceValue != null;
                        if (drawConditionLabel)
                        {
                            DrawConditionLabel(itemRect, condition);
                            SetConditionProperty(itemRect, condition);
                        }
                        else
                        {
                            SetNullProperty(itemRect, condition);
                        }

                        DrawIsLabel(itemRect);
                        SetExpectedResultProperty(property, itemRect);
                        SetOperatorProperty(property, itemRect);
                    };

                    static void DrawConditionLabel(Rect rect, SerializedProperty serializedProperty)
                    {
                        var operatorRect = new Rect(rect.x + 20, rect.y, rect.width, rect.height);
                        var label = serializedProperty.objectReferenceValue.name;
                        Label(rect, If);
                        Label(operatorRect, label, boldLabel);
                    }

                    static void SetConditionProperty(Rect rect, SerializedProperty serializedProperty)
                    {
                        var conditionRect = new Rect(rect.x + rect.width - 180, rect.y, 20, rect.height);
                        PropertyField(conditionRect, serializedProperty, none);
                    }

                    static void SetNullProperty(Rect rect, SerializedProperty condition)
                    {
                        var propertyRect = new Rect(rect.x, rect.y, 150, rect.height);
                        PropertyField(propertyRect, condition, none);
                    }

                    static void DrawIsLabel(Rect rect)
                    {
                        var isRect = new Rect(rect.x + rect.width - 120, rect.y, 20, rect.height);
                        LabelField(isRect, Is);
                    }

                    static void SetExpectedResultProperty(SerializedProperty serializedProperty, Rect rect)
                    {
                        var expectedResultProperty = serializedProperty.FindPropertyRelative(ExpectedResultProperty);
                        var expectedResultRect = new Rect(rect.x + rect.width - 60, rect.y, 60, rect.height);
                        PropertyField(expectedResultRect, expectedResultProperty, none);
                    }

                    static void SetOperatorProperty(SerializedProperty serializedProperty, Rect rect)
                    {
                        var operatorProperty = serializedProperty.FindPropertyRelative(OperatorProperty);
                        var operatorRect = new Rect(rect.x + 20, rect.y + singleLineHeight + 5, 60, rect.height);
                        PropertyField(operatorRect, operatorProperty, none);
                    }
                }

                ChangedCallbackDelegate ChangeList()
                {
                    return list => reorderableList.serializedProperty.serializedObject.ApplyModifiedProperties();
                }

                static ElementCallbackDelegate DrawBackground()
                {
                    return (rect, index, isActive, isFocused) =>
                    {
                        if (isFocused) DrawRect(rect, Focused);
                        DrawRect(rect, index % 2 != 0 ? ZebraDark : ZebraLight);
                    };
                }
            }
        }

        internal class TransitionItemSO : ScriptableObject
        {
            //public TransitionData item;
        }

        internal void Display(Rect position)
        {
            var rect = position;
            var listHeight = list.GetHeight();
            var drawAddButton = DrawAddButton();
            SetPosition();
            if (drawAddButton) return;
            DrawBackground();
            SetSpace();
            SetStateFields();
            SetConditionsList();
            SetAddButton();
            SetCancelButton();

            void SetPosition()
            {
                position.x += 8;
                position.width -= 16;
            }

            bool DrawAddButton()
            {
                if (toggle) return false;
                position.height = singleLineHeight;
                GetRect(position.width, position.height);
                if (!Button(position, Text.Button.AddTransition)) return true;
                toggle = true;
                SerializedTransition.ClearProperties();
                return true;
            }

            void DrawBackground()
            {
                position.height = listHeight + singleLineHeight * 4;
                DrawRect(position, LightGray);
            }

            void SetSpace()
            {
                GetRect(position.width, position.height);
            }

            void SetStateFields()
            {
                position.y += 10;
                position.x += 20;
                SetField(position, From, SerializedTransition.FromState);
                position.x = rect.width / 2 + 20;
                SetField(position, To, SerializedTransition.ToState);

                static void SetField(Rect pos, string label, SerializedProperty prop)
                {
                    pos.height = singleLineHeight;
                    LabelField(pos, label);
                    pos.x += 40;
                    pos.width /= 4;
                    PropertyField(pos, prop, none);
                }
            }

            void SetConditionsList()
            {
                position.y += 30;
                position.x = rect.x + 5;
                position.height = listHeight;
                position.width -= 10;
                list.DoList(position);
            }

            void SetAddButton()
            {
                var fromState = SerializedTransition.FromState.objectReferenceValue;
                var toState = SerializedTransition.ToState.objectReferenceValue;
                var noFromState = fromState == null;
                var noToState = toState == null;
                var fromIsToState = fromState == toState;
                var hasErrors = noFromState || noToState || fromIsToState;
                Set();

                void Set()
                {
                    position.y += position.height + 5;
                    position.height = singleLineHeight;
                    position.width = rect.width / 2 - 20;
                    if (!Button(position, Text.Button.AddTransition)) return;
                    if (hasErrors)
                    {
                        GetErrors(noFromState, fromState, noToState, toState);
                    }
                    else
                    {
                        //editor.AddTransition(SerializedTransition);
                        toggle = false;
                    }

                    position.x += rect.width / 2;
                }

                static void GetErrors(bool noFromState, Object fromState, bool noToState, Object toState)
                {
                    //if (noFromState) ArgumentNullError(fromState.name);
                    //else if (noToState) ArgumentNullError(toState.name);
                    //else if (true) SameStateError();
                }
            }

            void SetCancelButton()
            {
                if (Button(position, Cancel)) toggle = false;
            }
        }

        public void Dispose()
        {
            DestroyImmediate(transition.targetObject);
            transition.Dispose();
            SuppressFinalize(helper);
        }

        internal static class Text
        {
            internal static class Label
            {
                internal const string Is = "Is";
                internal const string If = "If";
                internal const string Conditions = "Conditions";
                internal const string From = "From";
                internal const string To = "To";
            }

            internal static class Property
            {
                internal const string ExpectedResultProperty = "ExpectedResult";
                internal const string OperatorProperty = "Operator";
                internal const string ConditionProperty = "Condition";
                internal const string Item = "Item";
            }

            internal static class Button
            {
                internal const string Cancel = "Cancel";
                internal const string AddTransition = "Add Transition";
            }
        }
    }
}*/
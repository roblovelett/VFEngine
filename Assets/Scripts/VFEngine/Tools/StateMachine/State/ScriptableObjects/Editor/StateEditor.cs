using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.ScriptableObjects.TransitionTable.Editor;

// ReSharper disable UnusedParameter.Local

namespace VFEngine.Tools.StateMachine.State.ScriptableObjects.Editor
{
    using static Undo;
    using static GUI;
    using static EditorGUIUtility;
    using static EditorGUI;
    using static EditorStyles;
    using static GUIContent;
    using static ContentStyle;

    //[CustomEditor(typeof(ModelSO))]
    public class StateEditor : UnityEditor.Editor
    {
        private ReorderableList list;
        private SerializedProperty actions;

        private void OnEnable()
        {
            undoRedoPerformed += OnUndo;
            actions = serializedObject.FindProperty("actions");
            list = new ReorderableList(serializedObject, actions, true, true, true, true);
            SetupActionsList(list);
        }

        private void OnDisable()
        {
            undoRedoPerformed -= OnUndo;
        }

        public override void OnInspectorGUI()
        {
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnUndo()
        {
            serializedObject.UpdateIfRequiredOrScript();
        }

        private static void SetupActionsList(ReorderableList actionList)
        {
            actionList.elementHeight *= 1.5f;
            actionList.drawHeaderCallback += rect => Label(rect, "Actions");
            actionList.onAddCallback += list =>
            {
                var action = list.serializedProperty;
                var actionsAmount = list.count;
                action.InsertArrayElementAtIndex(actionsAmount);
                var lastAction = action.GetArrayElementAtIndex(actionsAmount);
                lastAction.objectReferenceValue = null;
            };
            actionList.drawElementCallback += (rectangle, index, isActive, isFocused) =>
            {
                var rect = rectangle;
                rect.height = singleLineHeight;
                rect.y += 5;
                rect.x += 5;
                var action = actionList.serializedProperty;
                var currentAction = action.GetArrayElementAtIndex(index);
                var currentActionNotNull = currentAction != null;
                if (currentActionNotNull)
                {
                    var currentActionLabel = currentAction.objectReferenceValue.name;
                    rect.width = 35;
                    PropertyField(rectangle, currentAction, none);
                    rect.width = rectangle.width - 50;
                    rect.x += 42;
                    Label(rect, currentActionLabel, boldLabel);
                }
                else
                {
                    PropertyField(rectangle, null, none);
                }
            };
            actionList.onChangedCallback += list =>
            {
                var actionObject = list.serializedProperty.serializedObject;
                actionObject.ApplyModifiedProperties();
            };
            actionList.drawElementBackgroundCallback += (rectangle, index, isActive, isFocused) =>
            {
                if (isFocused) DrawRect(rectangle, Focused);
                var onOddItem = index % 2 != 0;
                DrawRect(rectangle, onOddItem ? ZebraDark : ZebraLight);
            };
        }
    }
}
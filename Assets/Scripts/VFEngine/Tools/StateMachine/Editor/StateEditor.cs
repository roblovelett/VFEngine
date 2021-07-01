using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VFEngine.Tools.StateMachine.Editor.Data;
using VFEngine.Tools.StateMachine.ScriptableObjects;
using EditorUnity = UnityEditor.Editor;

namespace VFEngine.Tools.StateMachine.Editor
{
    using static EditorGUI;
    using static EditorGUIUtility;
    using static EditorStyles;
    using static Undo;
    using static GUI;
    using static GUIContent;
    using static ContentStyle;
    using static EditorText;

    [CustomEditor(typeof(StateSO))]
    internal class StateEditor : EditorUnity
    {
        private ReorderableList reorderableList;

        // ReSharper disable UnusedParameter.Local
        private void OnEnable()
        {
            undoRedoPerformed += DoUndo;
            SerializedProperty reorderableListSerializedProperty;
            var actions = serializedObject.FindProperty(ActionsProperty);
            reorderableList = new ReorderableList(serializedObject, actions, true, true, true, true);
            reorderableList.elementHeight *= 1.5f;
            reorderableList.drawHeaderCallback += rect => Label(rect, Actions);
            reorderableList.onAddCallback += list =>
            {
                var reorderableListItemsAmount = list.count;
                list.serializedProperty.InsertArrayElementAtIndex(reorderableListItemsAmount);
                reorderableListSerializedProperty =
                    list.serializedProperty.GetArrayElementAtIndex(reorderableListItemsAmount);
                reorderableListSerializedProperty.objectReferenceValue = null;
            };
            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) =>
            {
                var reorderableListRect = rect;
                reorderableListRect.height = singleLineHeight;
                reorderableListRect.y += 5;
                reorderableListRect.x += 5;
                reorderableListSerializedProperty = reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                if (reorderableListSerializedProperty.objectReferenceValue != null)
                {
                    var reorderableListLabel = reorderableListSerializedProperty.objectReferenceValue.name;
                    reorderableListRect.width = 35;
                    PropertyField(reorderableListRect, reorderableListSerializedProperty, none);
                    reorderableListRect.width = rect.width - 50;
                    reorderableListRect.x += 42;
                    Label(reorderableListRect, reorderableListLabel, boldLabel);
                }
                else
                {
                    PropertyField(reorderableListRect, reorderableListSerializedProperty, none);
                }
            };
            reorderableList.onChangedCallback +=
                list => list.serializedProperty.serializedObject.ApplyModifiedProperties();
            reorderableList.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
            {
                if (isFocused) DrawRect(rect, Focused);
                DrawRect(rect, index % 2 != 0 ? ZebraDark : ZebraLight);
            };
        }

        private void OnDisable()
        {
            undoRedoPerformed -= DoUndo;
        }

        public override void OnInspectorGUI()
        {
            reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void DoUndo()
        {
            serializedObject.UpdateIfRequiredOrScript();
        }
    }
}
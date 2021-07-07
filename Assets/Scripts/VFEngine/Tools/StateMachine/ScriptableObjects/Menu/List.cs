using UnityEditor;
using UnityEditorInternal;

// ReSharper disable UnusedParameter.Local
namespace VFEngine.Tools.StateMachine.ScriptableObjects.Menu
{
    using static EditorGUI;
    using static ContentStyle;

    internal static class List
    {
        internal static void OnChangedCallback(ref ReorderableList list)
        {
            list.onChangedCallback += l => l.serializedProperty.serializedObject.ApplyModifiedProperties();
            list.drawElementBackgroundCallback += (rect, index, isActive, isFocused) =>
            {
                if (isFocused) DrawRect(rect, Focused);
                DrawRect(rect, index % 2 != 0 ? ZebraDark : ZebraLight);
            };
        }

        internal static void OnAddCallBack(ref ReorderableList list, out SerializedProperty prop)
        {
            var count = list.count;
            list.serializedProperty.InsertArrayElementAtIndex(count);
            prop = list.serializedProperty.GetArrayElementAtIndex(count);
        }
    }
}
using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable UnusedType.Global
namespace VFEngine.Tools.BetterEvent.Editor
{
    public class BetterEventDrawer : OdinValueDrawer<BetterEventEntry>
    {
        private Object tmpTarget;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.BeginBox();
            {
                SirenixEditorGUI.BeginToolbarBoxHeader();
                {
                    var rect = GUILayoutUtility.GetRect(0, 19);
                    var unityObjectFieldRect = rect.Padding(2).AlignLeft(rect.width / 2);
                    var methodSelectorRect = rect.Padding(2).AlignRight(rect.width / 2 - 5);
                    var dInfo = GetDelegateInfo();
                    EditorGUI.BeginChangeCheck();
                    var newTarget =
                        SirenixEditorFields.UnityObjectField(unityObjectFieldRect, dInfo.target, typeof(Object), true);
                    if (EditorGUI.EndChangeCheck()) tmpTarget = newTarget;
                    EditorGUI.BeginChangeCheck();
                    var selectorText = dInfo.method == null || tmpTarget ? "Select a method" : dInfo.method.Name;
                    var newMethod =
                        MethodSelector.DrawSelectorDropdown(methodSelectorRect, selectorText, CreateSelector);
                    if (EditorGUI.EndChangeCheck())
                    {
                        CreateAndAssignNewDelegate(newMethod.FirstOrDefault());
                        tmpTarget = null;
                    }
                }
                SirenixEditorGUI.EndToolbarBoxHeader();

                // Draws the rest of the ICustomEvent, and since we've drawn the label, we simply pass along null.
                foreach (var child in Property.Children)
                {
                    if (child.Name == "Result") continue;
                    child.Draw();
                }
            }
            SirenixEditorGUI.EndBox();
        }

        private void CreateAndAssignNewDelegate(DelegateInfo delInfo)
        {
            var method = delInfo.method;
            var target = delInfo.target;
            var pTypes = method.GetParameters().Select(x => x.ParameterType).ToArray();
            var args = new object[pTypes.Length];
            Type delegateType = null;
            if (method.ReturnType == typeof(void))
            {
                switch (args.Length)
                {
                    case 0:
                        delegateType = typeof(Action);
                        break;
                    case 1:
                        delegateType = typeof(Action<>).MakeGenericType(pTypes);
                        break;
                    case 2:
                        delegateType = typeof(Action<,>).MakeGenericType(pTypes);
                        break;
                    case 3:
                        delegateType = typeof(Action<,,>).MakeGenericType(pTypes);
                        break;
                    case 4:
                        delegateType = typeof(Action<,,,>).MakeGenericType(pTypes);
                        break;
                    case 5:
                        delegateType = typeof(Action<,,,,>).MakeGenericType(pTypes);
                        break;
                }
            }
            else
            {
                pTypes = pTypes.Append(method.ReturnType).ToArray();
                switch (args.Length)
                {
                    case 0:
                        delegateType = typeof(Func<>).MakeArrayType();
                        break;
                    case 1:
                        delegateType = typeof(Func<,>).MakeGenericType(pTypes);
                        break;
                    case 2:
                        delegateType = typeof(Func<,,>).MakeGenericType(pTypes);
                        break;
                    case 3:
                        delegateType = typeof(Func<,,,>).MakeGenericType(pTypes);
                        break;
                    case 4:
                        delegateType = typeof(Func<,,,,>).MakeGenericType(pTypes);
                        break;
                    case 5:
                        delegateType = typeof(Func<,,,,,>).MakeGenericType(pTypes);
                        break;
                }
            }

            if (delegateType == null)
            {
                Debug.LogError("Unsupported Method Type");
                return;
            }

            var del = Delegate.CreateDelegate(delegateType, target, method);
            Property.Tree.DelayActionUntilRepaint(() =>
            {
                ValueEntry.WeakSmartValue = new BetterEventEntry(del);
                GUI.changed = true;
                Property.RefreshSetup();
            });
        }

        private DelegateInfo GetDelegateInfo()
        {
            var value = ValueEntry.SmartValue;
            var del = value.@delegate;
            var methodInfo = del?.Method;
            Object target = null;
            if (tmpTarget) target = tmpTarget;
            else if (del != null) target = del.Target as Object;
            return new DelegateInfo {target = target, method = methodInfo};
        }

        private OdinSelector<DelegateInfo> CreateSelector(Rect arg)
        {
            arg.xMin -= arg.width;
            var info = GetDelegateInfo();
            var sel = new MethodSelector(info.target);
            sel.SetSelection(info);
            sel.ShowInPopup(arg);
            return sel;
        }
    }
}
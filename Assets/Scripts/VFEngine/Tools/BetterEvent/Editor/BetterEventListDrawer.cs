using Sirenix.OdinInspector.Editor;
using UnityEngine;

// ReSharper disable UnusedType.Global
namespace VFEngine.Tools.BetterEvent.Editor
{
    public class BetterEventListDrawer : OdinValueDrawer<BetterEvent>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            Property.Children["events"].Draw(label);
        }
    }
}
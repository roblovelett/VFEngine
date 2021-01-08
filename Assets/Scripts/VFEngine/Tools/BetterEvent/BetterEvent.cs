using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;

namespace VFEngine.Tools.BetterEvent
{
    [Serializable]
    public struct BetterEvent
    {
        [HideReferenceObjectPicker]
        [ListDrawerSettings(CustomAddFunction = "GetDefaultBetterEvent", OnTitleBarGUI = "DrawInvokeButton")]
        public List<BetterEventEntry> events;

        public void Invoke()
        {
            if (events == null) return;
            foreach (var t in events) t.Invoke();
        }

#if UNITY_EDITOR

        private BetterEventEntry GetDefaultBetterEvent()
        {
            return new BetterEventEntry(null);
        }

        private void DrawInvokeButton()
        {
            if (SirenixEditorGUI.ToolbarButton("Invoke")) Invoke();
        }

#endif
    }
}
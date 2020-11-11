using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PlatformerSettings", menuName = PlatformerSettingsPath, order = 0)]
    [InlineEditor]
    public class PlatformerSettings : ScriptableObject
    {
        #region properties

        [LabelText("Display Warnings")] [SerializeField]
        public bool displayWarningsControl = true;

        #endregion
    }
}
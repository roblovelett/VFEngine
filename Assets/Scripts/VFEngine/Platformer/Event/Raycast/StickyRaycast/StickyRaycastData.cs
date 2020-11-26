using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static RaycastData;

    public class StickyRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private StickyRaycastSettings settings;

        #endregion

        private const string ModelAssetPath = "StickyRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public StickyRaycastSettings Settings => settings;
        public bool DisplayWarningsControl => settings.displayWarningsControl;
        public float StickToSlopesOffsetY => settings.stickToSlopesOffsetY;

        #endregion

        public bool IsCastingLeft { get; set; }
        public float StickyRaycastLength { get; set; }
        public static string StickyRaycastPath { get; } = $"{RaycastPath}StickyRaycast/";

        public static readonly string StickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{StickyRaycastPath}{ModelAssetPath}";

        #endregion
    }
}
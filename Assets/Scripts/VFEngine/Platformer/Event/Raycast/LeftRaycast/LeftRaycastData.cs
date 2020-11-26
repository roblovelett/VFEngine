using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class LeftRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string LeftRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{LeftRaycastPath}LeftRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public float LeftRayLength { get; set; }
        public Vector2 LeftRaycastFromBottomOrigin { get; set; }
        public Vector2 LeftRaycastToTopOrigin { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }
        public Vector2 CurrentLeftRaycastOrigin { get; set; }
        public static readonly string LeftRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
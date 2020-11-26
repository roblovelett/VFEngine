using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class DownRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string DownRaycastPath = $"{RaycastPath}DownRaycast/";
        private static readonly string ModelAssetPath = $"{DownRaycastPath}DownRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public float DownRayLength { get; set; }
        public Vector2 CurrentDownRaycastOrigin { get; set; }
        public Vector2 DownRaycastFromLeft { get; set; }
        public Vector2 DownRaycastToRight { get; set; }
        public RaycastHit2D CurrentDownRaycastHit { get; set; }
        public static readonly string DownRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
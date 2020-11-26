using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class DistanceToGroundRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string DistanceToGroundRaycastPath = $"{RaycastPath}DistanceToGroundRaycast/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastPath}DistanceToGroundRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        public Vector2 DistanceToGroundRaycastOrigin { get; set; } = Vector2.zero;

        public static readonly string DistanceToGroundRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
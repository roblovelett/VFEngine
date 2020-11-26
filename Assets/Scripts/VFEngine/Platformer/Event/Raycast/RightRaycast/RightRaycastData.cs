using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class RightRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string RightRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{RightRaycastPath}RightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public float RightRayLength { get; set; }
        public Vector2 RightRaycastFromBottomOrigin { get; set; }
        public Vector2 RightRaycastToTopOrigin { get; set; }
        public RaycastHit2D CurrentRightRaycastHit { get; set; }
        public Vector2 CurrentRightRaycastOrigin { get; set; }
        public static readonly string RightRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
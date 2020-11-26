using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class UpRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string UpRaycastPath = $"{RaycastPath}UpRaycast/";
        private static readonly string ModelAssetPath = $"{UpRaycastPath}UpRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

         public float UpRayLength { get; set; }
        public Vector2 UpRaycastStart { get; set; } = Vector2.zero;
        public Vector2 UpRaycastEnd { get; set; } = Vector2.zero;
        public float UpRaycastSmallestDistance { get; set; }
        public Vector2 CurrentUpRaycastOrigin { get; set; }
        public RaycastHit2D CurrentUpRaycastHit { get; set; }
        public static readonly string UpRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
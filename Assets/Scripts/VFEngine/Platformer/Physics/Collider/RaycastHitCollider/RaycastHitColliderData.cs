using UnityEngine;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Tools;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    public class RaycastHitColliderData
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string ModelAssetPath = $"{RaycastHitColliderPath}RaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }
        public RaycastHitColliderController Controller { get; set; }
        public RightRaycastRuntimeData RightRaycastRuntimeData { get; set; }
        public LeftRaycastRuntimeData LeftRaycastRuntimeData { get; set; }
        public RaycastHit2D CurrentRightRaycastHit { get; set; }
        public RaycastHit2D CurrentLeftRaycastHit { get; set; }

        #endregion

        public RaycastHitColliderRuntimeData RuntimeData { get; set; }
        public RaycastHitColliderContactList ContactList { get; set; }
        public Collider2D IgnoredCollider { get; set; }
        public const string RaycastHitColliderPath = "Physics/Collider/RaycastHitCollider/";

        public static readonly string RaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
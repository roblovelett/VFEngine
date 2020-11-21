using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "RightRaycastModel", menuName = PlatformerRightRaycastModelPath, order = 0)]
    [InlineEditor]
    public class RightRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Right Raycast Data")] [SerializeField]
        private RightRaycastData r = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            r.RuntimeData = r.Character.GetComponentNoAllocation<RaycastController>().RightRaycastRuntimeData;
            r.RuntimeData.SetRightRaycast(r.RightRayLength, r.RightRaycastFromBottomOrigin, r.RightRaycastToTopOrigin,
                r.CurrentRightRaycastHit);
        }

        private void InitializeModel()
        {
            r.PlatformerRuntimeData = r.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            r.RaycastRuntimeData = r.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            r.RightRaycastHitColliderRuntimeData = r.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .RightRaycastHitColliderRuntimeData;
            r.PhysicsRuntimeData = r.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            r.LayerMaskRuntimeData = r.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            r.Transform = r.PlatformerRuntimeData.platformer.Transform;
            r.DrawRaycastGizmosControl = r.RaycastRuntimeData.raycast.DrawRaycastGizmosControl;
            r.NumberOfHorizontalRaysPerSide = r.RaycastRuntimeData.raycast.NumberOfHorizontalRaysPerSide;
            r.RayOffset = r.RaycastRuntimeData.raycast.RayOffset;
            r.ObstacleHeightTolerance = r.RaycastRuntimeData.raycast.ObstacleHeightTolerance;
            r.BoundsWidth = r.RaycastRuntimeData.raycast.BoundsWidth;
            r.BoundsBottomLeftCorner = r.RaycastRuntimeData.raycast.BoundsBottomLeftCorner;
            r.BoundsBottomRightCorner = r.RaycastRuntimeData.raycast.BoundsBottomRightCorner;
            r.BoundsTopLeftCorner = r.RaycastRuntimeData.raycast.BoundsTopLeftCorner;
            r.BoundsTopRightCorner = r.RaycastRuntimeData.raycast.BoundsTopRightCorner;
            r.CurrentRightHitsStorageIndex = r.RightRaycastHitColliderRuntimeData.rightRaycastHitCollider
                .CurrentRightHitsStorageIndex;
            r.Speed = r.PhysicsRuntimeData.physics.Speed;
            r.PlatformMask = r.LayerMaskRuntimeData.layerMask.PlatformMask;
            r.OneWayPlatformMask = r.LayerMaskRuntimeData.layerMask.OneWayPlatformMask;
            r.MovingOneWayPlatformMask = r.LayerMaskRuntimeData.layerMask.MovingOneWayPlatformMask;
        }

        private void SetRightRaycastFromBottomOrigin()
        {
            r.RightRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(r.BoundsBottomRightCorner,
                r.BoundsBottomLeftCorner, r.Transform, r.ObstacleHeightTolerance);
        }

        private void SetRightRaycastToTopOrigin()
        {
            r.RightRaycastToTopOrigin = OnSetRaycastToTopOrigin(r.BoundsTopLeftCorner, r.BoundsTopRightCorner,
                r.Transform, r.ObstacleHeightTolerance);
        }

        private void InitializeRightRaycastLength()
        {
            r.RightRayLength = OnSetHorizontalRayLength(r.Speed.x, r.BoundsWidth, r.RayOffset);
        }

        private void SetCurrentRightRaycastOrigin()
        {
            r.CurrentRightRaycastOrigin = OnSetCurrentRaycastOrigin(r.RightRaycastFromBottomOrigin,
                r.RightRaycastToTopOrigin, r.CurrentRightHitsStorageIndex, r.NumberOfHorizontalRaysPerSide);
        }

        private void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            r.CurrentRightRaycastHit = Raycast(r.CurrentRightRaycastOrigin, r.Transform.right, r.RightRayLength,
                r.PlatformMask, red, r.DrawRaycastGizmosControl);
        }

        private void SetCurrentRightRaycast()
        {
            r.CurrentRightRaycastHit = Raycast(r.CurrentRightRaycastOrigin, r.Transform.right, r.RightRayLength,
                r.PlatformMask & ~r.OneWayPlatformMask & ~r.MovingOneWayPlatformMask, red, r.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnSetRightRaycastFromBottomOrigin()
        {
            SetRightRaycastFromBottomOrigin();
        }

        public void OnSetRightRaycastToTopOrigin()
        {
            SetRightRaycastToTopOrigin();
        }

        public void OnInitializeRightRaycastLength()
        {
            InitializeRightRaycastLength();
        }

        public void OnSetCurrentRightRaycastOrigin()
        {
            SetCurrentRightRaycastOrigin();
        }

        public void OnSetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentRightRaycast()
        {
            SetCurrentRightRaycast();
        }

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

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

        [SerializeField] private GameObject character;
        private RightRaycastData r;

        #endregion

        #region private methods

        private void InitializeData()
        {
            r = new RightRaycastData {Character = character};
            r.RuntimeData = RightRaycastRuntimeData.CreateInstance(r.RightRayLength, r.RightRaycastFromBottomOrigin,
                r.RightRaycastToTopOrigin, r.CurrentRightRaycastHit);
        }

        private void InitializeModel()
        {
            r.PhysicsRuntimeData = r.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            r.RaycastRuntimeData = r.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            r.RightRaycastHitColliderRuntimeData = r.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .RightRaycastHitColliderRuntimeData;
            r.LayerMaskRuntimeData = r.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            r.Transform = r.PhysicsRuntimeData.Transform;
            r.Speed = r.PhysicsRuntimeData.Speed;
            r.DrawRaycastGizmosControl = r.RaycastRuntimeData.DrawRaycastGizmosControl;
            r.NumberOfHorizontalRaysPerSide = r.RaycastRuntimeData.NumberOfHorizontalRaysPerSide;
            r.RayOffset = r.RaycastRuntimeData.RayOffset;
            r.ObstacleHeightTolerance = r.RaycastRuntimeData.ObstacleHeightTolerance;
            r.BoundsWidth = r.RaycastRuntimeData.BoundsWidth;
            r.BoundsBottomLeftCorner = r.RaycastRuntimeData.BoundsBottomLeftCorner;
            r.BoundsBottomRightCorner = r.RaycastRuntimeData.BoundsBottomRightCorner;
            r.BoundsTopLeftCorner = r.RaycastRuntimeData.BoundsTopLeftCorner;
            r.BoundsTopRightCorner = r.RaycastRuntimeData.BoundsTopRightCorner;
            r.CurrentRightHitsStorageIndex = r.RightRaycastHitColliderRuntimeData.CurrentRightHitsStorageIndex;
            r.PlatformMask = r.LayerMaskRuntimeData.PlatformMask;
            r.OneWayPlatformMask = r.LayerMaskRuntimeData.OneWayPlatformMask;
            r.MovingOneWayPlatformMask = r.LayerMaskRuntimeData.MovingOneWayPlatformMask;
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

        public RightRaycastRuntimeData RuntimeData => r.RuntimeData;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

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

        #endregion

        #endregion
    }
}
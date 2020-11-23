using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "LeftRaycastModel", menuName = PlatformerLeftRaycastModelPath, order = 0)]
    [InlineEditor]
    public class LeftRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private LeftRaycastData l;

        #endregion

        #region private methods

        private void InitializeData()
        {
            l = new LeftRaycastData {Character = character};
            l.RuntimeData = LeftRaycastRuntimeData.CreateInstance(l.LeftRayLength, l.LeftRaycastFromBottomOrigin,
                l.LeftRaycastToTopOrigin, l.CurrentLeftRaycastHit);
        }

        private void InitializeModel()
        {
            l.PhysicsRuntimeData = l.Character.GetComponentNoAllocation<PhysicsController>().PhysicsRuntimeData;
            l.RaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().RaycastRuntimeData;
            l.LeftRaycastHitColliderRuntimeData = l.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .LeftRaycastHitColliderRuntimeData;
            l.LayerMaskRuntimeData = l.Character.GetComponentNoAllocation<LayerMaskController>().LayerMaskRuntimeData;
            l.Transform = l.PhysicsRuntimeData.Transform;
            l.DrawRaycastGizmosControl = l.RaycastRuntimeData.DrawRaycastGizmosControl;
            l.NumberOfHorizontalRaysPerSide = l.RaycastRuntimeData.NumberOfHorizontalRaysPerSide;
            l.RayOffset = l.RaycastRuntimeData.RayOffset;
            l.ObstacleHeightTolerance = l.RaycastRuntimeData.ObstacleHeightTolerance;
            l.BoundsWidth = l.RaycastRuntimeData.BoundsWidth;
            l.BoundsBottomLeftCorner = l.RaycastRuntimeData.BoundsBottomLeftCorner;
            l.BoundsBottomRightCorner = l.RaycastRuntimeData.BoundsBottomRightCorner;
            l.BoundsTopLeftCorner = l.RaycastRuntimeData.BoundsTopLeftCorner;
            l.BoundsTopRightCorner = l.RaycastRuntimeData.BoundsTopRightCorner;
            l.CurrentLeftHitsStorageIndex = l.LeftRaycastHitColliderRuntimeData.CurrentLeftHitsStorageIndex;
            l.Speed = l.PhysicsRuntimeData.Speed;
            l.PlatformMask = l.LayerMaskRuntimeData.PlatformMask;
            l.OneWayPlatformMask = l.LayerMaskRuntimeData.OneWayPlatformMask;
            l.MovingOneWayPlatformMask = l.LayerMaskRuntimeData.MovingOneWayPlatformMask;
        }

        private void SetLeftRaycastFromBottomOrigin()
        {
            l.LeftRaycastFromBottomOrigin = OnSetRaycastFromBottomOrigin(l.BoundsBottomRightCorner,
                l.BoundsBottomLeftCorner, l.Transform, l.ObstacleHeightTolerance);
        }

        private void SetLeftRaycastToTopOrigin()
        {
            l.LeftRaycastToTopOrigin = OnSetRaycastToTopOrigin(l.BoundsTopLeftCorner, l.BoundsTopRightCorner,
                l.Transform, l.ObstacleHeightTolerance);
        }

        private void SetCurrentLeftRaycastOrigin()
        {
            l.CurrentLeftRaycastOrigin = OnSetCurrentRaycastOrigin(l.LeftRaycastFromBottomOrigin,
                l.LeftRaycastToTopOrigin, l.CurrentLeftHitsStorageIndex, l.NumberOfHorizontalRaysPerSide);
        }

        private void InitializeLeftRaycastLength()
        {
            l.LeftRayLength = OnSetHorizontalRayLength(l.Speed.x, l.BoundsWidth, l.RayOffset);
        }

        private void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -l.Transform.right, l.LeftRayLength,
                l.PlatformMask, red, l.DrawRaycastGizmosControl);
        }

        private void SetCurrentLeftRaycast()
        {
            l.CurrentLeftRaycastHit = Raycast(l.CurrentLeftRaycastOrigin, -l.Transform.right, l.LeftRayLength,
                l.PlatformMask & ~l.OneWayPlatformMask & ~l.MovingOneWayPlatformMask, red, l.DrawRaycastGizmosControl);
        }

        #endregion

        #endregion

        #region properties

        public LeftRaycastRuntimeData RuntimeData => l.RuntimeData;

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

        public void OnSetLeftRaycastFromBottomOrigin()
        {
            SetLeftRaycastFromBottomOrigin();
        }

        public void OnSetLeftRaycastToTopOrigin()
        {
            SetLeftRaycastToTopOrigin();
        }

        public void OnInitializeLeftRaycastLength()
        {
            InitializeLeftRaycastLength();
        }

        public void OnSetCurrentLeftRaycastOrigin()
        {
            SetCurrentLeftRaycastOrigin();
        }

        public void OnSetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentLeftRaycast()
        {
            SetCurrentLeftRaycast();
        }

        #endregion

        #endregion
    }
}
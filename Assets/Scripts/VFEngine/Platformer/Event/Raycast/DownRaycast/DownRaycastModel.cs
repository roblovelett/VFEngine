using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static RaycastModel;
    using static DebugExtensions;
    using static Color;
    using static Mathf;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [CreateAssetMenu(fileName = "DownRaycastModel", menuName = PlatformerDownRaycastModelPath, order = 0)]
    [InlineEditor]
    public class DownRaycastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private DownRaycastData d;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            d = new DownRaycastData {Character = character};
            d.RuntimeData = DownRaycastRuntimeData.CreateInstance(d.DownRayLength, d.CurrentDownRaycastOrigin,
                d.DownRaycastFromLeft, d.DownRaycastToRight, d.CurrentDownRaycastHit);
        }

        private void InitializeModel()
        {
            d.PhysicsRuntimeData = d.Character.GetComponentNoAllocation<PhysicsController>().RuntimeData;
            d.RaycastRuntimeData = d.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            d.DownRaycastHitColliderRuntimeData = d.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .DownRaycastHitColliderRuntimeData;
            d.LayerMaskRuntimeData = d.Character.GetComponentNoAllocation<LayerMaskController>().RuntimeData;
            d.Transform = d.PhysicsRuntimeData.Transform;
            d.NewPosition = d.PhysicsRuntimeData.NewPosition;
            d.DrawRaycastGizmosControl = d.RaycastRuntimeData.DrawRaycastGizmosControl;
            d.NumberOfVerticalRaysPerSide = d.RaycastRuntimeData.NumberOfVerticalRaysPerSide;
            d.RayOffset = d.RaycastRuntimeData.RayOffset;
            d.BoundsHeight = d.RaycastRuntimeData.BoundsHeight;
            d.BoundsBottomLeftCorner = d.RaycastRuntimeData.BoundsBottomLeftCorner;
            d.BoundsBottomRightCorner = d.RaycastRuntimeData.BoundsBottomRightCorner;
            d.BoundsTopLeftCorner = d.RaycastRuntimeData.BoundsTopLeftCorner;
            d.BoundsTopRightCorner = d.RaycastRuntimeData.BoundsTopRightCorner;
            d.CurrentDownHitsStorageIndex = d.DownRaycastHitColliderRuntimeData.CurrentDownHitsStorageIndex;
            d.RaysBelowLayerMaskPlatformsWithoutOneWay =
                d.LayerMaskRuntimeData.RaysBelowLayerMaskPlatformsWithoutOneWay;
            d.RaysBelowLayerMaskPlatforms = d.LayerMaskRuntimeData.RaysBelowLayerMaskPlatforms;
        }

        private void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -d.Transform.up, d.DownRayLength,
                d.RaysBelowLayerMaskPlatformsWithoutOneWay, blue, d.DrawRaycastGizmosControl);
        }

        private void SetCurrentDownRaycast()
        {
            d.CurrentDownRaycastHit = Raycast(d.CurrentDownRaycastOrigin, -d.Transform.up, d.DownRayLength,
                d.RaysBelowLayerMaskPlatforms, blue, d.DrawRaycastGizmosControl);
        }

        private void InitializeDownRayLength()
        {
            d.DownRayLength = d.BoundsHeight / 2 + d.RayOffset;
        }

        private void DoubleDownRayLength()
        {
            d.DownRayLength *= 2;
        }

        private void SetDownRayLengthToVerticalNewPosition()
        {
            d.DownRayLength += Abs(d.NewPosition.y);
        }

        private void SetDownRaycastFromLeft()
        {
            d.DownRaycastFromLeft = OnSetVerticalRaycast(d.BoundsBottomLeftCorner, d.BoundsTopLeftCorner, d.Transform,
                d.RayOffset, d.NewPosition.x);
        }

        private void SetDownRaycastToRight()
        {
            d.DownRaycastToRight = OnSetVerticalRaycast(d.BoundsBottomRightCorner, d.BoundsTopRightCorner, d.Transform,
                d.RayOffset, d.NewPosition.x);
        }

        private void SetCurrentDownRaycastOriginPoint()
        {
            d.CurrentDownRaycastOrigin = OnSetCurrentRaycastOrigin(d.DownRaycastFromLeft, d.DownRaycastToRight,
                d.CurrentDownHitsStorageIndex, d.NumberOfVerticalRaysPerSide);
        }

        #endregion

        #endregion

        #region properties

        public DownRaycastRuntimeData RuntimeData => d.RuntimeData;

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

        public void OnSetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            SetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void OnSetCurrentDownRaycast()
        {
            SetCurrentDownRaycast();
        }

        public void OnInitializeDownRayLength()
        {
            InitializeDownRayLength();
        }

        public void OnDoubleDownRayLength()
        {
            DoubleDownRayLength();
        }

        public void OnSetDownRayLengthToVerticalNewPosition()
        {
            SetDownRayLengthToVerticalNewPosition();
        }

        public void OnSetDownRaycastFromLeft()
        {
            SetDownRaycastFromLeft();
        }

        public void OnSetDownRaycastToRight()
        {
            SetDownRaycastToRight();
        }

        public void OnSetCurrentDownRaycastOriginPoint()
        {
            SetCurrentDownRaycastOriginPoint();
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
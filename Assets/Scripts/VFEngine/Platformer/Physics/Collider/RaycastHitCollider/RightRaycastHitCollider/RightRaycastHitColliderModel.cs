using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    using static RaycastHitColliderModel;
    using static UniTaskExtensions;
    using static MathsExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RightRaycastHitColliderModel", menuName = PlatformerRightRaycastHitColliderModelPath,
        order = 0)]
    [InlineEditor]
    public class RightRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Right Raycast Hit Collider Data")] [SerializeField]
        private RightRaycastHitColliderData r = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            r.RuntimeData = r.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .RightRaycastHitColliderRuntimeData;
            r.RightHitsStorageLength = r.RightHitsStorage.Length;
            r.RuntimeData.SetRightRaycastHitCollider(r.RightHitConnected, r.IsCollidingRight, r.RightHitsStorageLength,
                r.CurrentRightHitsStorageIndex, r.CurrentRightHitAngle, r.CurrentRightHitDistance,
                r.DistanceBetweenRightHitAndRaycastOrigin, r.CurrentRightHitCollider);
        }

        private void InitializeModel()
        {
            r.PlatformerRuntimeData = r.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            r.RaycastRuntimeData = r.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            r.RightRaycastRuntimeData =
                r.Character.GetComponentNoAllocation<RaycastController>().RightRaycastRuntimeData;
            r.Transform = r.PlatformerRuntimeData.platformer.Transform;
            r.NumberOfHorizontalRaysPerSide = r.RaycastRuntimeData.raycast.NumberOfHorizontalRaysPerSide;
            r.CurrentRightRaycastHit = r.RightRaycastRuntimeData.rightRaycast.CurrentRightRaycastHit;
            r.RightRaycastFromBottomOrigin = r.RightRaycastRuntimeData.rightRaycast.RightRaycastFromBottomOrigin;
            r.RightRaycastToTopOrigin = r.RightRaycastRuntimeData.rightRaycast.RightRaycastToTopOrigin;
            InitializeRightHitsStorage();
            InitializeCurrentRightHitsStorageIndex();
            ResetState();
        }

        private void InitializeRightHitsStorage()
        {
            r.RightHitsStorage = new RaycastHit2D[r.NumberOfHorizontalRaysPerSide];
        }

        private void InitializeCurrentRightHitsStorageIndex()
        {
            r.CurrentRightHitsStorageIndex = 0;
        }

        private void SetRightRaycastHitConnected()
        {
            r.RightHitConnected = true;
        }

        private void SetRightDistanceToRightCollider()
        {
            r.DistanceToRightCollider = r.CurrentRightHitAngle;
        }

        private void SetRightRaycastHitMissed()
        {
            r.RightHitConnected = false;
        }

        private void SetCurrentRightHitsStorage()
        {
            r.RightHitsStorage[r.CurrentRightHitsStorageIndex] = r.CurrentRightRaycastHit;
        }

        private void SetCurrentRightHitAngle()
        {
            r.CurrentRightHitAngle =
                OnSetRaycastHitAngle(r.RightHitsStorage[r.CurrentRightHitsStorageIndex].normal, r.Transform);
        }

        private void SetIsCollidingRight()
        {
            r.IsCollidingRight = true;
        }

        private void SetRightCurrentWallCollider()
        {
            r.CurrentRightWallCollider = r.CurrentRightHitCollider.gameObject;
        }

        private void SetCurrentWallColliderNull()
        {
            r.CurrentRightWallCollider = null;
        }

        private void AddToCurrentRightHitsStorageIndex()
        {
            r.CurrentRightHitsStorageIndex++;
        }

        private void SetCurrentRightHitDistance()
        {
            r.CurrentRightHitDistance = r.RightHitsStorage[r.CurrentRightHitsStorageIndex].distance;
        }

        private void SetCurrentRightHitCollider()
        {
            r.CurrentRightHitCollider = r.RightHitsStorage[r.CurrentRightHitsStorageIndex].collider;
        }

        private void SetCurrentRightLateralSlopeAngle()
        {
            r.RightLateralSlopeAngle = r.CurrentRightHitAngle;
        }

        private void SetRightFailedSlopeAngle()
        {
            r.PassedRightSlopeAngle = false;
        }

        private void SetCurrentDistanceBetweenRightHitAndRaycastOrigin()
        {
            r.DistanceBetweenRightHitAndRaycastOrigin = DistanceBetweenPointAndLine(
                r.RightHitsStorage[r.CurrentRightHitsStorageIndex].point, r.RightRaycastFromBottomOrigin,
                r.RightRaycastToTopOrigin);
        }

        private void ResetState()
        {
            SetRightRaycastHitMissed();
            SetRightFailedSlopeAngle();
            SetCurrentWallColliderNull();
            r.IsCollidingRight = false;
            r.CurrentRightHitCollider = null;
            r.CurrentRightHitAngle = 0f;
            r.RightLateralSlopeAngle = 0f;
            r.DistanceToRightCollider = -1f;
        }

        #endregion

        #endregion

        #region properties

        public RightRaycastHitColliderRuntimeData RuntimeData => r.RuntimeData;

        #region public methods

        public void OnInitializeRightHitsStorage()
        {
            InitializeRightHitsStorage();
        }

        public void OnInitializeCurrentRightHitsStorageIndex()
        {
            InitializeCurrentRightHitsStorageIndex();
        }

        public void OnSetRightRaycastHitConnected()
        {
            SetRightRaycastHitConnected();
        }

        public void OnSetRightRaycastHitMissed()
        {
            SetRightRaycastHitMissed();
        }

        public void OnSetCurrentRightHitsStorage()
        {
            SetCurrentRightHitsStorage();
        }

        public void OnSetCurrentRightHitAngle()
        {
            SetCurrentRightHitAngle();
        }

        public void OnSetIsCollidingRight()
        {
            SetIsCollidingRight();
        }

        public void OnSetRightDistanceToRightCollider()
        {
            SetRightDistanceToRightCollider();
        }

        public void OnSetRightCurrentWallCollider()
        {
            SetRightCurrentWallCollider();
        }

        public void OnAddToCurrentRightHitsStorageIndex()
        {
            AddToCurrentRightHitsStorageIndex();
        }

        public void OnSetCurrentRightHitDistance()
        {
            SetCurrentRightHitDistance();
        }

        public void OnSetCurrentRightHitCollider()
        {
            SetCurrentRightHitCollider();
        }

        public void OnSetCurrentRightLateralSlopeAngle()
        {
            SetCurrentRightLateralSlopeAngle();
        }

        public void OnSetRightFailedSlopeAngle()
        {
            SetRightFailedSlopeAngle();
        }

        public void OnSetCurrentDistanceBetweenRightHitAndRaycastOrigin()
        {
            SetCurrentDistanceBetweenRightHitAndRaycastOrigin();
        }

        public void OnSetCurrentWallColliderNull()
        {
            SetCurrentWallColliderNull();
        }

        public void OnResetState()
        {
            ResetState();
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
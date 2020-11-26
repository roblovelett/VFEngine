using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
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

        [SerializeField] private RightRaycastHitColliderData r;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        private PhysicsData physics;
        private RaycastData raycast;
        private RightRaycastData rightRaycast;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!r) r = CreateInstance<RightRaycastHitColliderData>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            raycast = raycastController.RaycastModel.Data;
            rightRaycast = raycastController.RightRaycastModel.Data;
            InitializeRightHitsStorage();
            InitializeCurrentRightHitsStorageIndex();
            ResetState();
        }

        private void InitializeRightHitsStorage()
        {
            r.RightHitsStorage = new RaycastHit2D[raycast.NumberOfHorizontalRaysPerSide];
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
            r.RightHitsStorage[r.CurrentRightHitsStorageIndex] = rightRaycast.CurrentRightRaycastHit;
        }

        private void SetCurrentRightHitAngle()
        {
            r.CurrentRightHitAngle = OnSetRaycastHitAngle(r.RightHitsStorage[r.CurrentRightHitsStorageIndex].normal,
                physics.Transform);
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
                r.RightHitsStorage[r.CurrentRightHitsStorageIndex].point, rightRaycast.RightRaycastFromBottomOrigin,
                rightRaycast.RightRaycastToTopOrigin);
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

        public RightRaycastHitColliderData Data => r;

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

        #endregion

        #endregion
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    using static MathsExtensions;
    using static RaycastHitCollider;
    using static UniTaskExtensions;

    public class RightRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private RightRaycastController rightRaycastController;
        private RightRaycastHitColliderData r;
        private PhysicsData physics;
        private RaycastData raycast;
        private RightRaycastData rightRaycast;

        #endregion

        #region private methods

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            physicsController = GetComponent<PhysicsController>();
            raycastController = GetComponent<RaycastController>();
            rightRaycastController = GetComponent<RightRaycastController>();
        }
        
        private void InitializeData()
        {
            r = new RightRaycastHitColliderData
            {
                CurrentRightWallCollider = null
            };
        }

        private void Start()
        {
            SetDependencies();
            Initialize();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            rightRaycast = rightRaycastController.Data;
        }

        private void Initialize()
        {
            InitializeRightHitsStorage();
            ResetState();
        }

        private void InitializeRightHitsStorage()
        {
            r.RightHitsStorage = new RaycastHit2D[raycast.NumberOfHorizontalRaysPerSide];
        }
        
        private void ResetState()
        {
            SetIsNotCollidingRight();
            InitializeDistanceToRightCollider();
            SetRightFailedSlopeAngle();
            InitializeRightLateralSlopeAngle();
        }

        private void SetIsNotCollidingRight()
        {
            r.IsCollidingRight = false;
        }

        private void InitializeDistanceToRightCollider()
        {
            r.DistanceToRightCollider = -1;
        }
        
        private void SetRightFailedSlopeAngle()
        {
            r.PassedRightSlopeAngle = false;
        }

        private void InitializeRightLateralSlopeAngle()
        {
            r.RightLateralSlopeAngle = 0;
        }

        private void PlatformerInitializeFrame()
        {
            SetCurrentWallColliderNull();
            ResetState();
        }
        
        private void SetCurrentWallColliderNull()
        {
            r.CurrentRightWallCollider = null;
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

        private void SetCurrentDistanceBetweenRightHitAndRaycastOrigin()
        {
            r.DistanceBetweenRightHitAndRaycastOrigin = DistanceBetweenPointAndLine(
                r.RightHitsStorage[r.CurrentRightHitsStorageIndex].point, rightRaycast.RightRaycastFromBottomOrigin,
                rightRaycast.RightRaycastToTopOrigin);
        }

        #endregion

        #endregion

        #region properties

        public RightRaycastHitColliderData Data => r;

        #region public methods
        
        #region platformer

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }
        
        #endregion

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

        public async UniTaskVoid OnSetIsCollidingRight()
        {
            SetIsCollidingRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetRightDistanceToRightCollider()
        {
            SetRightDistanceToRightCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        public async UniTaskVoid OnSetCurrentWallColliderNull()
        {
            SetCurrentWallColliderNull();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
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
                CurrentWallCollider = null
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
            InitializeHitsStorage();
            ResetState();
        }

        private void InitializeHitsStorage()
        {
            r.HitsStorage = new RaycastHit2D[raycast.NumberOfHorizontalRaysPerSide];
        }
        
        private void ResetState()
        {
            SetIsNotColliding();
            InitializeDistanceToCollider();
            SetFailedSlopeAngle();
            InitializeLateralSlopeAngle();
        }

        private void SetIsNotColliding()
        {
            r.IsColliding = false;
        }

        private void InitializeDistanceToCollider()
        {
            r.DistanceToCollider = -1;
        }
        
        private void SetFailedSlopeAngle()
        {
            r.PassedSlopeAngle = false;
        }

        private void InitializeLateralSlopeAngle()
        {
            r.LateralSlopeAngle = 0;
        }

        private void PlatformerInitializeFrame()
        {
            SetCurrentWallColliderNull();
            ResetState();
        }
        
        private void SetCurrentWallColliderNull()
        {
            r.CurrentWallCollider = null;
        }
        private bool IncorrectHitsStorage => r.HitsStorage.Length != raycast.NumberOfHorizontalRaysPerSide;
        
        private void PlatformerSetHitsStorage()
        {
            if (IncorrectHitsStorage) InitializeHitsStorage();
            InitializeCurrentHitsStorageIndex();
        }
        
        private void InitializeCurrentHitsStorageIndex()
        {
            r.CurrentHitsStorageIndex = 0;
        }

        private void PlatformerSetCurrentHitsStorage()
        {
            SetCurrentHitsStorage();
        }
        
        private void SetCurrentHitsStorage()
        {
            r.HitsStorage[r.CurrentHitsStorageIndex] = rightRaycast.CurrentRaycastHit;
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        private void PlatformerAddToCurrentHitsStorageIndex()
        {
            AddToCurrentHitsStorageIndex();
        }
        
        private void AddToCurrentHitsStorageIndex()
        {
            r.CurrentHitsStorageIndex++;
        }
        
        // ============================================================== //
        
        private void SetRightRaycastHitConnected()
        {
            r.HitConnected = true;
        }

        private void SetRightDistanceToRightCollider()
        {
            r.DistanceToCollider = r.CurrentHitAngle;
        }

        private void SetRightRaycastHitMissed()
        {
            r.HitConnected = false;
        }

        private void SetCurrentRightHitAngle()
        {
            r.CurrentHitAngle = OnSetRaycastHitAngle(r.HitsStorage[r.CurrentHitsStorageIndex].normal,
                physics.Transform);
        }

        private void SetIsCollidingRight()
        {
            r.IsColliding = true;
        }

        private void SetRightCurrentWallCollider()
        {
            r.CurrentWallCollider = r.CurrentHitCollider.gameObject;
        }
        
        private void SetCurrentRightHitDistance()
        {
            r.CurrentHitDistance = r.HitsStorage[r.CurrentHitsStorageIndex].distance;
        }

        private void SetCurrentRightHitCollider()
        {
            r.CurrentHitCollider = r.HitsStorage[r.CurrentHitsStorageIndex].collider;
        }

        private void SetCurrentRightLateralSlopeAngle()
        {
            r.LateralSlopeAngle = r.CurrentHitAngle;
        }

        private void SetCurrentDistanceBetweenRightHitAndRaycastOrigin()
        {
            r.DistanceBetweenHitAndRaycastOrigin = DistanceBetweenPointAndLine(
                r.HitsStorage[r.CurrentHitsStorageIndex].point, rightRaycast.RaycastFromBottomOrigin,
                rightRaycast.RaycastToTopOrigin);
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

        public void OnPlatformerSetHitsStorage()
        {
            PlatformerSetHitsStorage();
        }

        public void OnPlatformerSetCurrentHitsStorage()
        {
            PlatformerSetCurrentHitsStorage();
        }

        public void OnPlatformerAddToCurrentHitsStorageIndex()
        {
            PlatformerAddToCurrentHitsStorageIndex();
        }
        
        #endregion

        /*public void OnInitializeRightHitsStorage()
        {
            InitializeHitsStorage();
        }

        public void OnInitializeCurrentRightHitsStorageIndex()
        {
            InitializeCurrentRightHitsStorageIndex();
        }*/

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
            SetCurrentHitsStorage();
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

        /*public void OnAddToCurrentRightHitsStorageIndex()
        {
            AddToCurrentRightHitsStorageIndex();
        }*/

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
            SetFailedSlopeAngle();
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
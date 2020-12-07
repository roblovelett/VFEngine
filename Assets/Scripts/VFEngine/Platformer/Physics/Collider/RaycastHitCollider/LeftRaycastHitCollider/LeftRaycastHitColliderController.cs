using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    using static MathsExtensions;
    using static RaycastHitCollider;
    using static UniTaskExtensions;

    public class LeftRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private LeftRaycastController leftRaycastController;
        private LeftRaycastHitColliderData l;
        private PhysicsData physics;
        private RaycastData raycast;
        private LeftRaycastData leftRaycast;

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
            leftRaycastController = GetComponent<LeftRaycastController>();
        }
        
        private void InitializeData()
        {
            l = new LeftRaycastHitColliderData
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
            leftRaycast = leftRaycastController.Data;
        }

        private void Initialize()
        {
            InitializeHitsStorage();
            ResetState();
        }

        private void InitializeHitsStorage()
        {
            l.HitsStorage = new RaycastHit2D[raycast.NumberOfHorizontalRaysPerSide];
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
            l.IsColliding = false;
        }

        private void InitializeDistanceToCollider()
        {
            l.DistanceToCollider = -1;
        }
        
        private void SetFailedSlopeAngle()
        {
            l.PassedSlopeAngle = false;
        }

        private void InitializeLateralSlopeAngle()
        {
            l.LateralSlopeAngle = 0;
        }

        private void PlatformerInitializeFrame()
        {
            SetCurrentWallColliderNull();
            ResetState();
        }
        
        private void SetCurrentWallColliderNull()
        {
            l.CurrentWallCollider = null;
        }
        
        private bool IncorrectHitsStorage => l.HitsStorage.Length != raycast.NumberOfHorizontalRaysPerSide;
        
        private void PlatformerSetHitsStorage()
        {
            if (IncorrectHitsStorage) InitializeHitsStorage();
            InitializeCurrentHitsStorageIndex();
        }
        
        private void InitializeCurrentHitsStorageIndex()
        {
            l.CurrentHitsStorageIndex = 0;
        }

        private void PlatformerSetCurrentHitsStorage()
        {
            SetCurrentHitsStorage();
        }
        
        private void SetCurrentHitsStorage()
        {
            l.HitsStorage[l.CurrentHitsStorageIndex] = leftRaycast.CurrentRaycastHit;
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        private void PlatformerAddToCurrentHitsStorageIndex()
        {
            AddToCurrentHitsStorageIndex();
        }
        
        private void AddToCurrentHitsStorageIndex()
        {
            l.CurrentHitsStorageIndex++;
        }
        
        // ========================================================== //
        
        

        private void SetCurrentLeftHitDistance()
        {
            l.CurrentHitDistance = l.HitsStorage[l.CurrentHitsStorageIndex].distance;
        }

        private void SetLeftRaycastHitConnected()
        {
            l.HitConnected = true;
        }

        private void SetLeftRaycastHitMissed()
        {
            l.HitConnected = false;
        }

        private void SetCurrentLeftHitAngle()
        {
            l.CurrentHitAngle = OnSetRaycastHitAngle(l.HitsStorage[l.CurrentHitsStorageIndex].normal,
                physics.Transform);
        }

        private void SetCurrentLeftHitCollider()
        {
            l.CurrentHitCollider = l.HitsStorage[l.CurrentHitsStorageIndex].collider;
        }

        private void SetCurrentLeftLateralSlopeAngle()
        {
            l.LateralSlopeAngle = l.CurrentHitAngle;
        }

        private void SetIsCollidingLeft()
        {
            l.IsColliding = true;
        }

        private void SetLeftDistanceToLeftCollider()
        {
            l.DistanceToCollider = l.CurrentHitAngle;
        }

        private void SetLeftCurrentWallCollider()
        {
            l.CurrentWallCollider = l.CurrentHitCollider.gameObject;
        }

        private void SetCurrentDistanceBetweenLeftHitAndRaycastOrigin()
        {
            l.DistanceBetweenHitAndRaycastOrigin = DistanceBetweenPointAndLine(
                l.HitsStorage[l.CurrentHitsStorageIndex].point, leftRaycast.RaycastFromBottomOrigin,
                leftRaycast.RaycastToTopOrigin);
        }

        

        #endregion

        #endregion

        #region properties

        public LeftRaycastHitColliderData Data => l;

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

        /*public void OnInitializeLeftHitsStorage()
        {
            InitializeLeftHitsStorage();
        }

        public void OnInitializeCurrentLeftHitsStorageIndex()
        {
            InitializeCurrentLeftHitsStorageIndex();
        }*/

        /*public void OnSetCurrentLeftHitsStorage()
        {
            SetCurrentLeftHitsStorage();
        }*/

        public void OnSetCurrentLeftHitAngle()
        {
            SetCurrentLeftHitAngle();
        }

        public async UniTaskVoid OnSetIsCollidingLeft()
        {
            SetIsCollidingLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetLeftDistanceToLeftCollider()
        {
            SetLeftDistanceToLeftCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetLeftCurrentWallCollider()
        {
            SetLeftCurrentWallCollider();
        }

        public async UniTaskVoid OnSetCurrentWallColliderNull()
        {
            SetCurrentWallColliderNull();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetLeftFailedSlopeAngle()
        {
            SetFailedSlopeAngle();
        }

        /*public void OnAddToCurrentLeftHitsStorageIndex()
        {
            AddToCurrentHitsStorageIndex();
        }*/

        public void OnSetCurrentLeftHitDistance()
        {
            SetCurrentLeftHitDistance();
        }

        public void OnSetLeftRaycastHitConnected()
        {
            SetLeftRaycastHitConnected();
        }

        public void OnSetLeftRaycastHitMissed()
        {
            SetLeftRaycastHitMissed();
        }

        public void OnSetCurrentLeftHitCollider()
        {
            SetCurrentLeftHitCollider();
        }

        public void OnSetCurrentLeftLateralSlopeAngle()
        {
            SetCurrentLeftLateralSlopeAngle();
        }

        public void OnSetCurrentDistanceBetweenLeftHitAndRaycastOrigin()
        {
            SetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
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
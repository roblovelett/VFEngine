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
    using static Mathf;
    using static Vector2;

    public class LeftRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private LeftRaycastController leftRaycastController;
        private RaycastHitColliderController raycastHitColliderController;
        private LeftRaycastHitColliderData l;
        private PhysicsData physics;
        private RaycastData raycast;
        private LeftRaycastData leftRaycast;
        private RaycastHitColliderData raycastHitCollider;

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
            raycastHitColliderController = GetComponent<RaycastHitColliderController>();
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
            raycastHitCollider = raycastHitColliderController.Data;
        }

        private void Initialize()
        {
            InitializeHitsStorage();
            SetCurrentHitsStorage();
            SetCurrentHit();
            SetCurrentLeftHitCollider();
            SetHitIgnoredCollider();
            SetCurrentHitAngle();
            ResetState();
        }

        private void InitializeHitsStorage()
        {
            l.HitsStorage = new RaycastHit2D[raycast.NumberOfHorizontalRaysPerSide];
        }
        
        private void SetCurrentHitsStorage()
        {
            l.HitsStorage[l.CurrentHitsStorageIndex] = leftRaycast.CurrentRaycastHit;
        }

        private void SetCurrentHit()
        {
            l.CurrentHit = l.HitsStorage[l.CurrentHitsStorageIndex];
        }
        
        private void SetCurrentLeftHitCollider()
        {
            l.CurrentHitCollider = l.CurrentHit.collider;
        }

        private void SetHitIgnoredCollider()
        {
            l.HitIgnoredCollider = l.CurrentHitCollider == raycastHitCollider.IgnoredCollider;
        }

        private void SetCurrentHitAngle()
        {
            l.CurrentHitAngle = Abs(Angle(l.CurrentHit.normal, physics.Transform.up));
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

        private void PlatformerSetLateralSlopeAngle()
        {
            SetLateralSlopeAngle();
        }
        
        private void SetLateralSlopeAngle()
        {
            l.LateralSlopeAngle = l.CurrentHitAngle;
        }

        private void PlatformerSetIsColliding()
        {
            SetIsColliding();
            SetDistanceToCollider();
        }
        
        private void SetIsColliding()
        {
            l.IsColliding = true;
        }
        
        private void SetDistanceToCollider()
        {
            l.DistanceToCollider = l.CurrentHit.distance;
        }

        private void PlatformerAddToCurrentHitsStorageIndex()
        {
            AddToCurrentHitsStorageIndex();
        }
        
        private void AddToCurrentHitsStorageIndex()
        {
            l.CurrentHitsStorageIndex++;
        }

        

        /*
        

        

        private void SetLeftCurrentWallCollider()
        {
            l.CurrentWallCollider = l.CurrentHitCollider.gameObject;
        }*/
        
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

        public void OnPlatformerSetLateralSlopeAngle()
        {
            PlatformerSetLateralSlopeAngle();
        }

        public void OnPlatformerSetIsColliding()
        {
            PlatformerSetIsColliding();
        }

        /*public void OnPlatformerSetCurrentHitsStorage()
        {
            PlatformerSetCurrentHitsStorage();
        }*/

        /*public void OnPlatformerSetCurrentHitDistance()
        {
            PlatformerSetCurrentHitDistance();
        }*/

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

        /*public void OnSetCurrentLeftHitAngle()
        {
            SetCurrentLeftHitAngle();
        }*/

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

        /*public void OnSetCurrentLeftHitDistance()
        {
            SetCurrentHitDistance();
        }

        public void OnSetLeftRaycastHitConnected()
        {
            SetLeftRaycastHitConnected();
        }

        public void OnSetLeftRaycastHitMissed()
        {
            SetLeftRaycastHitMissed();
        }*/

        /*public void OnSetCurrentLeftHitCollider()
        {
            SetCurrentLeftHitCollider();
        }*/

        /*public void OnSetCurrentLeftLateralSlopeAngle()
        {
            SetCurrentLeftLateralSlopeAngle();
        }*/

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
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Tools;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider
{
    using static MathsExtensions;
    using static Mathf;
    using static Vector2;

    public class RightRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RaycastController raycastController;
        private RightRaycastController rightRaycastController;
        private RaycastHitColliderController raycastHitColliderController;
        private RightRaycastHitColliderData r;
        private PhysicsData physics;
        private RaycastData raycast;
        private RightRaycastData rightRaycast;
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
            rightRaycastController = GetComponent<RightRaycastController>();
            raycastHitColliderController = GetComponent<RaycastHitColliderController>();
        }

        private void InitializeData()
        {
            r = new RightRaycastHitColliderData {CurrentWallCollider = null};
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
            raycastHitCollider = raycastHitColliderController.Data;
        }

        private void Initialize()
        {
            InitializeHitsStorage();
            InitializeCurrentHitsStorage();
            InitializeCurrentHit();
            InitializeHitConnected();
            InitializeCurrentLeftHitCollider();
            InitializeHitIgnoredCollider();
            InitializeCurrentHitAngle();
            ResetState();
        }

        private void InitializeHitsStorage()
        {
            r.HitsStorage = new RaycastHit2D[raycast.NumberOfHorizontalRaysPerSide];
        }

        private void InitializeCurrentHitsStorage()
        {
            r.HitsStorage[r.CurrentHitsStorageIndex] = rightRaycast.CurrentRaycastHit;
        }

        private void InitializeCurrentHit()
        {
            r.CurrentHit = r.HitsStorage[r.CurrentHitsStorageIndex];
        }

        private void InitializeHitConnected()
        {
            r.HitConnected = r.CurrentHit.distance > 0;
        }

        private void InitializeCurrentLeftHitCollider()
        {
            r.CurrentHitCollider = r.CurrentHit.collider;
        }

        private void InitializeHitIgnoredCollider()
        {
            r.HitIgnoredCollider = r.CurrentHitCollider == raycastHitCollider.IgnoredCollider;
        }

        private void InitializeCurrentHitAngle()
        {
            r.CurrentHitAngle = Abs(Angle(r.CurrentHit.normal, physics.Transform.up));
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

        #region platformer

        private void PlatformerInitializeFrame()
        {
            SetCurrentWallColliderNull();
            ResetState();
        }

        private void PlatformerSetHitsStorage()
        {
            if (IncorrectHitsStorage) InitializeHitsStorage();
            InitializeCurrentHitsStorageIndex();
        }

        private void PlatformerSetLateralSlopeAngle()
        {
            SetLateralSlopeAngle();
        }

        private void PlatformerSetIsColliding()
        {
            SetIsColliding();
            SetDistanceToCollider();
        }

        private void PlatformerHitWall()
        {
            SetCurrentWallCollider();
            SetFailedSlopeAngle();
            SetDistanceBetweenHitAndRaycastOrigins();
        }

        private void PlatformerAddToCurrentHitsStorageIndex()
        {
            AddToCurrentHitsStorageIndex();
        }

        #endregion

        private void SetCurrentWallColliderNull()
        {
            r.CurrentWallCollider = null;
        }

        private bool IncorrectHitsStorage => r.HitsStorage.Length != raycast.NumberOfHorizontalRaysPerSide;

        private void InitializeCurrentHitsStorageIndex()
        {
            r.CurrentHitsStorageIndex = 0;
        }

        private void SetLateralSlopeAngle()
        {
            r.LateralSlopeAngle = r.CurrentHitAngle;
        }

        private void SetIsColliding()
        {
            r.IsColliding = true;
        }

        private void SetDistanceToCollider()
        {
            r.DistanceToCollider = r.CurrentHit.distance;
        }

        private void SetCurrentWallCollider()
        {
            r.CurrentWallCollider = r.CurrentHitCollider.gameObject;
        }

        private void SetDistanceBetweenHitAndRaycastOrigins()
        {
            r.DistanceBetweenHitAndRaycastOrigins = DistanceBetweenPointAndLine(r.CurrentHit.point,
                rightRaycast.RaycastFromBottomOrigin, rightRaycast.RaycastToTopOrigin);
        }

        private void AddToCurrentHitsStorageIndex()
        {
            r.CurrentHitsStorageIndex++;
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

        public void OnPlatformerSetLateralSlopeAngle()
        {
            PlatformerSetLateralSlopeAngle();
        }

        public void OnPlatformerSetIsColliding()
        {
            PlatformerSetIsColliding();
        }

        public void OnPlatformerHitWall()
        {
            PlatformerHitWall();
        }

        public void OnPlatformerAddToCurrentHitsStorageIndex()
        {
            PlatformerAddToCurrentHitsStorageIndex();
        }

        #endregion

        #endregion

        #endregion
    }
}
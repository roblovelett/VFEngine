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
                CurrentLeftWallCollider = null
            };
        }

        private void Start()
        {
            SetDependencies();
            InitializeFrame();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            raycast = raycastController.Data;
            leftRaycast = leftRaycastController.Data;
        }

        private void InitializeFrame()
        {
            InitializeLeftHitsStorage();
            //InitializeCurrentLeftHitsStorageIndex();
            //l.LeftHitsStorageLength = l.LeftHitsStorage.Length;
            ResetState();
        }

        private void InitializeLeftHitsStorage()
        {
            l.LeftHitsStorage = new RaycastHit2D[raycast.NumberOfHorizontalRaysPerSide];
        }

        private void InitializeCurrentLeftHitsStorageIndex()
        {
            l.CurrentLeftHitsStorageIndex = 0;
        }

        private void SetCurrentLeftHitsStorage()
        {
            l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex] = leftRaycast.CurrentLeftRaycastHit;
        }

        private void SetCurrentLeftHitDistance()
        {
            l.CurrentLeftHitDistance = l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].distance;
        }

        private void SetLeftRaycastHitConnected()
        {
            l.LeftHitConnected = true;
        }

        private void SetLeftRaycastHitMissed()
        {
            l.LeftHitConnected = false;
        }

        private void SetCurrentLeftHitAngle()
        {
            l.CurrentLeftHitAngle = OnSetRaycastHitAngle(l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].normal,
                physics.Transform);
        }

        private void SetCurrentLeftHitCollider()
        {
            l.CurrentLeftHitCollider = l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].collider;
        }

        private void SetCurrentLeftLateralSlopeAngle()
        {
            l.LeftLateralSlopeAngle = l.CurrentLeftHitAngle;
        }

        private void SetIsCollidingLeft()
        {
            l.IsCollidingLeft = true;
        }

        private void SetLeftDistanceToLeftCollider()
        {
            l.DistanceToLeftCollider = l.CurrentLeftHitAngle;
        }

        private void SetLeftCurrentWallCollider()
        {
            l.CurrentLeftWallCollider = l.CurrentLeftHitCollider.gameObject;
        }

        private void SetCurrentWallColliderNull()
        {
            l.CurrentLeftWallCollider = null;
        }

        private void SetLeftFailedSlopeAngle()
        {
            l.PassedLeftSlopeAngle = false;
        }

        private void SetCurrentDistanceBetweenLeftHitAndRaycastOrigin()
        {
            l.DistanceBetweenLeftHitAndRaycastOrigin = DistanceBetweenPointAndLine(
                l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].point, leftRaycast.LeftRaycastFromBottomOrigin,
                leftRaycast.LeftRaycastToTopOrigin);
        }

        private void AddToCurrentLeftHitsStorageIndex()
        {
            l.CurrentLeftHitsStorageIndex++;
        }

        private void ResetState()
        {
            l.LeftHitConnected = false;
            l.PassedLeftSlopeAngle = false;
            l.IsCollidingLeft = false;
            l.CurrentLeftHitCollider = null;
            l.CurrentLeftWallCollider = null;
            l.CurrentLeftHitAngle = 0f;
            l.LeftLateralSlopeAngle = 0f;
            l.DistanceToLeftCollider = -1f;
        }

        #endregion

        #endregion

        #region properties

        public LeftRaycastHitColliderData Data => l;

        #region public methods

        public void OnInitializeLeftHitsStorage()
        {
            InitializeLeftHitsStorage();
        }

        public void OnInitializeCurrentLeftHitsStorageIndex()
        {
            InitializeCurrentLeftHitsStorageIndex();
        }

        public void OnSetCurrentLeftHitsStorage()
        {
            SetCurrentLeftHitsStorage();
        }

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
            SetLeftFailedSlopeAngle();
        }

        public void OnAddToCurrentLeftHitsStorageIndex()
        {
            AddToCurrentLeftHitsStorageIndex();
        }

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
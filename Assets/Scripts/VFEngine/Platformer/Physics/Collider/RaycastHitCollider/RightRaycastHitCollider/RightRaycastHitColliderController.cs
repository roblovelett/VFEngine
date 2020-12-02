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

        [SerializeField] private GameObject character;
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
            LoadCharacter();
            InitializeData();
            SetControllers();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            r = new RightRaycastHitColliderData();
        }

        private void SetControllers()
        {
            physicsController = character.GetComponentNoAllocation<PhysicsController>();
            raycastController = character.GetComponentNoAllocation<RaycastController>();
            rightRaycastController = character.GetComponentNoAllocation<RightRaycastController>();
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
            rightRaycast = rightRaycastController.Data;
        }

        private void InitializeFrame()
        {
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
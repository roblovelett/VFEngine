using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static RaycastHitColliderData;
    using static UpRaycastHitColliderData;
    using static RightRaycastHitColliderData;
    using static DownRaycastHitColliderData;
    using static LeftRaycastHitColliderData;
    using static DistanceToGroundRaycastHitColliderData;
    using static StickyRaycastHitColliderData;
    using static LeftStickyRaycastHitColliderData;
    using static RightStickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class RaycastHitColliderController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastHitColliderModel raycastHitColliderModel;
        [SerializeField] private UpRaycastHitColliderModel upRaycastHitColliderModel;
        [SerializeField] private RightRaycastHitColliderModel rightRaycastHitColliderModel;
        [SerializeField] private DownRaycastHitColliderModel downRaycastHitColliderModel;
        [SerializeField] private LeftRaycastHitColliderModel leftRaycastHitColliderModel;
        [SerializeField] private DistanceToGroundRaycastHitColliderModel distanceToGroundRaycastHitColliderModel;
        [SerializeField] private StickyRaycastHitColliderModel stickyRaycastHitColliderModel;
        [SerializeField] private RightStickyRaycastHitColliderModel rightStickyRaycastHitColliderModel;
        [SerializeField] private LeftStickyRaycastHitColliderModel leftStickyRaycastHitColliderModel;

        #endregion

        #region private methods

        private void Awake()
        {
            GetModels();
            Async(InitializeModels());
        }

        private void GetModels()
        {
            if (!raycastHitColliderModel)
                raycastHitColliderModel = LoadModel<RaycastHitColliderModel>(RaycastHitColliderModelPath);
            if (!upRaycastHitColliderModel)
                upRaycastHitColliderModel = LoadModel<UpRaycastHitColliderModel>(UpRaycastHitColliderModelPath);
            if (!rightRaycastHitColliderModel)
                rightRaycastHitColliderModel =
                    LoadModel<RightRaycastHitColliderModel>(RightRaycastHitColliderModelPath);
            if (!downRaycastHitColliderModel)
                downRaycastHitColliderModel = LoadModel<DownRaycastHitColliderModel>(DownRaycastHitColliderModelPath);
            if (!leftRaycastHitColliderModel)
                leftRaycastHitColliderModel = LoadModel<LeftRaycastHitColliderModel>(LeftRaycastHitColliderModelPath);
            if (!distanceToGroundRaycastHitColliderModel)
                distanceToGroundRaycastHitColliderModel =
                    LoadModel<DistanceToGroundRaycastHitColliderModel>(DistanceToGroundRaycastHitColliderModelPath);
            if (!stickyRaycastHitColliderModel)
                stickyRaycastHitColliderModel =
                    LoadModel<StickyRaycastHitColliderModel>(StickyRaycastHitColliderModelPath);
            if (!rightStickyRaycastHitColliderModel)
                rightStickyRaycastHitColliderModel =
                    LoadModel<RightStickyRaycastHitColliderModel>(RightStickyRaycastHitColliderModelPath);
            if (!leftStickyRaycastHitColliderModel)
                leftStickyRaycastHitColliderModel =
                    LoadModel<LeftStickyRaycastHitColliderModel>(LeftStickyRaycastHitColliderModelPath);
        }

        private async UniTaskVoid InitializeModels()
        {
            var rTask1 = Async(raycastHitColliderModel.OnInitialize());
            var rTask2 = Async(upRaycastHitColliderModel.OnInitialize());
            var rTask3 = Async(rightRaycastHitColliderModel.OnInitialize());
            var rTask4 = Async(downRaycastHitColliderModel.OnInitialize());
            var rTask5 = Async(leftRaycastHitColliderModel.OnInitialize());
            var rTask6 = Async(distanceToGroundRaycastHitColliderModel.OnInitialize());
            var rTask7 = Async(stickyRaycastHitColliderModel.OnInitialize());
            var rTask8 = Async(leftStickyRaycastHitColliderModel.OnInitialize());
            var rTask9 = Async(rightStickyRaycastHitColliderModel.OnInitialize());
            var rTask = await (rTask1, rTask2, rTask3, rTask4, rTask5, rTask6, rTask7, rTask8, rTask9);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #region right raycast model

        public async UniTaskVoid SetCurrentRightWallColliderNull()
        {
            rightRaycastHitColliderModel.OnSetCurrentWallColliderNull();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region left raycast model

        public async UniTaskVoid SetCurrentLeftWallColliderNull()
        {
            leftRaycastHitColliderModel.OnSetCurrentWallColliderNull();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #endregion

        #region properties

        #region public methods

        #region raycast hit collider model

        public async UniTaskVoid ResetRaycastHitColliderState()
        {
            raycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ClearContactList()
        {
            raycastHitColliderModel.OnClearContactList();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void AddRightHitToContactList()
        {
            raycastHitColliderModel.OnAddRightHitToContactList();
        }

        public void AddLeftHitToContactList()
        {
            raycastHitColliderModel.OnAddLeftHitToContactList();
        }

        #endregion

        #region up raycast hit collider model

        public async UniTaskVoid ResetUpRaycastHitColliderState()
        {
            upRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpHitConnected()
        {
            upRaycastHitColliderModel.OnInitializeUpHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpHitsStorageCollidingIndex()
        {
            upRaycastHitColliderModel.OnInitializeUpHitsStorageCollidingIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpHitsStorageCurrentIndex()
        {
            upRaycastHitColliderModel.OnInitializeUpHitsStorageCurrentIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void InitializeUpHitsStorage()
        {
            upRaycastHitColliderModel.OnInitializeUpHitsStorage();
        }

        public void AddToUpHitsStorageCurrentIndex()
        {
            upRaycastHitColliderModel.OnAddToUpHitsStorageCurrentIndex();
        }

        public void SetCurrentUpHitsStorage()
        {
            upRaycastHitColliderModel.OnSetCurrentUpHitsStorage();
        }

        public void SetRaycastUpHitAt()
        {
            upRaycastHitColliderModel.OnSetRaycastUpHitAt();
        }

        public async UniTaskVoid SetUpHitConnected()
        {
            upRaycastHitColliderModel.OnSetUpHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetUpHitsStorageCollidingIndexAt()
        {
            upRaycastHitColliderModel.OnSetUpHitsStorageCollidingIndexAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetIsCollidingAbove()
        {
            upRaycastHitColliderModel.OnSetIsCollidingAbove();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetWasTouchingCeilingLastFrame()
        {
            upRaycastHitColliderModel.OnSetWasTouchingCeilingLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region right raycast hit collider model

        public async UniTaskVoid ResetRightRaycastHitColliderState()
        {
            rightRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void InitializeRightHitsStorage()
        {
            rightRaycastHitColliderModel.OnInitializeRightHitsStorage();
        }

        public void InitializeCurrentRightHitsStorageIndex()
        {
            rightRaycastHitColliderModel.OnInitializeCurrentRightHitsStorageIndex();
        }

        public void SetCurrentRightHitsStorage()
        {
            rightRaycastHitColliderModel.OnSetCurrentRightHitsStorage();
        }

        public void SetRightRaycastHitConnected()
        {
            rightRaycastHitColliderModel.OnSetRightRaycastHitConnected();
        }

        public void SetRightRaycastHitMissed()
        {
            rightRaycastHitColliderModel.OnSetRightRaycastHitMissed();
        }

        public void SetCurrentRightHitAngle()
        {
            rightRaycastHitColliderModel.OnSetCurrentRightHitAngle();
        }

        public async UniTaskVoid SetRightIsCollidingRight()
        {
            rightRaycastHitColliderModel.OnSetIsCollidingRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightDistanceToRightCollider()
        {
            rightRaycastHitColliderModel.OnSetRightDistanceToRightCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetRightCurrentWallCollider()
        {
            rightRaycastHitColliderModel.OnSetRightCurrentWallCollider();
        }

        public void AddToCurrentRightHitsStorageIndex()
        {
            rightRaycastHitColliderModel.OnAddToCurrentRightHitsStorageIndex();
        }

        public void SetCurrentRightHitDistance()
        {
            rightRaycastHitColliderModel.OnSetCurrentRightHitDistance();
        }

        public void SetCurrentRightHitCollider()
        {
            rightRaycastHitColliderModel.OnSetCurrentRightHitCollider();
        }

        public void SetCurrentRightLateralSlopeAngle()
        {
            rightRaycastHitColliderModel.OnSetCurrentRightLateralSlopeAngle();
        }

        public void SetRightFailedSlopeAngle()
        {
            rightRaycastHitColliderModel.OnSetRightFailedSlopeAngle();
        }

        public void SetCurrentDistanceBetweenRightHitAndRaycastOrigin()
        {
            rightRaycastHitColliderModel.OnSetCurrentDistanceBetweenRightHitAndRaycastOrigin();
        }

        #endregion

        #region down raycast hit collider model

        public async UniTaskVoid SetOnMovingPlatform()
        {
            downRaycastHitColliderModel.OnSetOnMovingPlatform();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetMovingPlatformCurrentGravity()
        {
            downRaycastHitColliderModel.OnSetMovingPlatformCurrentGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetWasGroundedLastFrame()
        {
            downRaycastHitColliderModel.OnSetWasGroundedLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetStandingOnLastFrame()
        {
            downRaycastHitColliderModel.OnSetStandingOnLastFrame();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ResetDownRaycastHitColliderState()
        {
            downRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCurrentDownHitsStorage()
        {
            downRaycastHitColliderModel.OnSetCurrentDownHitsStorage();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeFriction()
        {
            downRaycastHitColliderModel.OnInitializeFriction();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void InitializeDownHitsStorage()
        {
            downRaycastHitColliderModel.OnInitializeDownHitsStorage();
        }

        public async UniTaskVoid InitializeDownHitsStorageSmallestDistanceIndex()
        {
            downRaycastHitColliderModel.OnInitializeDownHitsStorageSmallestDistanceIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeDownHitConnected()
        {
            downRaycastHitColliderModel.OnInitializeDownHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeDownHitsStorageIndex()
        {
            downRaycastHitColliderModel.OnInitializeDownHitsStorageIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void AddDownHitsStorageIndex()
        {
            downRaycastHitColliderModel.OnAddDownHitsStorageIndex();
        }

        public async UniTaskVoid SetRaycastDownHitAt()
        {
            downRaycastHitColliderModel.OnSetRaycastDownHitAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDownHitConnected()
        {
            downRaycastHitColliderModel.OnSetDownHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleAt()
        {
            downRaycastHitColliderModel.OnSetBelowSlopeAngleAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleAt()
        {
            downRaycastHitColliderModel.OnSetCrossBelowSlopeAngleAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSmallestDistanceIndexAt()
        {
            downRaycastHitColliderModel.OnSetSmallestDistanceIndexAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetNegativeBelowSlopeAngle()
        {
            downRaycastHitColliderModel.OnSetNegativeBelowSlopeAngle();
        }

        public async UniTaskVoid SetDownHitWithSmallestDistance()
        {
            downRaycastHitColliderModel.OnSetDownHitWithSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetIsCollidingBelow()
        {
            downRaycastHitColliderModel.OnSetIsCollidingBelow();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetIsNotCollidingBelow()
        {
            downRaycastHitColliderModel.OnSetIsNotCollidingBelow();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetFrictionToDownHitWithSmallestDistancesFriction()
        {
            downRaycastHitColliderModel.OnSetFrictionToDownHitWithSmallestDistancesFriction();
        }

        public void SetMovingPlatformToDownHitWithSmallestDistancesPathMovement()
        {
            downRaycastHitColliderModel.OnSetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
        }

        public async UniTaskVoid SetMovingPlatformToNull()
        {
            downRaycastHitColliderModel.OnSetMovingPlatformToNull();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid StopMovingPlatformCurrentGravity()
        {
            downRaycastHitColliderModel.OnStopMovingPlatformCurrentGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCurrentDownHitSmallestDistance()
        {
            downRaycastHitColliderModel.OnSetCurrentDownHitSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetGroundedEvent()
        {
            downRaycastHitColliderModel.OnSetGroundedEvent();
        }

        public void SetStandingOnLastFrameLayerToPlatforms()
        {
            downRaycastHitColliderModel.OnSetStandingOnLastFrameLayerToPlatform();
        }

        public void SetStandingOnLastFrameLayerToSavedBelowLayer()
        {
            downRaycastHitColliderModel.OnSetStandingOnLastFrameLayerToSavedBelowLayer();
        }

        public async UniTaskVoid SetStandingOn()
        {
            downRaycastHitColliderModel.OnSetStandingOn();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetStandingOnCollider()
        {
            downRaycastHitColliderModel.OnSetStandingOnCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetNotOnMovingPlatform()
        {
            downRaycastHitColliderModel.OnSetNotOnMovingPlatform();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region left raycast hit collider model

        public async UniTaskVoid ResetLeftRaycastHitColliderState()
        {
            leftRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void InitializeLeftHitsStorage()
        {
            leftRaycastHitColliderModel.OnInitializeLeftHitsStorage();
        }

        public void InitializeCurrentLeftHitsStorageIndex()
        {
            leftRaycastHitColliderModel.OnInitializeCurrentLeftHitsStorageIndex();
        }

        public void SetCurrentLeftHitsStorage()
        {
            leftRaycastHitColliderModel.OnSetCurrentLeftHitsStorage();
        }

        public void SetCurrentLeftHitAngle()
        {
            leftRaycastHitColliderModel.OnSetCurrentLeftHitAngle();
        }

        public async UniTaskVoid SetLeftIsCollidingLeft()
        {
            leftRaycastHitColliderModel.OnSetIsCollidingLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftDistanceToLeftCollider()
        {
            leftRaycastHitColliderModel.OnSetLeftDistanceToLeftCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetLeftCurrentWallCollider()
        {
            leftRaycastHitColliderModel.OnSetLeftCurrentWallCollider();
        }

        public void AddToCurrentLeftHitsStorageIndex()
        {
            leftRaycastHitColliderModel.OnAddToCurrentLeftHitsStorageIndex();
        }

        public void SetCurrentLeftHitDistance()
        {
            leftRaycastHitColliderModel.OnSetCurrentLeftHitDistance();
        }

        public void SetLeftRaycastHitConnected()
        {
            leftRaycastHitColliderModel.OnSetLeftRaycastHitConnected();
        }

        public void SetLeftRaycastHitMissed()
        {
            leftRaycastHitColliderModel.OnSetLeftRaycastHitMissed();
        }

        public void SetCurrentLeftHitCollider()
        {
            leftRaycastHitColliderModel.OnSetCurrentLeftHitCollider();
        }

        public void SetCurrentLeftLateralSlopeAngle()
        {
            leftRaycastHitColliderModel.OnSetCurrentLeftLateralSlopeAngle();
        }

        public void SetLeftFailedSlopeAngle()
        {
            leftRaycastHitColliderModel.OnSetLeftFailedSlopeAngle();
        }

        public void SetCurrentDistanceBetweenLeftHitAndRaycastOrigin()
        {
            leftRaycastHitColliderModel.OnSetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
        }

        #endregion

        #region distance to ground raycast hit collider model

        public async UniTaskVoid ResetDistanceToGroundRaycastHitColliderState()
        {
            distanceToGroundRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDistanceToGroundRaycastHit()
        {
            distanceToGroundRaycastHitColliderModel.OnSetDistanceToGroundRaycastHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeDistanceToGround()
        {
            distanceToGroundRaycastHitColliderModel.OnInitializeDistanceToGround();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void DecreaseDistanceToGround()
        {
            distanceToGroundRaycastHitColliderModel.OnDecreaseDistanceToGround();
        }

        public void ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            distanceToGroundRaycastHitColliderModel.OnApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
        }

        #endregion

        #region sticky raycast hit collider model

        public async UniTaskVoid ResetStickyRaycastHitColliderState()
        {
            stickyRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeBelowSlopeAngle()
        {
            stickyRaycastHitColliderModel.OnInitializeBelowSlopeAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            stickyRaycastHitColliderModel.OnSetBelowSlopeAngleToBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            stickyRaycastHitColliderModel.OnSetBelowSlopeAngleToBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region right sticky raycast hit collider model

        public async UniTaskVoid ResetRightStickyRaycastHitColliderState()
        {
            rightStickyRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleRight()
        {
            rightStickyRaycastHitColliderModel.OnSetBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleRight()
        {
            rightStickyRaycastHitColliderModel.OnSetCrossBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetBelowSlopeAngleRightToNegative()
        {
            rightStickyRaycastHitColliderModel.OnSetBelowSlopeAngleRightToNegative();
        }

        #endregion

        #region left sticky raycast hit collider model

        public async UniTaskVoid ResetLeftStickyRaycastHitColliderState()
        {
            leftStickyRaycastHitColliderModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleLeft()
        {
            leftStickyRaycastHitColliderModel.OnSetBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleLeft()
        {
            leftStickyRaycastHitColliderModel.OnSetCrossBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetBelowSlopeAngleLeftToNegative()
        {
            leftStickyRaycastHitColliderModel.OnSetBelowSlopeAngleLeftToNegative();
        }

        #endregion

        #endregion

        #endregion
    }
}
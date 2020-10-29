using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static Debug;
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    public class RaycastHitColliderController : MonoBehaviour, IController
    {
        /* fields: dependencies */
        [SerializeField] private RaycastHitColliderModel upColliderModel;
        [SerializeField] private RaycastHitColliderModel rightColliderModel;
        [SerializeField] private RaycastHitColliderModel downColliderModel;
        [SerializeField] private RaycastHitColliderModel leftColliderModel;

        /* fields */
        private RaycastHitColliderModel[] models;

        /* fields: methods */
        private async void Awake()
        {
            GetModels();
            var rTask1 = Async(upColliderModel.Initialize());
            var rTask2 = Async(rightColliderModel.Initialize());
            var rTask3 = Async(downColliderModel.Initialize());
            var rTask4 = Async(leftColliderModel.Initialize());
            var rTask = await (rTask1, rTask2, rTask3, rTask4);
        }

        private void GetModels()
        {
            models = new[] {upColliderModel, rightColliderModel, downColliderModel, leftColliderModel};
            var names = new[] {"upColliderModel", "rightColliderModel", "downColliderModel", "leftColliderModel"};
            for (var i = 0; i < models.Length; i++)
            {
                if (models[i]) continue;
                models[i] = LoadData(ModelPath) as RaycastHitColliderModel;
                switch (i)
                {
                    case 0:
                        upColliderModel = models[i];
                        break;
                    case 1:
                        rightColliderModel = models[i];
                        break;
                    case 2:
                        downColliderModel = models[i];
                        break;
                    case 3:
                        leftColliderModel = models[i];
                        break;
                }

                Assert(models[i] != null, names[i] + " != null");
            }
        }

        public async UniTaskVoid ClearContactList()
        {
            var rhcTask1 = Async(upColliderModel.OnClearContactList());
            var rhcTask2 = Async(rightColliderModel.OnClearContactList());
            var rhcTask3 = Async(downColliderModel.OnClearContactList());
            var rhcTask4 = Async(leftColliderModel.OnClearContactList());
            var rhcTask = await (rhcTask1, rhcTask2, rhcTask3, rhcTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetWasGroundedLastFrame()
        {
            var rhcTask1 = Async(upColliderModel.OnSetWasGroundedLastFrame());
            var rhcTask2 = Async(rightColliderModel.OnSetWasGroundedLastFrame());
            var rhcTask3 = Async(downColliderModel.OnSetWasGroundedLastFrame());
            var rhcTask4 = Async(leftColliderModel.OnSetWasGroundedLastFrame());
            var rhcTask = await (rhcTask1, rhcTask2, rhcTask3, rhcTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetStandingOnLastFrame()
        {
            var rhcTask1 = Async(upColliderModel.OnSetStandingOnLastFrame());
            var rhcTask2 = Async(rightColliderModel.OnSetStandingOnLastFrame());
            var rhcTask3 = Async(downColliderModel.OnSetStandingOnLastFrame());
            var rhcTask4 = Async(leftColliderModel.OnSetStandingOnLastFrame());
            var rhcTask = await (rhcTask1, rhcTask2, rhcTask3, rhcTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetWasTouchingCeilingLastFrame()
        {
            var rhcTask1 = Async(upColliderModel.OnSetWasTouchingCeilingLastFrame());
            var rhcTask2 = Async(rightColliderModel.OnSetWasTouchingCeilingLastFrame());
            var rhcTask3 = Async(downColliderModel.OnSetWasTouchingCeilingLastFrame());
            var rhcTask4 = Async(leftColliderModel.OnSetWasTouchingCeilingLastFrame());
            var rhcTask = await (rhcTask1, rhcTask2, rhcTask3, rhcTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCurrentWallColliderNull()
        {
            var rhcTask1 = Async(upColliderModel.OnSetCurrentWallColliderNull());
            var rhcTask2 = Async(rightColliderModel.OnSetCurrentWallColliderNull());
            var rhcTask3 = Async(downColliderModel.OnSetCurrentWallColliderNull());
            var rhcTask4 = Async(leftColliderModel.OnSetCurrentWallColliderNull());
            var rhcTask = await (rhcTask1, rhcTask2, rhcTask3, rhcTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ResetState()
        {
            var rhcTask1 = Async(upColliderModel.OnResetState());
            var rhcTask2 = Async(rightColliderModel.OnResetState());
            var rhcTask3 = Async(downColliderModel.OnResetState());
            var rhcTask4 = Async(leftColliderModel.OnResetState());
            var rhcTask = await (rhcTask1, rhcTask2, rhcTask3, rhcTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetOnMovingPlatform()
        {
            var rchTask1 = Async(upColliderModel.OnSetOnMovingPlatform());
            var rchTask2 = Async(rightColliderModel.OnSetOnMovingPlatform());
            var rchTask3 = Async(downColliderModel.OnSetOnMovingPlatform());
            var rchTask4 = Async(leftColliderModel.OnSetOnMovingPlatform());
            var rhcTask = await (rchTask1, rchTask2, rchTask3, rchTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetMovingPlatformCurrentGravity()
        {
            var rchTask1 = Async(upColliderModel.OnSetMovingPlatformCurrentGravity());
            var rchTask2 = Async(rightColliderModel.OnSetMovingPlatformCurrentGravity());
            var rchTask3 = Async(downColliderModel.OnSetMovingPlatformCurrentGravity());
            var rchTask4 = Async(leftColliderModel.OnSetMovingPlatformCurrentGravity());
            var rhcTask = await (rchTask1, rchTask2, rchTask3, rchTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void InitializeRightHitsStorage()
        {
            rightColliderModel.OnInitializeRightHitsStorage();
        }

        public void InitializeLeftHitsStorage()
        {
            leftColliderModel.OnInitializeLeftHitsStorage();
        }

        public void InitializeCurrentRightHitsStorageIndex()
        {
            rightColliderModel.OnInitializeCurrentRightHitsStorageIndex();
        }

        public void InitializeCurrentLeftHitsStorageIndex()
        {
            leftColliderModel.OnInitializeCurrentLeftHitsStorageIndex();
        }

        public void SetCurrentRightHitsStorage()
        {
            rightColliderModel.OnSetCurrentRightHitsStorage();
        }

        public void SetCurrentLeftHitsStorage()
        {
            leftColliderModel.OnSetCurrentLeftHitsStorage();
        }

        public async UniTaskVoid SetCurrentDownHitsStorage()
        {
            downColliderModel.OnSetCurrentDownHitsStorage();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCurrentRightHitAngle()
        {
            rightColliderModel.OnSetCurrentRightHitAngle();
        }

        public void SetCurrentLeftHitAngle()
        {
            leftColliderModel.OnSetCurrentLeftHitAngle();
        }

        public async UniTaskVoid SetRightIsCollidingRight()
        {
            rightColliderModel.OnSetRightIsCollidingRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightDistanceToRightCollider()
        {
            rightColliderModel.OnSetRightDistanceToRightCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftIsCollidingLeft()
        {
            leftColliderModel.OnSetLeftIsCollidingLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftDistanceToLeftCollider()
        {
            leftColliderModel.OnSetLeftDistanceToLeftCollider();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetRightCurrentWallCollider()
        {
            rightColliderModel.OnSetRightCurrentWallCollider();
        }

        public void SetLeftCurrentWallCollider()
        {
            leftColliderModel.OnSetLeftCurrentWallCollider();
        }

        public void AddRightHitToContactList()
        {
            rightColliderModel.OnAddRightHitToContactList();
        }

        public void AddLeftHitToContactList()
        {
            leftColliderModel.OnAddLeftHitToContactList();
        }

        public void AddToCurrentRightHitsStorageIndex()
        {
            rightColliderModel.OnAddToCurrentRightHitsStorageIndex();
        }

        public void AddToCurrentLeftHitsStorageIndex()
        {
            leftColliderModel.OnAddToCurrentLeftHitsStorageIndex();
        }

        public async UniTaskVoid InitializeFriction()
        {
            downColliderModel.OnInitializeFriction();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void InitializeDownHitsStorage()
        {
            downColliderModel.OnInitializeDownHitsStorage();
        }

        public async UniTaskVoid InitializeDownHitsStorageSmallestDistanceIndex()
        {
            downColliderModel.OnInitializeDownHitsStorageSmallestDistanceIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeDownHitConnected()
        {
            downColliderModel.OnInitializeDownHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeDownHitsStorageIndex()
        {
            downColliderModel.OnInitializeDownHitsStorageIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void AddDownHitsStorageIndex()
        {
            downColliderModel.OnAddDownHitsStorageIndex();
        }

        public async UniTaskVoid SetRaycastDownHitAt()
        {
            downColliderModel.OnSetRaycastDownHitAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDownHitConnected()
        {
            downColliderModel.OnSetDownHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleAt()
        {
            downColliderModel.OnSetBelowSlopeAngleAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleAt()
        {
            downColliderModel.OnSetCrossBelowSlopeAngleAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSmallestDistanceIndexAt()
        {
            downColliderModel.OnSetSmallestDistanceIndexAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetNegativeBelowSlopeAngle()
        {
            downColliderModel.OnSetNegativeBelowSlopeAngle();
        }

        public async UniTaskVoid SetDownHitWithSmallestDistance()
        {
            downColliderModel.OnSetDownHitWithSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetIsCollidingBelow()
        {
            downColliderModel.OnSetIsCollidingBelow();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetIsNotCollidingBelow()
        {
            downColliderModel.OnSetIsNotCollidingBelow();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetFrictionToDownHitWithSmallestDistancesFriction()
        {
            downColliderModel.OnSetFrictionToDownHitWithSmallestDistancesFriction();
        }

        public async UniTaskVoid SetMovingPlatformToDownHitWithSmallestDistancesPathMovement()
        {
            downColliderModel.OnSetMovingPlatformToDownHitWithSmallestDistancesPathMovement();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetHasMovingPlatform()
        {
            downColliderModel.OnSetHasMovingPlatform();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetMovingPlatformToNull()
        {
            downColliderModel.OnSetMovingPlatformToNull();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDoesNotHaveMovingPlatform()
        {
            downColliderModel.OnSetDoesNotHaveMovingPlatform();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid StopMovingPlatformCurrentGravity()
        {
            downColliderModel.OnStopMovingPlatformCurrentGravity();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpHitConnected()
        {
            upColliderModel.OnInitializeUpHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpHitsStorageCollidingIndex()
        {
            upColliderModel.OnInitializeUpHitsStorageCollidingIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpHitsStorageCurrentIndex()
        {
            upColliderModel.OnInitializeUpHitsStorageCurrentIndex();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void InitializeUpHitsStorage()
        {
            upColliderModel.OnInitializeUpHitsStorage();
        }

        public void AddToUpHitsStorageCurrentIndex()
        {
            upColliderModel.OnAddToUpHitsStorageCurrentIndex();
        }

        public void SetCurrentUpHitsStorage()
        {
            upColliderModel.OnSetCurrentUpHitsStorage();
        }

        public void SetRaycastUpHitAt()
        {
            upColliderModel.OnSetRaycastUpHitAt();
        }

        public async UniTaskVoid SetUpHitConnected()
        {
            upColliderModel.OnSetUpHitConnected();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetUpHitsStorageCollidingIndexAt()
        {
            upColliderModel.OnSetUpHitsStorageCollidingIndexAt();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetIsCollidingAbove()
        {
            upColliderModel.OnSetIsCollidingAbove();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCurrentRightHitDistance()
        {
            rightColliderModel.OnSetCurrentRightHitDistance();
        }

        public void SetCurrentLeftHitDistance()
        {
            leftColliderModel.OnSetCurrentLeftHitDistance();
        }

        public void SetCurrentRightHitCollider()
        {
            rightColliderModel.OnSetCurrentRightHitCollider();
        }

        public void SetCurrentLeftHitCollider()
        {
            leftColliderModel.OnSetCurrentLeftHitCollider();
        }

        public void SetCurrentRightLateralSlopeAngle()
        {
            rightColliderModel.OnSetCurrentRightLateralSlopeAngle();
        }

        public void SetCurrentLeftLateralSlopeAngle()
        {
            leftColliderModel.OnSetCurrentLeftLateralSlopeAngle();
        }

        public void SetRightFailedSlopeAngle()
        {
            rightColliderModel.OnSetRightFailedSlopeAngle();
        }

        public void SetLeftFailedSlopeAngle()
        {
            leftColliderModel.OnSetLeftFailedSlopeAngle();
        }

        public void SetCurrentDistanceBetweenRightHitAndRaycastOrigin()
        {
            rightColliderModel.OnSetCurrentDistanceBetweenRightHitAndRaycastOrigin();
        }

        public void SetCurrentDistanceBetweenLeftHitAndRaycastOrigin()
        {
            leftColliderModel.OnSetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
        }

        public async UniTaskVoid SetCurrentDownHitSmallestDistance()
        {
            downColliderModel.OnSetCurrentDownHitSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetGroundedEvent()
        {
            downColliderModel.OnSetGroundedEvent();
        }

        public async UniTaskVoid InitializeDistanceToGround()
        {
            downColliderModel.OnInitializeDistanceToGround();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void DecreaseDistanceToGround()
        {
            downColliderModel.OnDecreaseDistanceToGround();
        }

        public void ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            downColliderModel.OnApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
        }

        public void SetStandingOnLastFrameLayerToPlatforms()
        {
            downColliderModel.OnSetStandingOnLastFrameLayerToPlatforms();
        }

        public void SetStandingOnLastFrameLayerToSavedBelowLayer()
        {
            downColliderModel.OnSetStandingOnLastFrameLayerToSavedBelowLayer();
        }
    }
}
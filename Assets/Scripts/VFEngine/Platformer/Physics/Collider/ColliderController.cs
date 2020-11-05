using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DownRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.RightRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Physics.Collider
{
    using static RaycastHitColliderData;
    using static UpRaycastHitColliderData;
    using static RightRaycastHitColliderData;
    using static DownRaycastHitColliderData;
    using static LeftRaycastHitColliderData;
    using static DistanceToGroundRaycastData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;
    
    //[RequireComponent(typeof(BoxcastController))]
    public class ColliderController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastHitColliderModel raycastHitColliderModel;
        [SerializeField] private UpRaycastHitColliderModel upRaycastHitColliderModel;
        [SerializeField] private RightRaycastHitColliderModel rightRaycastHitColliderModel;
        [SerializeField] private DownRaycastHitColliderModel downRaycastHitColliderModel;
        [SerializeField] private LeftRaycastHitColliderModel leftRaycastHitColliderModel;
        [SerializeField] private DistanceToGroundRaycastModel distanceToGroundRaycastModel;

        #endregion

        #region private methods

        private void Awake()
        {
            
            GetModels();
            //Async(InitializeModels());
        }

        private void GetModels()
        {
            if (!raycastHitColliderModel)
                raycastHitColliderModel = LoadModel<RaycastHitColliderModel>(RaycastHitColliderModelPath);
            if (!upRaycastHitColliderModel)
                upRaycastHitColliderModel = LoadModel<UpRaycastHitColliderModel>(UpRaycastHitColliderModelPath);
            if (!rightRaycastHitColliderModel)
                rightRaycastHitColliderModel = LoadModel<RightRaycastHitColliderModel>(RightRaycastHitColliderModelPath);
            if (!downRaycastHitColliderModel)
                downRaycastHitColliderModel = LoadModel<DownRaycastHitColliderModel>(DownRaycastHitColliderModelPath);
            if (!leftRaycastHitColliderModel)
                leftRaycastHitColliderModel = LoadModel<LeftRaycastHitColliderModel>(LeftRaycastHitColliderModelPath);
            if (!distanceToGroundRaycastModel)
                distanceToGroundRaycastModel =
                    LoadModel<DistanceToGroundRaycastModel>(DistanceToGroundRaycastModelPath);
        }

        /*
        private async UniTaskVoid InitializeModels()
        {
            var rTask1 = Async(raycastModel.OnInitialize());
            var rTask2 = Async(stickyRaycastModel.OnInitialize());
            var task1 = await (rTask1, rTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        */

        #endregion

        #endregion

        #region properties

        #region public methods

        #region raycast hit collider model

        public void AddRightHitToContactList()
        {
            //raycastHitColliderModel.OnAddRightHitToContactList();
        }

        public void AddLeftHitToContactList()
        {
            //raycastHitColliderModel.OnAddLeftHitToContactList();
        }

        #endregion

        #region up raycast hit collider model

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

        #endregion
        
        #region right raycast hit collider model

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
        
        #region down raycast model

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
        
        /*
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
        */
        
        #endregion
        
        #endregion
        
        #endregion
    }
}
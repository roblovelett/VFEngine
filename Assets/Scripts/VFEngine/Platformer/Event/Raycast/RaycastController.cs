using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast
{
    using static RaycastData;
    using static UpRaycastData;
    using static RightRaycastData;
    using static DownRaycastData;
    using static LeftRaycastData;
    using static DistanceToGroundRaycastData;
    using static StickyRaycastData;
    using static RightStickyRaycastData;
    using static LeftStickyRaycastData;
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [RequireComponent(typeof(BoxcastController))]
    public class RaycastController : MonoBehaviour
    {
        [SerializeField] private RaycastModel raycastModel;
        [SerializeField] private UpRaycastModel upRaycastModel;
        [SerializeField] private RightRaycastModel rightRaycastModel;
        [SerializeField] private DownRaycastModel downRaycastModel;
        [SerializeField] private LeftRaycastModel leftRaycastModel;
        [SerializeField] private DistanceToGroundRaycastModel distanceToGroundRaycastModel;
        [SerializeField] private StickyRaycastModel stickyRaycastModel;
        [SerializeField] private LeftStickyRaycastModel leftStickyRaycastModel;
        [SerializeField] private RightStickyRaycastModel rightStickyRaycastModel;

        private void Awake()
        {
            GetModels();
            InitializeModels();
        }

        private void GetModels()
        {
            if (!raycastModel) raycastModel = LoadModel<RaycastModel>(RaycastModelPath);
            if (!upRaycastModel) upRaycastModel = LoadModel<UpRaycastModel>(UpRaycastModelPath);
            if (!rightRaycastModel) rightRaycastModel = LoadModel<RightRaycastModel>(RightRaycastModelPath);
            if (!downRaycastModel) downRaycastModel = LoadModel<DownRaycastModel>(DownRaycastModelPath);
            if (!leftRaycastModel) leftRaycastModel = LoadModel<LeftRaycastModel>(LeftRaycastModelPath);
            if (!distanceToGroundRaycastModel)
                distanceToGroundRaycastModel =
                    LoadModel<DistanceToGroundRaycastModel>(DistanceToGroundRaycastModelPath);
            if (!stickyRaycastModel) stickyRaycastModel = LoadModel<StickyRaycastModel>(StickyRaycastModelPath);
            if (!rightStickyRaycastModel)
                rightStickyRaycastModel = LoadModel<RightStickyRaycastModel>(RightStickyRaycastModelPath);
            if (!leftStickyRaycastModel)
                leftStickyRaycastModel = LoadModel<LeftStickyRaycastModel>(LeftStickyRaycastModelPath);
        }

        private void InitializeModels()
        {
            raycastModel.OnInitialize();
        }

        #region raycast model

        public async UniTaskVoid SetRaysParameters()
        {
            raycastModel.OnSetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region up raycast model

        public async UniTaskVoid InitializeUpRaycastLength()
        {
            upRaycastModel.OnInitializeUpRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpRaycastStart()
        {
            upRaycastModel.OnInitializeUpRaycastStart();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpRaycastEnd()
        {
            upRaycastModel.OnInitializeUpRaycastEnd();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeUpRaycastSmallestDistance()
        {
            upRaycastModel.OnInitializeUpRaycastSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCurrentUpRaycastOrigin()
        {
            upRaycastModel.OnSetCurrentUpRaycastOrigin();
        }

        public void SetCurrentUpRaycast()
        {
            upRaycastModel.OnSetCurrentUpRaycast();
        }

        public void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            upRaycastModel.OnSetUpRaycastSmallestDistanceToRaycastUpHitAt();
        }

        #endregion

        #region right raycast model

        public void SetRightRaycastFromBottomOrigin()
        {
            rightRaycastModel.OnSetRightRaycastFromBottomOrigin();
        }

        public void SetRightRaycastToTopOrigin()
        {
            rightRaycastModel.OnSetRightRaycastToTopOrigin();
        }

        public void InitializeRightRaycastLength()
        {
            rightRaycastModel.OnInitializeRightRaycastLength();
        }

        public void SetCurrentRightRaycastOrigin()
        {
            rightRaycastModel.OnSetCurrentRightRaycastOrigin();
        }

        public void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            rightRaycastModel.OnSetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void SetCurrentRightRaycast()
        {
            rightRaycastModel.OnSetCurrentRightRaycast();
        }

        #endregion

        #region down raycast model

        public void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            downRaycastModel.OnSetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        public void SetCurrentDownRaycast()
        {
            downRaycastModel.OnSetCurrentDownRaycast();
        }

        public async UniTaskVoid InitializeDownRayLength()
        {
            downRaycastModel.OnInitializeDownRayLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void DoubleDownRayLength()
        {
            downRaycastModel.OnDoubleDownRayLength();
        }

        public void SetDownRayLengthToVerticalNewPosition()
        {
            downRaycastModel.OnSetDownRayLengthToVerticalNewPosition();
        }

        public async UniTaskVoid SetDownRaycastFromLeft()
        {
            downRaycastModel.OnSetDownRaycastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDownRaycastToRight()
        {
            downRaycastModel.OnSetDownRaycastToRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeSmallestDistanceToDownHit()
        {
            downRaycastModel.OnInitializeSmallestDistanceToDownHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSmallestDistanceToDownHitDistance()
        {
            downRaycastModel.OnSetSmallestDistanceToDownHitDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetDistanceBetweenDownRaycastsAndSmallestDistancePoint()
        {
            downRaycastModel.OnSetDistanceBetweenDownRaycastsAndSmallestDistancePoint();
        }

        public void SetCurrentDownRaycastOriginPoint()
        {
            downRaycastModel.OnSetCurrentDownRaycastOriginPoint();
        }

        #endregion

        #region left raycast model

        public void SetLeftRaycastFromBottomOrigin()
        {
            leftRaycastModel.OnSetLeftRaycastFromBottomOrigin();
        }

        public void SetLeftRaycastToTopOrigin()
        {
            leftRaycastModel.OnSetLeftRaycastToTopOrigin();
        }

        public void InitializeLeftRaycastLength()
        {
            leftRaycastModel.OnInitializeLeftRaycastLength();
        }

        public void SetCurrentLeftRaycastOrigin()
        {
            leftRaycastModel.OnSetCurrentLeftRaycastOrigin();
        }

        public void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            leftRaycastModel.OnSetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }

        public void SetCurrentLeftRaycast()
        {
            leftRaycastModel.OnSetCurrentLeftRaycast();
        }

        #endregion

        #region distance to ground raycast model

        public void SetDistanceToGroundRaycastOrigin()
        {
            distanceToGroundRaycastModel.OnSetDistanceToGroundRaycastOrigin();
        }

        public async UniTaskVoid SetDistanceToGroundRaycast()
        {
            distanceToGroundRaycastModel.OnSetDistanceToGroundRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetHasDistanceToGroundRaycast()
        {
            distanceToGroundRaycastModel.OnSetHasDistanceToGroundRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #region sticky raycast model

        public async UniTaskVoid SetStickyRaycastLength()
        {
            stickyRaycastModel.OnSetStickyRaycastLength();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetStickyRaycastLengthToSelf()
        {
            stickyRaycastModel.OnSetStickyRaycastLengthToSelf();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDoNotCastFromLeft()
        {
            stickyRaycastModel.OnSetDoNotCastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeBelowSlopeAngle()
        {
            stickyRaycastModel.OnInitializeBelowSlopeAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            stickyRaycastModel.OnSetBelowSlopeAngleToBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleRightLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            stickyRaycastModel.OnSetBelowSlopeAngleToBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            stickyRaycastModel.OnSetCastFromLeftWithLeftDistanceLtRightDistance();
        }

        public void ResetStickyRaycastState()
        {
            stickyRaycastModel.OnResetState();
        }

        #endregion

        #region left stickyraycast model

        public void SetLeftStickyRaycastLength()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastLength();
        }

        public void SetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastLengthToStickyRaycastLength();
        }

        public async UniTaskVoid SetLeftStickyRaycastOriginX()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftStickyRaycastOriginY()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftStickyRaycast()
        {
            leftStickyRaycastModel.OnSetLeftStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleLeft()
        {
            leftStickyRaycastModel.OnSetBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleLeft()
        {
            leftStickyRaycastModel.OnSetCrossBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetBelowSlopeAngleLeftToNegative()
        {
            leftStickyRaycastModel.OnSetBelowSlopeAngleLeftToNegative();
        }

        #endregion

        #region right stickyraycast model

        public void SetRightStickyRaycastLengthToStickyRaycastLength()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastLengthToStickyRaycastLength();
        }

        public void SetRightStickyRaycastLength()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastLength();
        }

        public async UniTaskVoid SetRightStickyRaycastOriginY()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightStickyRaycastOriginX()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightStickyRaycast()
        {
            rightStickyRaycastModel.OnSetRightStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleRight()
        {
            rightStickyRaycastModel.OnSetBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleRight()
        {
            rightStickyRaycastModel.OnSetCrossBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetBelowSlopeAngleRightToNegative()
        {
            rightStickyRaycastModel.OnSetBelowSlopeAngleRightToNegative();
        }

        #endregion
    }
}
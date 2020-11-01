using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
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
    using static ScriptableObjectExtensions;
    using static UniTaskExtensions;

    [RequireComponent(typeof(BoxcastController))]
    public class RaycastController : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastModel raycastModel;
        [SerializeField] private UpRaycastModel upRaycastModel;
        [SerializeField] private RightRaycastModel rightRaycastModel;
        [SerializeField] private DownRaycastModel downRaycastModel;
        [SerializeField] private LeftRaycastModel leftRaycastModel;
        [SerializeField] private DistanceToGroundRaycastModel distanceToGroundRaycastModel;
        //[SerializeField] private StickyRaycastModel stickyRaycastModel;
        //[SerializeField] private LeftStickyRaycastModel leftStickyRaycastModel;
        //[SerializeField] private RightStickyRaycastModel rightStickyRaycastModel;

        /* fields */
        private RaycastModel[] models;

        /* fields: methods */
        private async void Awake()
        {
            GetModels();
            Async(raycastModel.OnInitialize());
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
            /*
            if (!stickyRaycastModel) stickyRaycastModel = LoadModel<StickyRaycastModel>(StickyRaycastModelPath);
            if (!rightStickyRaycastModel)
                rightStickyRaycastModel = LoadModel<RightStickyRaycastModel>(RightStickyRaycastModelPath);
            if (!leftStickyRaycastModel)
                leftStickyRaycastModel = LoadModel<LeftStickyRaycastModel>(LeftStickyRaycastModelPath);*/
        }

        /* properties: methods */

        #region raycast model

        public async UniTaskVoid SetRaysParameters()
        {
            raycastModel.OnSetRaysParameters();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid ResetRaycastState()
        {
            raycastModel.OnResetState();
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
        /*
        #region sticky raycast model

        public void SetStickyRaycastLength()
        {
            stickyRaycastModel.OnSetStickyRaycastLength();
        }

        public void SetStickyRaycastLengthToSelf()
        {
            stickyRaycastModel.OnSetStickyRaycastLengthToSelf();
        }

        public void SetDoNotCastFromLeft()
        {
            stickyRaycastModel.OnSetDoNotCastFromLeft();
        }

        public void InitializeBelowSlopeAngle()
        {
            stickyRaycastModel.OnInitializeBelowSlopeAngle();
        }

        public void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public void SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLtZero();
        }

        public void SetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            stickyRaycastModel.OnSetBelowSlopeAngleToBelowSlopeAngleLeft();
        }

        public void SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleRightLtZero();
        }

        public void SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            stickyRaycastModel.OnSetBelowSlopeAngleToBelowSlopeAngleRight();
        }

        public void SetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero();
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
        */
    }
}
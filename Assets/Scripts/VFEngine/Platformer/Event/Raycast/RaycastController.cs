using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Platformer.Event.Raycast.DownRaycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Platformer.Event.Raycast.UpRaycast;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
namespace VFEngine.Platformer.Event.Raycast
{
    using static UniTaskExtensions;

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private RaycastModel raycastModel;
        [SerializeField] private UpRaycastModel upRaycastModel;
        [SerializeField] private RightRaycastModel rightRaycastModel;
        [SerializeField] private DownRaycastModel downRaycastModel;
        [SerializeField] private LeftRaycastModel leftRaycastModel;
        [SerializeField] private DistanceToGroundRaycastModel distanceToGroundRaycastModel;
        [SerializeField] private StickyRaycastModel stickyRaycastModel;
        [SerializeField] private LeftStickyRaycastModel leftStickyRaycastModel;
        [SerializeField] private RightStickyRaycastModel rightStickyRaycastModel;

        #endregion

        #region private methods

        private void PlatformerInitializeData()
        {
            LoadCharacter();
            LoadRaycastModel();
            LoadUpRaycastModel();
            LoadRightRaycastModel();
            LoadDownRaycastModel();
            LoadLeftRaycastModel();
            LoadDistanceToGroundRaycastModel();
            LoadStickyRaycastModel();
            LoadRightStickyRaycastModel();
            LoadLeftStickyRaycastModel();
            InitializeRaycastData();
            InitializeUpRaycastData();
            InitializeRightRaycastData();
            InitializeDownRaycastData();
            InitializeLeftRaycastData();
            InitializeDistanceToGroundRaycastData();
            InitializeStickyRaycastData();
            InitializeRightStickyRaycastData();
            InitializeLeftStickyRaycastData();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void LoadRaycastModel()
        {
            raycastModel = new RaycastModel();
        }

        private void LoadUpRaycastModel()
        {
            raycastModel = new RaycastModel();
        }

        private void LoadRightRaycastModel()
        {
            rightRaycastModel = new RightRaycastModel();
        }

        private void LoadDownRaycastModel()
        {
            downRaycastModel = new DownRaycastModel();
        }

        private void LoadLeftRaycastModel()
        {
            leftRaycastModel = new LeftRaycastModel();
        }

        private void LoadDistanceToGroundRaycastModel()
        {
            distanceToGroundRaycastModel = new DistanceToGroundRaycastModel();
        }

        private void LoadStickyRaycastModel()
        {
            stickyRaycastModel = new StickyRaycastModel();
        }

        private void LoadRightStickyRaycastModel()
        {
            rightStickyRaycastModel = new RightStickyRaycastModel();
        }

        private void LoadLeftStickyRaycastModel()
        {
            leftStickyRaycastModel = new LeftStickyRaycastModel();
        }

        private void InitializeRaycastData()
        {
            raycastModel.OnInitializeData();
        }

        private void InitializeUpRaycastData()
        {
            upRaycastModel.OnInitializeData();
        }

        private void InitializeRightRaycastData()
        {
            rightRaycastModel.OnInitializeData();
        }

        private void InitializeDownRaycastData()
        {
            downRaycastModel.OnInitializeData();
        }

        private void InitializeLeftRaycastData()
        {
            leftRaycastModel.OnInitializeData();
        }

        private void InitializeDistanceToGroundRaycastData()
        {
            distanceToGroundRaycastModel.OnInitializeData();
        }

        private void InitializeStickyRaycastData()
        {
            stickyRaycastModel.OnInitializeData();
        }

        private void InitializeRightStickyRaycastData()
        {
            rightStickyRaycastModel.OnInitializeData();
        }

        private void InitializeLeftStickyRaycastData()
        {
            leftStickyRaycastModel.OnInitializeData();
        }

        #endregion

        #endregion

        #region properties

        public GameObject Character => character;
        public RaycastModel RaycastModel => raycastModel;
        public DistanceToGroundRaycastModel DistanceToGroundRaycastModel => distanceToGroundRaycastModel;
        public DownRaycastModel DownRaycastModel => downRaycastModel;
        public LeftRaycastModel LeftRaycastModel => leftRaycastModel;
        public RightRaycastModel RightRaycastModel => rightRaycastModel;
        public StickyRaycastModel StickyRaycastModel => stickyRaycastModel;
        public RightStickyRaycastModel RightStickyRaycastModel => rightStickyRaycastModel;
        public LeftStickyRaycastModel LeftStickyRaycastModel => leftStickyRaycastModel;
        public UpRaycastModel UpRaycastModel => upRaycastModel;

        #region public methods

        #region platformer

        public void OnPlatformerInitializeData()
        {
            PlatformerInitializeData();
        }

        #endregion

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

        /*public async UniTaskVoid InitializeSmallestDistanceToDownHit()
        {
            downRaycastModel.OnInitializeSmallestDistanceToDownHit();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSmallestDistanceToDownHitDistance()
        {
            downRaycastModel.OnSetSmallestDistanceToDownHitDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/
        /*public void SetDistanceBetweenDownRaycastsAndSmallestDistancePoint()
        {
            downRaycastModel.OnSetDistanceBetweenDownRaycastsAndSmallestDistancePoint();
        }*/

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

        public void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            stickyRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleRightLtZero();
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

        public async UniTaskVoid ResetStickyRaycastState()
        {
            stickyRaycastModel.OnResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        #endregion

        #endregion

        #endregion
    }
}
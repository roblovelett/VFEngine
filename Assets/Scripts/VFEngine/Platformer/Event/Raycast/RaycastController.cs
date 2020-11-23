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
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable UnusedVariable
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

    public class RaycastController : MonoBehaviour
    {
        #region fields

        #region dependencies

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

        private void Awake()
        {
            InitializeData();
        }

        private void Start()
        {
            Async(InitializeModels());
        }

        private void InitializeData()
        {
            Async(LoadModels());
            Async(InitializeModelsData());
        }

        private async UniTaskVoid LoadModels()
        {
            var t1 = Async(LoadRaycastModel());
            var t2 = Async(LoadUpRaycastModel());
            var t3 = Async(LoadRightRaycastModel());
            var t4 = Async(LoadDownRaycastModel());
            var t5 = Async(LoadLeftRaycastModel());
            var t6 = Async(LoadDistanceToGroundRaycastModel());
            var t7 = Async(LoadStickyRaycastModel());
            var t8 = Async(LoadRightStickyRaycastModel());
            var t9 = Async(LoadLeftStickyRaycastModel());
            var task1 = await (t1, t2, t3, t4, t5, t6, t7, t8, t9);
            await SetYieldOrSwitchToThreadPoolAsync();

            async UniTaskVoid LoadRaycastModel()
            {
                if (!raycastModel) raycastModel = LoadModel<RaycastModel>(RaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadUpRaycastModel()
            {
                if (!upRaycastModel) upRaycastModel = LoadModel<UpRaycastModel>(UpRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadRightRaycastModel()
            {
                if (!rightRaycastModel) rightRaycastModel = LoadModel<RightRaycastModel>(RightRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadDownRaycastModel()
            {
                if (!downRaycastModel) downRaycastModel = LoadModel<DownRaycastModel>(DownRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadLeftRaycastModel()
            {
                if (!leftRaycastModel) leftRaycastModel = LoadModel<LeftRaycastModel>(LeftRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadDistanceToGroundRaycastModel()
            {
                if (!distanceToGroundRaycastModel)
                    distanceToGroundRaycastModel =
                        LoadModel<DistanceToGroundRaycastModel>(DistanceToGroundRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadStickyRaycastModel()
            {
                if (!stickyRaycastModel) stickyRaycastModel = LoadModel<StickyRaycastModel>(StickyRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadRightStickyRaycastModel()
            {
                if (!rightStickyRaycastModel)
                    rightStickyRaycastModel = LoadModel<RightStickyRaycastModel>(RightStickyRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }

            async UniTaskVoid LoadLeftStickyRaycastModel()
            {
                if (!leftStickyRaycastModel)
                    leftStickyRaycastModel = LoadModel<LeftStickyRaycastModel>(LeftStickyRaycastModelPath);
                await SetYieldOrSwitchToThreadPoolAsync();
            }
        }

        private async UniTaskVoid InitializeModelsData()
        {
            var t1 = Async(raycastModel.OnInitializeData());
            var t3 = Async(upRaycastModel.OnInitializeData());
            var t4 = Async(rightRaycastModel.OnInitializeData());
            var t5 = Async(downRaycastModel.OnInitializeData());
            var t6 = Async(leftRaycastModel.OnInitializeData());
            var t2 = Async(distanceToGroundRaycastModel.OnInitializeData());
            var t7 = Async(stickyRaycastModel.OnInitializeData());
            var t8 = Async(rightStickyRaycastModel.OnInitializeData());
            var t9 = Async(leftStickyRaycastModel.OnInitializeData());
            var task1 = await (t1, t2, t3, t4, t5, t6, t7, t8, t9);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        private async UniTaskVoid InitializeModels()
        {
            var t1 = Async(raycastModel.OnInitializeModel());
            var t2 = Async(distanceToGroundRaycastModel.OnInitializeModel());
            var t3 = Async(upRaycastModel.OnInitializeModel());
            var t4 = Async(rightRaycastModel.OnInitializeModel());
            var t5 = Async(downRaycastModel.OnInitializeModel());
            var t6 = Async(leftRaycastModel.OnInitializeModel());
            var t7 = Async(stickyRaycastModel.OnInitializeModel());
            var t8 = Async(rightStickyRaycastModel.OnInitializeModel());
            var t9 = Async(leftStickyRaycastModel.OnInitializeModel());
            var task1 = await (t1, t2, t3, t4, t5, t6, t7, t8, t9);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion

        #region properties

        public RaycastRuntimeData RaycastRuntimeData => raycastModel.RuntimeData;
        public DistanceToGroundRaycastRuntimeData DistanceToGroundRaycastRuntimeData => distanceToGroundRaycastModel.RuntimeData;
        public DownRaycastRuntimeData DownRaycastRuntimeData => downRaycastModel.RuntimeData;
        public LeftRaycastRuntimeData LeftRaycastRuntimeData => leftRaycastModel.RuntimeData;
        public RightRaycastRuntimeData RightRaycastRuntimeData => rightRaycastModel.RuntimeData;
        public StickyRaycastRuntimeData StickyRaycastRuntimeData => stickyRaycastModel.RuntimeData;
        public RightStickyRaycastRuntimeData RightStickyRaycastRuntimeData => rightStickyRaycastModel.RuntimeData;
        public LeftStickyRaycastRuntimeData LeftStickyRaycastRuntimeData => leftStickyRaycastModel.RuntimeData;
        public UpRaycastRuntimeData UpRaycastRuntimeData => upRaycastModel.RuntimeData;
        
        #region public methods

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
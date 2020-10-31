using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static Debug;
    using static StickyRaycastData;
    using static UniTaskExtensions;

    public class StickyRaycastController : MonoBehaviour, IController
    {
        [SerializeField] private StickyRaycastModel model;
        [SerializeField] private StickyRaycastModel leftRaycastModel;
        [SerializeField] private StickyRaycastModel rightRaycastModel;
        private StickyRaycastModel[] models;

        private async void Awake()
        {
            GetModels();
            var srTask1 = Async(leftRaycastModel.OnInitialize());
            var srTask2 = Async(rightRaycastModel.OnInitialize());
            var task1 = await (srTask1, srTask2);
        }

        private void GetModels()
        {
            models = new[] {model, leftRaycastModel, rightRaycastModel};
            var names = new[] {"model, leftRaycastModel, rightRaycastModel"};
            for (var i = 0; i < models.Length; i++)
            {
                if (models[i]) continue;
                models[i] = LoadData(ModelPath) as StickyRaycastModel;
                switch (i)
                {
                    case 0:
                        model = models[i];
                        break;
                    case 1:
                        leftRaycastModel = models[i];
                        break;
                    case 2:
                        rightRaycastModel = models[i];
                        break;
                }

                Assert(models[i] != null, names[i] + " != null");
            }
        }

        public async UniTaskVoid SetStickyRaycastLength()
        {
            var srTask1 = Async(leftRaycastModel.OnSetStickyRaycastLength());
            var srTask2 = Async(rightRaycastModel.OnSetStickyRaycastLength());
            var task1 = await (srTask1, srTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetStickyRaycastLengthToSelf()
        {
            var srTask1 = Async(leftRaycastModel.OnSetStickyRaycastLengthToSelf());
            var srTask2 = Async(rightRaycastModel.OnSetStickyRaycastLengthToSelf());
            var task1 = await (srTask1, srTask2);
            await SetYieldOrSwitchToThreadPoolAsync();
        }
        public void SetLeftStickyRaycastLength()
        {
            leftRaycastModel.OnSetLeftStickyRaycastLength();
        }

        public void SetLeftStickyRaycastLengthToStickyRaycastLength()
        {
            leftRaycastModel.OnSetLeftStickyRaycastLengthToStickyRaycastLength();
        }

        public void SetRightStickyRaycastLengthToStickyRaycastLength()
        {
            rightRaycastModel.OnSetRightStickyRaycastLengthToStickyRaycastLength();
        }

        public void SetRightStickyRaycastLength()
        {
            rightRaycastModel.OnSetRightStickyRaycastLength();
        }

        public async UniTaskVoid SetLeftStickyRaycastOriginY()
        {
            leftRaycastModel.OnSetLeftStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftStickyRaycastOriginX()
        {
            leftRaycastModel.OnSetLeftStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightStickyRaycastOriginY()
        {
            rightRaycastModel.OnSetRightStickyRaycastOriginY();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightStickyRaycastOriginX()
        {
            rightRaycastModel.OnSetRightStickyRaycastOriginX();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetLeftStickyRaycast()
        {
            leftRaycastModel.OnSetLeftStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetRightStickyRaycast()
        {
            rightRaycastModel.OnSetRightStickyRaycast();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetDoNotCastFromLeft()
        {
            leftRaycastModel.OnSetDoNotCastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeBelowSlopeAngle()
        {
            rightRaycastModel.OnInitializeBelowSlopeAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleLeft()
        {
            leftRaycastModel.OnSetBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleRight()
        {
            rightRaycastModel.OnSetBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleLeft()
        {
            leftRaycastModel.OnSetCrossBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCrossBelowSlopeAngleRight()
        {
            rightRaycastModel.OnSetCrossBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetBelowSlopeAngleLeftToNegative()
        {
            leftRaycastModel.OnSetBelowSlopeAngleLeftToNegative();
        }

        public void SetBelowSlopeAngleRightToNegative()
        {
            rightRaycastModel.OnSetBelowSlopeAngleRightToNegative();
        }

        public void SetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight()
        {
            leftRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftGtBelowSlopeAngleRight();
        }

        public async UniTaskVoid SetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            leftRaycastModel.OnSetBelowSlopeAngleToBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleLtZero()
        {
            leftRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleRightLtZero()
        {
            leftRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleRightLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            rightRaycastModel.OnSetBelowSlopeAngleToBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetCastFromLeftWithBelowSlopeAngleLeftLtZero()
        {
            leftRaycastModel.OnSetCastFromLeftWithBelowSlopeAngleLeftLtZero();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetCastFromLeftWithLeftDistanceLtRightDistance()
        {
            leftRaycastModel.OnSetCastFromLeftWithLeftDistanceLtRightDistance();
        }

        public void ResetState()
        {
            model.OnResetState();
        }
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Event.Raycast
{
    using static Debug;
    using static RaycastData;
    using static ScriptableObjectExtensions;
    using static RaycastDirection;
    using static UniTaskExtensions;

    [RequireComponent(typeof(StickyRaycastController))]
    [RequireComponent(typeof(SafetyBoxcastController))]
    public class RaycastController : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastModel upRaycastModel;
        [SerializeField] private RaycastModel rightRaycastModel;
        [SerializeField] private RaycastModel downRaycastModel;
        [SerializeField] private RaycastModel leftRaycastModel;

        /* fields */
        private RaycastModel[] models;

        /* fields: methods */
        private async void Awake()
        {
            GetModels();
            var rTask1 = Async(upRaycastModel.Initialize(Up));
            var rTask2 = Async(rightRaycastModel.Initialize(Right));
            var rTask3 = Async(downRaycastModel.Initialize(Down));
            var rTask4 = Async(leftRaycastModel.Initialize(Left));
            var rTask = await (rTask1, rTask2, rTask3, rTask4);
        }

        private void GetModels()
        {
            models = new[] {upRaycastModel, rightRaycastModel, downRaycastModel, leftRaycastModel};
            var names = new[] {"upRaycastModel", "rightRaycastModel", "downRaycastModel", "leftRaycastModel"};
            for (var i = 0; i < models.Length; i++)
            {
                if (models[i]) continue;
                models[i] = LoadData(ModelPath) as RaycastModel;
                switch (i)
                {
                    case 0:
                        upRaycastModel = models[i];
                        break;
                    case 1:
                        rightRaycastModel = models[i];
                        break;
                    case 2:
                        downRaycastModel = models[i];
                        break;
                    case 3:
                        leftRaycastModel = models[i];
                        break;
                }

                Assert(models[i] != null, names[i] + " != null");
            }
        }
        
        /* properties: methods */
        public async UniTaskVoid SetRaysParameters()
        {
            var rTask1 = Async(upRaycastModel.OnSetRaysParameters());
            var rTask2 = Async(rightRaycastModel.OnSetRaysParameters());
            var rTask3 = Async(downRaycastModel.OnSetRaysParameters());
            var rTask4 = Async(leftRaycastModel.OnSetRaysParameters());
            var rTask = await (rTask1, rTask2, rTask3, rTask4);
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        /*public void SetCurrentRightRaycastToIgnoreOneWayPlatform()
        {
            rightRaycastModel.OnSetCurrentRightRaycastToIgnoreOneWayPlatform();
        }

        public void SetCurrentLeftRaycastToIgnoreOneWayPlatform()
        {
            leftRaycastModel.OnSetCurrentLeftRaycastToIgnoreOneWayPlatform();
        }*/

        public void SetCurrentDownRaycastToIgnoreOneWayPlatform()
        {
            downRaycastModel.OnSetCurrentDownRaycastToIgnoreOneWayPlatform();
        }

        /*public void SetCurrentRightRaycast()
        {
            rightRaycastModel.OnSetCurrentRightRaycast();
        }

        public void SetCurrentLeftRaycast()
        {
            leftRaycastModel.OnSetCurrentLeftRaycast();
        }*/

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

        public async UniTaskVoid SetVerticalRaycastFromLeft()
        {
            downRaycastModel.OnSetVerticalRaycastFromLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetVerticalRaycastToRight()
        {
            downRaycastModel.OnSetVerticalRaycastToRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid InitializeSmallestDistance()
        {
            downRaycastModel.OnInitializeSmallestDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid SetSmallestDistanceToDownHitDistance()
        {
            downRaycastModel.OnSetSmallestDistanceToDownHitDistance();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void SetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint()
        {
            downRaycastModel.OnSetDistanceBetweenVerticalRaycastsAndSmallestDistanceDownRaycastPoint();
        }
        
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

        public void SetCurrentUpRaycastOriginPoint()
        {
            upRaycastModel.OnSetCurrentUpRaycastOriginPoint();
        }

        public void SetCurrentUpRaycast()
        {
            upRaycastModel.OnSetCurrentUpRaycast();
        }

        public void SetUpRaycastSmallestDistanceToRaycastUpHitAt()
        {
            upRaycastModel.OnSetUpRaycastSmallestDistanceToRaycastUpHitAt();
        }
        
        public void SetRightRaycastFromBottomOrigin()
        {
            rightRaycastModel.OnSetRightRaycastFromBottomOrigin();
        }

        public void SetLeftRaycastFromBottomOrigin()
        {
            leftRaycastModel.OnSetLeftRaycastFromBottomOrigin();
        }

        public void SetRightRaycastToTopOrigin()
        {
            rightRaycastModel.OnSetRightRaycastToTopOrigin();
        }

        public void SetLeftRaycastToTopOrigin()
        {
            leftRaycastModel.OnSetLeftRaycastToTopOrigin();
        }

        public void InitializeRightRaycastLength()
        {
            rightRaycastModel.OnInitializeRightRaycastLength();
        }

        public void InitializeLeftRaycastLength()
        {
            leftRaycastModel.OnInitializeLeftRaycastLength();
        }
    }
}
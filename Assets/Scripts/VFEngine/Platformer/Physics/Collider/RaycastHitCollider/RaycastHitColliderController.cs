﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static Debug;
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;
    using static ColliderDirection;
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
            var rTask1 = Async(upColliderModel.Initialize(Up));
            var rTask2 = Async(rightColliderModel.Initialize(Right));
            var rTask3 = Async(downColliderModel.Initialize(Down));
            var rTask4 = Async(leftColliderModel.Initialize(Left));
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

        public async UniTaskVoid SetCurrentWallCollider()
        {
            var rhcTask1 = Async(upColliderModel.OnSetCurrentWallCollider());
            var rhcTask2 = Async(rightColliderModel.OnSetCurrentWallCollider());
            var rhcTask3 = Async(downColliderModel.OnSetCurrentWallCollider());
            var rhcTask4 = Async(leftColliderModel.OnSetCurrentWallCollider());
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

        public void InitializeRightHitsStorageHalf()
        {
            rightColliderModel.OnInitializeRightHitsStorageHalf();
        }

        public void InitializeLeftHitsStorage()
        {
            leftColliderModel.OnInitializeLeftHitsStorage();
        }

        public void InitializeLeftHitsStorageHalf()
        {
            leftColliderModel.OnInitializeLeftHitsStorageHalf();
        }

        public void SetRightHitsStorageToIgnoreOneWayPlatform()
        {
            rightColliderModel.OnSetRightHitsStorageToIgnoreOneWayPlatform();
        }

        public void SetLeftHitsStorageToIgnoreOneWayPlatform()
        {
            leftColliderModel.OnSetLeftHitsStorageToIgnoreOneWayPlatform();
        }

        public void SetRightHitsStorage()
        {
            rightColliderModel.OnSetRightHitsStorage();
        }

        public void SetLeftHitsStorage()
        {
            leftColliderModel.OnSetLeftHitsStorage();
        }
    }
}
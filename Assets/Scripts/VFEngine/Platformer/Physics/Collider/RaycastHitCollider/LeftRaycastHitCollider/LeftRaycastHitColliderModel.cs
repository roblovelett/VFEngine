﻿using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.LeftRaycastHitCollider
{
    using static RaycastHitColliderModel;
    using static UniTaskExtensions;
    using static MathsExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LeftRaycastHitColliderModel", menuName = PlatformerLeftRaycastHitColliderModelPath,
        order = 0)]
    [InlineEditor]
    public class LeftRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private LeftRaycastHitColliderData l = null;

        #endregion

        #region private methods

        private void InitializeData()
        {
            l = new LeftRaycastHitColliderData {Character = character};
            /*l.RuntimeData = l.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .LeftRaycastHitColliderRuntimeData;
            l.LeftHitsStorageLength = l.LeftHitsStorage.Length;
            l.RuntimeData.SetLeftRaycastHitCollider(l.LeftHitConnected, l.IsCollidingLeft, l.LeftHitsStorageLength,
                l.CurrentLeftHitsStorageIndex, l.CurrentLeftHitAngle, l.CurrentLeftHitDistance,
                l.DistanceBetweenLeftHitAndRaycastOrigin, l.CurrentLeftHitCollider);*/
        }

        private void InitializeModel()
        {
            /*l.PlatformerRuntimeData = l.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            l.RaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().RuntimeData;
            l.LeftRaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().LeftRaycastRuntimeData;
            l.Transform = l.PlatformerRuntimeData.platformer.Transform;
            l.NumberOfHorizontalRaysPerSide = l.RaycastRuntimeData.raycast.NumberOfHorizontalRaysPerSide;
            l.CurrentLeftRaycastHit = l.LeftRaycastRuntimeData.leftRaycast.CurrentLeftRaycastHit;
            l.LeftRaycastFromBottomOrigin = l.LeftRaycastRuntimeData.leftRaycast.LeftRaycastFromBottomOrigin;
            l.LeftRaycastToTopOrigin = l.LeftRaycastRuntimeData.leftRaycast.LeftRaycastToTopOrigin;
            InitializeLeftHitsStorage();
            InitializeCurrentLeftHitsStorageIndex();
            ResetState();*/
        }

        private void InitializeLeftHitsStorage()
        {
            l.LeftHitsStorage = new RaycastHit2D[l.NumberOfHorizontalRaysPerSide];
        }

        private void InitializeCurrentLeftHitsStorageIndex()
        {
            l.CurrentLeftHitsStorageIndex = 0;
        }

        private void SetCurrentLeftHitsStorage()
        {
            l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex] = l.CurrentLeftRaycastHit;
        }

        private void SetCurrentLeftHitDistance()
        {
            l.CurrentLeftHitDistance = l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].distance;
        }

        private void SetLeftRaycastHitConnected()
        {
            l.LeftHitConnected = true;
        }

        private void SetLeftRaycastHitMissed()
        {
            l.LeftHitConnected = false;
        }

        private void SetCurrentLeftHitAngle()
        {
            l.CurrentLeftHitAngle = OnSetRaycastHitAngle(l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].normal,
                l.Transform);
        }

        private void SetCurrentLeftHitCollider()
        {
            l.CurrentLeftHitCollider = l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].collider;
        }

        private void SetCurrentLeftLateralSlopeAngle()
        {
            l.LeftLateralSlopeAngle = l.CurrentLeftHitAngle;
        }

        private void SetIsCollidingLeft()
        {
            l.IsCollidingLeft = true;
        }

        private void SetLeftDistanceToLeftCollider()
        {
            l.DistanceToLeftCollider = l.CurrentLeftHitAngle;
        }

        private void SetLeftCurrentWallCollider()
        {
            l.CurrentLeftWallCollider = l.CurrentLeftHitCollider.gameObject;
        }

        private void SetCurrentWallColliderNull()
        {
            l.CurrentLeftWallCollider = null;
        }

        private void SetLeftFailedSlopeAngle()
        {
            l.PassedLeftSlopeAngle = false;
        }

        private void SetCurrentDistanceBetweenLeftHitAndRaycastOrigin()
        {
            l.DistanceBetweenLeftHitAndRaycastOrigin = DistanceBetweenPointAndLine(
                l.LeftHitsStorage[l.CurrentLeftHitsStorageIndex].point, l.LeftRaycastFromBottomOrigin,
                l.LeftRaycastToTopOrigin);
        }

        private void AddToCurrentLeftHitsStorageIndex()
        {
            l.CurrentLeftHitsStorageIndex++;
        }

        private void ResetState()
        {
            l.LeftHitConnected = false;
            l.PassedLeftSlopeAngle = false;
            l.IsCollidingLeft = false;
            l.CurrentLeftHitCollider = null;
            l.CurrentLeftWallCollider = null;
            l.CurrentLeftHitAngle = 0f;
            l.LeftLateralSlopeAngle = 0f;
            l.DistanceToLeftCollider = -1f;
        }

        #endregion

        #endregion

        #region properties

        public LeftRaycastHitColliderRuntimeData RuntimeData => l.RuntimeData;

        #region public methods

        public void OnInitializeLeftHitsStorage()
        {
            InitializeLeftHitsStorage();
        }

        public void OnInitializeCurrentLeftHitsStorageIndex()
        {
            InitializeCurrentLeftHitsStorageIndex();
        }

        public void OnSetCurrentLeftHitsStorage()
        {
            SetCurrentLeftHitsStorage();
        }

        public void OnSetCurrentLeftHitAngle()
        {
            SetCurrentLeftHitAngle();
        }

        public void OnSetIsCollidingLeft()
        {
            SetIsCollidingLeft();
        }

        public void OnSetLeftDistanceToLeftCollider()
        {
            SetLeftDistanceToLeftCollider();
        }

        public void OnSetLeftCurrentWallCollider()
        {
            SetLeftCurrentWallCollider();
        }

        public void OnSetCurrentWallColliderNull()
        {
            SetCurrentWallColliderNull();
        }

        public void OnSetLeftFailedSlopeAngle()
        {
            SetLeftFailedSlopeAngle();
        }

        public void OnAddToCurrentLeftHitsStorageIndex()
        {
            AddToCurrentLeftHitsStorageIndex();
        }

        public void OnSetCurrentLeftHitDistance()
        {
            SetCurrentLeftHitDistance();
        }

        public void OnSetLeftRaycastHitConnected()
        {
            SetLeftRaycastHitConnected();
        }

        public void OnSetLeftRaycastHitMissed()
        {
            SetLeftRaycastHitMissed();
        }

        public void OnSetCurrentLeftHitCollider()
        {
            SetCurrentLeftHitCollider();
        }

        public void OnSetCurrentLeftLateralSlopeAngle()
        {
            SetCurrentLeftLateralSlopeAngle();
        }

        public void OnSetCurrentDistanceBetweenLeftHitAndRaycastOrigin()
        {
            SetCurrentDistanceBetweenLeftHitAndRaycastOrigin();
        }

        public void OnResetState()
        {
            ResetState();
        }

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
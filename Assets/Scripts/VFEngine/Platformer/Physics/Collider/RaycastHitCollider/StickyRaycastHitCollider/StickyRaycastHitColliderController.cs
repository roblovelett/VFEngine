﻿using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    using static UniTaskExtensions;

    public class StickyRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private RightStickyRaycastHitColliderController rightStickyRaycastHitColliderController;
        private LeftStickyRaycastHitColliderController leftStickyRaycastHitColliderController;
        private StickyRaycastHitColliderData s;
        private RightStickyRaycastHitColliderData rightStickyRaycastHitCollider;
        private LeftStickyRaycastHitColliderData leftStickyRaycastHitCollider;

        #endregion

        #region private methods

        private void Awake()
        {
            LoadCharacter();
            InitializeData();
            SetControllers();
        }

        private void LoadCharacter()
        {
            if (!character) character = transform.root.gameObject;
        }

        private void InitializeData()
        {
            s = new StickyRaycastHitColliderData();
        }

        private void SetControllers()
        {
            rightStickyRaycastHitColliderController =
                character.GetComponentNoAllocation<RightStickyRaycastHitColliderController>();
            leftStickyRaycastHitColliderController =
                character.GetComponentNoAllocation<LeftStickyRaycastHitColliderController>();
        }

        private void Start()
        {
            SetDependencies();
            InitializeFrame();
        }

        private void SetDependencies()
        {
            rightStickyRaycastHitCollider = rightStickyRaycastHitColliderController.Data;
            leftStickyRaycastHitCollider = leftStickyRaycastHitColliderController.Data;
        }

        private void InitializeFrame()
        {
            InitializeBelowSlopeAngle();
        }

        private void ResetState()
        {
            InitializeBelowSlopeAngle();
        }

        private void InitializeBelowSlopeAngle()
        {
            s.BelowSlopeAngle = 0f;
        }

        private void SetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            s.BelowSlopeAngle = leftStickyRaycastHitCollider.BelowSlopeAngleLeft;
        }

        private void SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            s.BelowSlopeAngle = rightStickyRaycastHitCollider.BelowSlopeAngleRight;
        }

        #endregion

        #endregion

        #region properties

        public StickyRaycastHitColliderData Data => s;

        #region public methods

        public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeBelowSlopeAngle()
        {
            InitializeBelowSlopeAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleToBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleToBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
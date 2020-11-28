﻿using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastHitColliderModel",
        menuName = PlatformerDistanceToGroundRaycastHitColliderModelPath, order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Hit Collider Data")] [SerializeField]
        private DistanceToGroundRaycastHitColliderData d;

        [SerializeField] private GameObject character;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private RaycastController raycastController;
        private RaycastData raycast;
        private DistanceToGroundRaycastData distanceToGroundRaycast;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!d) d = CreateInstance<DistanceToGroundRaycastHitColliderData>();
            if (!raycastHitColliderController && character)
                raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            else if (raycastHitColliderController && !character) character = raycastHitColliderController.Character;
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
        }

        private void InitializeModel()
        {
            raycast = raycastController.RaycastModel.Data;
            distanceToGroundRaycast = raycastController.DistanceToGroundRaycastModel.Data;
            ResetState();
        }

        private void SetDistanceToGroundRaycastHit()
        {
            d.DistanceToGroundRaycastHitConnected = true;
        }

        private void ResetState()
        {
            d.DistanceToGroundRaycastHitConnected = false;
            InitializeDistanceToGround();
        }

        private void InitializeDistanceToGround()
        {
            d.DistanceToGround = raycast.DistanceToGroundRayMaximumLength;
        }

        private void DecreaseDistanceToGround()
        {
            d.DistanceToGround -= 1f;
        }

        private void ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            d.DistanceToGround = distanceToGroundRaycast.DistanceToGroundRaycastHit.distance - raycast.BoundsHeight / 2;
        }

        #endregion

        #endregion

        #region properties

        public DistanceToGroundRaycastHitColliderData Data => d;

        #region public methods

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetDistanceToGroundRaycastHit()
        {
            SetDistanceToGroundRaycastHit();
        }

        public void OnInitializeDistanceToGround()
        {
            InitializeDistanceToGround();
        }

        public void OnDecreaseDistanceToGround()
        {
            DecreaseDistanceToGround();
        }

        public void OnApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround();
        }

        public void OnResetState()
        {
            ResetState();
        }

        #endregion

        #endregion
    }
}
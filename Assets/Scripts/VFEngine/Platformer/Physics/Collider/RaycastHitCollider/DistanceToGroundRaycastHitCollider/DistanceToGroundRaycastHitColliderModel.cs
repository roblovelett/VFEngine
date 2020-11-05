using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    [CreateAssetMenu(fileName = "DistanceToGroundRaycastHitColliderModel",
        menuName =
            "VFEngine/Platformer/Physics/Raycast Hit Collider/Distance To Ground Raycast Hit Collider/Distance To Ground Raycast Hit Collider Model",
        order = 0)]
    public class DistanceToGroundRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Distance To Ground Raycast Hit Collider Data")] [SerializeField]
        private DistanceToGroundRaycastHitColliderData d;

        #endregion

        #region private methods

        private void SetHasDistanceToGroundRaycast()
        {
            d.HasDistanceToGroundRaycast = true;
        }

        private void ResetState()
        {
            d.HasDistanceToGroundRaycast = false;
            InitializeDistanceToGround();
        }

        private void InitializeDistanceToGround()
        {
            d.DistanceToGround = d.DistanceToGroundRayMaximumLength;
        }

        private void DecreaseDistanceToGround()
        {
            d.DistanceToGround -= 1f;
        }

        private void ApplyDistanceToGroundRaycastAndBoundsHeightToDistanceToGround()
        {
            d.DistanceToGround = d.DistanceToGroundRaycastDistance - d.BoundsHeight / 2;
        }

        #endregion

        #endregion

        #region public methods

        public void OnSetHasDistanceToGroundRaycast()
        {
            SetHasDistanceToGroundRaycast();
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
    }
}
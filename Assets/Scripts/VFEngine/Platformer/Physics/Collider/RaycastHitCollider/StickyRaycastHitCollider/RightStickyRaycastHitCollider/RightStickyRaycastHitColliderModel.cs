using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    using static Vector3;
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "StickyRaycastHitColliderModel",
        menuName = PlatformerRightStickyRaycastHitColliderModelPath, order = 0)]
    [InlineEditor]
    public class RightStickyRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Right Sticky Raycast Hit Collider Data")] [SerializeField] private RightStickyRaycastHitColliderData r;
        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        private PhysicsData physics;
        private RightStickyRaycastData rightStickyRaycast;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!r) r = CreateInstance<RightStickyRaycastHitColliderData>();
            if (!physicsController) physicsController = character.GetComponent<PhysicsController>();
            if (!raycastController) raycastController = character.GetComponent<RaycastController>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            rightStickyRaycast = raycastController.RightStickyRaycastModel.Data;
            ResetState();
        }

        private void ResetState()
        {
            r.BelowSlopeAngleRight = 0f;
            r.CrossBelowSlopeAngleRight = zero;
        }

        private void SetBelowSlopeAngleRight()
        {
            r.BelowSlopeAngleRight =
                Vector2.Angle(rightStickyRaycast.RightStickyRaycastHit.normal, physics.Transform.up);
        }

        private void SetCrossBelowSlopeAngleRight()
        {
            r.CrossBelowSlopeAngleRight = Cross(physics.Transform.up, rightStickyRaycast.RightStickyRaycastHit.normal);
        }

        private void SetBelowSlopeAngleRightToNegative()
        {
            r.BelowSlopeAngleRight = -r.BelowSlopeAngleRight;
        }

        #endregion

        #endregion

        #region properties

        public RightStickyRaycastHitColliderData Data => r;

        #region public methods

        public async UniTaskVoid OnInitializeData()
        {
            InitializeData();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnResetState()
        {
            ResetState();
        }

        public void OnSetBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleRight();
        }

        public void OnSetCrossBelowSlopeAngleRight()
        {
            SetCrossBelowSlopeAngleRight();
        }

        public void OnSetBelowSlopeAngleRightToNegative()
        {
            SetBelowSlopeAngleRightToNegative();
        }

        #endregion

        #endregion
    }
}
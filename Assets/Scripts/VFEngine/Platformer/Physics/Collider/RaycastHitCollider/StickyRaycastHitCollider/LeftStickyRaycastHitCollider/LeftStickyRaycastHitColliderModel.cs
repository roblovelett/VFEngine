using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    using static Vector3;
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "StickyRaycastHitColliderModel",
        menuName = PlatformerLeftStickyRaycastHitColliderModelPath, order = 0)]
    [InlineEditor]
    public class LeftStickyRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private LeftStickyRaycastHitColliderData l;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private RaycastController raycastController;
        private PhysicsData physics;
        private LeftStickyRaycastData leftStickyRaycast;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!l) l = CreateInstance<LeftStickyRaycastHitColliderData>();
        }

        private void InitializeModel()
        {
            physics = physicsController.PhysicsModel.Data;
            leftStickyRaycast = raycastController.LeftStickyRaycastModel.Data;
            ResetState();
        }

        private void ResetState()
        {
            l.BelowSlopeAngleLeft = 0f;
            l.CrossBelowSlopeAngleLeft = zero;
        }

        private void SetBelowSlopeAngleLeft()
        {
            l.BelowSlopeAngleLeft = Vector2.Angle(leftStickyRaycast.LeftStickyRaycastHit.normal, physics.Transform.up);
        }

        private void SetCrossBelowSlopeAngleLeft()
        {
            l.CrossBelowSlopeAngleLeft = Cross(physics.Transform.up, leftStickyRaycast.LeftStickyRaycastHit.normal);
        }

        private void SetBelowSlopeAngleLeftToNegative()
        {
            l.BelowSlopeAngleLeft = -l.BelowSlopeAngleLeft;
        }

        #endregion

        #endregion

        #region properties

        public LeftStickyRaycastHitColliderData Data => l;

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

        public void OnSetBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleLeft();
        }

        public void OnSetCrossBelowSlopeAngleLeft()
        {
            SetCrossBelowSlopeAngleLeft();
        }

        public void OnSetBelowSlopeAngleLeftToNegative()
        {
            SetBelowSlopeAngleLeftToNegative();
        }

        public void OnResetState()
        {
            ResetState();
        }

        #endregion

        #endregion
    }
}
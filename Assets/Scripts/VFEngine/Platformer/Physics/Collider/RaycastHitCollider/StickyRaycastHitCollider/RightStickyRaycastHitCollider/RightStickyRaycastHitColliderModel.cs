using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
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

        [SerializeField] private RightStickyRaycastHitColliderData r;

        #endregion

        #region private methods

        private void InitializeData()
        {
            r = new RightStickyRaycastHitColliderData {Character = character};
            r.RuntimeData =
                RightStickyRaycastHitColliderRuntimeData.CreateInstance(r.BelowSlopeAngleRight,
                    r.CrossBelowSlopeAngleRight);
        }

        private void InitializeModel()
        {
            r.PhysicsRuntimeData = r.Character.GetComponentNoAllocation<PhysicsController>().PhysicsRuntimeData;
            r.RightStickyRaycastRuntimeData = r.Character.GetComponentNoAllocation<RaycastController>()
                .RightStickyRaycastRuntimeData;
            r.Transform = r.PhysicsRuntimeData.Transform;
            r.RightStickyRaycastHit = r.RightStickyRaycastRuntimeData.RightStickyRaycastHit;
            ResetState();
        }

        private void ResetState()
        {
            r.BelowSlopeAngleRight = 0f;
            r.CrossBelowSlopeAngleRight = zero;
        }

        private void SetBelowSlopeAngleRight()
        {
            r.BelowSlopeAngleRight = Vector2.Angle(r.RightStickyRaycastHit.normal, r.Transform.up);
        }

        private void SetCrossBelowSlopeAngleRight()
        {
            r.CrossBelowSlopeAngleRight = Cross(r.Transform.up, r.RightStickyRaycastHit.normal);
        }

        private void SetBelowSlopeAngleRightToNegative()
        {
            r.BelowSlopeAngleRight = -r.BelowSlopeAngleRight;
        }

        #endregion

        #endregion

        #region properties

        public RightStickyRaycastHitColliderRuntimeData RuntimeData => r.RuntimeData;

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
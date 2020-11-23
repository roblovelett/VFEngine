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

        [LabelText("Right Sticky Raycast Hit Collider Data")] [SerializeField]
        private RightStickyRaycastHitColliderData r = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            r.RuntimeData = r.Character.GetComponentNoAllocation<RaycastHitColliderController>()
                .RightStickyRaycastHitColliderRuntimeData;
            r.RuntimeData.SetRightStickyRaycastHitCollider(r.BelowSlopeAngleRight, r.CrossBelowSlopeAngleRight);
        }

        private void InitializeModel()
        {
            r.PlatformerRuntimeData = r.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            r.RightStickyRaycastRuntimeData = r.Character.GetComponentNoAllocation<RaycastController>()
                .RightStickyRaycastRuntimeData;
            r.Transform = r.PlatformerRuntimeData.platformer.Transform;
            r.RightStickyRaycastHit = r.RightStickyRaycastRuntimeData.rightStickyRaycast.RightStickyRaycastHit;
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

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
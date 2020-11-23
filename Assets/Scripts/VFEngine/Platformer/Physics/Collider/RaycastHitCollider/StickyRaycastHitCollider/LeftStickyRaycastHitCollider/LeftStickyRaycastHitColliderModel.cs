using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
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

        [SerializeField] private GameObject character;
        private LeftStickyRaycastHitColliderData l = null;

        #endregion

        #region private methods

        private void InitializeData()
        {
            l = new LeftStickyRaycastHitColliderData {Character = character};
            l.RuntimeData = LeftStickyRaycastHitColliderRuntimeData.CreateInstance(l.BelowSlopeAngleLeft, l.CrossBelowSlopeAngleLeft);
        }

        private void InitializeModel()
        {
            l.PhysicsRuntimeData = l.Character.GetComponentNoAllocation<PhysicsController>().PhysicsRuntimeData;
            l.LeftStickyRaycastRuntimeData = l.Character.GetComponentNoAllocation<RaycastController>().LeftStickyRaycastRuntimeData;
            l.Transform = l.PhysicsRuntimeData.Transform;
            l.LeftStickyRaycastHit = l.LeftStickyRaycastRuntimeData.LeftStickyRaycastHit;
            ResetState();
        }

        private void ResetState()
        {
            l.BelowSlopeAngleLeft = 0f;
            l.CrossBelowSlopeAngleLeft = zero;
        }

        private void SetBelowSlopeAngleLeft()
        {
            l.BelowSlopeAngleLeft = Vector2.Angle(l.LeftStickyRaycastHit.normal, l.Transform.up);
        }

        private void SetCrossBelowSlopeAngleLeft()
        {
            l.CrossBelowSlopeAngleLeft = Cross(l.Transform.up, l.LeftStickyRaycastHit.normal);
        }

        private void SetBelowSlopeAngleLeftToNegative()
        {
            l.BelowSlopeAngleLeft = -l.BelowSlopeAngleLeft;
        }

        #endregion

        #endregion

        #region properties

        public LeftStickyRaycastHitColliderRuntimeData RuntimeData => l.RuntimeData;

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
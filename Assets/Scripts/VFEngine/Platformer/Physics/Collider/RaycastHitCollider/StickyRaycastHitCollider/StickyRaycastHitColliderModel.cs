using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    using static UniTaskExtensions;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "StickyRaycastHitColliderModel", menuName = PlatformerStickyRaycastHitColliderModelPath,
        order = 0)]
    [InlineEditor]
    public class StickyRaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        private StickyRaycastHitColliderData s = null;

        #endregion

        #region private methods

        private void InitializeData()
        {
            s = new StickyRaycastHitColliderData {Character = character};
            s.RuntimeData = StickyRaycastHitColliderRuntimeData.CreateInstance(s.BelowSlopeAngleLeft);
        }

        private void InitializeModel()
        {
            s.RightStickyRaycastHitColliderRuntimeData = s.Character
                .GetComponentNoAllocation<RaycastHitColliderController>().RightStickyRaycastHitColliderRuntimeData;
            s.LeftStickyRaycastHitColliderRuntimeData = s.Character
                .GetComponentNoAllocation<RaycastHitColliderController>().LeftStickyRaycastHitColliderRuntimeData;
            s.BelowSlopeAngleRight = s.RightStickyRaycastHitColliderRuntimeData.BelowSlopeAngleRight;
            s.BelowSlopeAngleLeft = s.LeftStickyRaycastHitColliderRuntimeData.BelowSlopeAngleLeft;
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
            s.BelowSlopeAngle = s.BelowSlopeAngleLeft;
        }

        private void SetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            s.BelowSlopeAngle = s.BelowSlopeAngleRight;
        }

        #endregion

        #endregion

        #region properties

        public StickyRaycastHitColliderRuntimeData RuntimeData => s.RuntimeData;

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

        public void OnInitializeBelowSlopeAngle()
        {
            InitializeBelowSlopeAngle();
        }

        public void OnSetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleToBelowSlopeAngleLeft();
        }

        public void OnSetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleToBelowSlopeAngleRight();
        }

        #endregion

        #endregion
    }
}
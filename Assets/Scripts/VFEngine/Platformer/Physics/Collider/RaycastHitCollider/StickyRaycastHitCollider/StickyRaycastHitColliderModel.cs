using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

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

        [LabelText("Sticky Raycast Hit Collider Data")] [SerializeField] private StickyRaycastHitColliderData s;
        [SerializeField] private GameObject character;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        private RightStickyRaycastHitColliderData rightStickyRaycastHitCollider;
        private LeftStickyRaycastHitColliderData leftStickyRaycastHitCollider;

        #endregion

        #region private methods

        private void InitializeData()
        {
            if (!s) s = CreateInstance<StickyRaycastHitColliderData>();
            if (!raycastHitColliderController)
                raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
        }

        private void InitializeModel()
        {
            rightStickyRaycastHitCollider = raycastHitColliderController.RightStickyRaycastHitColliderModel.Data;
            leftStickyRaycastHitCollider = raycastHitColliderController.LeftStickyRaycastHitColliderModel.Data;
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
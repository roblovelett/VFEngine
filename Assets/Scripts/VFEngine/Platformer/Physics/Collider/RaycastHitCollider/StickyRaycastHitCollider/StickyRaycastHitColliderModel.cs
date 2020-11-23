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

        [LabelText("Sticky Raycast Hit Collider Data")] [SerializeField]
        private StickyRaycastHitColliderData s = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            s = new StickyRaycastHitColliderData {Character = character};
            //s.RuntimeData = s.Character.GetComponentNoAllocation<RaycastHitColliderController>()
            //    .StickyRaycastHitColliderRuntimeData;
            //s.RuntimeData.SetStickyRaycastHitCollider(s.BelowSlopeAngleLeft);
        }

        private void InitializeModel()
        {
            /*s.RightStickyRaycastHitColliderRuntimeData = s.Character
                .GetComponentNoAllocation<RaycastHitColliderController>().RightStickyRaycastHitColliderRuntimeData;
            s.LeftStickyRaycastHitColliderRuntimeData = s.Character
                .GetComponentNoAllocation<RaycastHitColliderController>().LeftStickyRaycastHitColliderRuntimeData;
            s.BelowSlopeAngleRight = s.RightStickyRaycastHitColliderRuntimeData.rightStickyRaycastHitCollider
                .BelowSlopeAngleRight;
            s.BelowSlopeAngleLeft = s.LeftStickyRaycastHitColliderRuntimeData.leftStickyRaycastHitCollider
                .BelowSlopeAngleLeft;
            InitializeBelowSlopeAngle();*/
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

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
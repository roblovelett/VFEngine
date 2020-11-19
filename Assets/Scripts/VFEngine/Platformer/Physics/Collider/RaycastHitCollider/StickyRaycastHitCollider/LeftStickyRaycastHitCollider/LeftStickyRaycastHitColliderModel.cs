using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
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

        [LabelText("Left Sticky Raycast Hit Collider Data")] [SerializeField]
        private LeftStickyRaycastHitColliderData l = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            l.RuntimeData = l.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            l.RuntimeData.SetLeftStickyRaycastHitCollider(l.BelowSlopeAngleLeft, l.CrossBelowSlopeAngleLeft);
        }

        private void InitializeModel()
        {
            l.LeftStickyRaycastHit = l.RuntimeData.leftStickyRaycast.LeftStickyRaycastHit;
            l.Transform = l.RuntimeData.platformer.Transform;
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

        #region public methods

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

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
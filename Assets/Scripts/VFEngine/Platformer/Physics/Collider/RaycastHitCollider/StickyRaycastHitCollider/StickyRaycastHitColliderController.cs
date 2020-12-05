using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider
{
    using static UniTaskExtensions;

    public class StickyRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private RightStickyRaycastHitColliderController rightStickyRaycastHitColliderController;
        private LeftStickyRaycastHitColliderController leftStickyRaycastHitColliderController;
        private StickyRaycastHitColliderData s;
        private RightStickyRaycastHitColliderData rightStickyRaycastHitCollider;
        private LeftStickyRaycastHitColliderData leftStickyRaycastHitCollider;

        #endregion

        #region private methods

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            rightStickyRaycastHitColliderController = GetComponent<RightStickyRaycastHitColliderController>();
            leftStickyRaycastHitColliderController = GetComponent<LeftStickyRaycastHitColliderController>();
        }
        
        private void InitializeData()
        {
            s = new StickyRaycastHitColliderData();
        }

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            rightStickyRaycastHitCollider = rightStickyRaycastHitColliderController.Data;
            leftStickyRaycastHitCollider = leftStickyRaycastHitColliderController.Data;
        }

        /*private void ResetState()
        {
            InitializeBelowSlopeAngle();
        }*/

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

        /*public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/

        public async UniTaskVoid OnInitializeBelowSlopeAngle()
        {
            InitializeBelowSlopeAngle();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetBelowSlopeAngleToBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleToBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetBelowSlopeAngleToBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleToBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        #endregion

        #endregion
    }
}
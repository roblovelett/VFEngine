using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    using static Vector3;
    using static UniTaskExtensions;

    public class RightStickyRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private RightStickyRaycastController rightStickyRaycastController;
        private RightStickyRaycastHitColliderData r;
        private PhysicsData physics;
        private RightStickyRaycastData rightStickyRaycast;

        #endregion

        #region private methods

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            physicsController = GetComponent<PhysicsController>();
            rightStickyRaycastController = GetComponent<RightStickyRaycastController>();
        }
        
        private void InitializeData()
        {
            r = new RightStickyRaycastHitColliderData();
        }

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            rightStickyRaycast = rightStickyRaycastController.Data;
        }

        /*private void ResetState()
        {
            r.BelowSlopeAngleRight = 0f;
            r.CrossBelowSlopeAngleRight = zero;
        }*/

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

        /*public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/

        public async UniTaskVoid OnSetBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCrossBelowSlopeAngleRight()
        {
            SetCrossBelowSlopeAngleRight();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetBelowSlopeAngleRightToNegative()
        {
            SetBelowSlopeAngleRightToNegative();
        }

        #endregion

        #endregion
    }
}
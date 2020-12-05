using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    using static Vector3;
    using static UniTaskExtensions;

    public class LeftStickyRaycastHitColliderController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        private PhysicsController physicsController;
        private LeftStickyRaycastController leftStickyRaycastController;
        private LeftStickyRaycastHitColliderData l;
        private PhysicsData physics;
        private LeftStickyRaycastData leftStickyRaycast;

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
            leftStickyRaycastController = GetComponent<LeftStickyRaycastController>();
        }

        private void InitializeData()
        {
            l = new LeftStickyRaycastHitColliderData();
        }
        
        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            physics = physicsController.Data;
            leftStickyRaycast = leftStickyRaycastController.Data;
        }

        /*private void ResetState()
        {
            l.BelowSlopeAngleLeft = 0f;
            l.CrossBelowSlopeAngleLeft = zero;
        }*/

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

        public async UniTaskVoid OnSetBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public async UniTaskVoid OnSetCrossBelowSlopeAngleLeft()
        {
            SetCrossBelowSlopeAngleLeft();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnSetBelowSlopeAngleLeftToNegative()
        {
            SetBelowSlopeAngleLeftToNegative();
        }

        /*public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/

        #endregion

        #endregion
    }
}
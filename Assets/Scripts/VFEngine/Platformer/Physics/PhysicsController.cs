using UnityEngine;
using VFEngine.Platformer.Event.Raycast;

// ReSharper disable ConvertToAutoProperty
// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics
{
    using static ScriptableObject;
    using static GameObject;

    public class PhysicsController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private PhysicsSettings settings;
        private PhysicsModel _model;
        private RaycastController _raycastController;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _raycastController = GetComponent<RaycastController>();
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            _model = new PhysicsModel(character, settings, _raycastController);
        }

        #endregion
        
        #endregion

        #endregion

        #region properties

        public PhysicsData Data => _model.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            _model.SetHorizontalMovementDirection();
        }

        public void OnPlatformerSetExternalForce()
        {
            _model.SetExternalForce();
        }

        public void OnPlatformerApplyGravity()
        {
            _model.ApplyGravity();
        }

        public void OnPlatformerApplyForcesToExternal()
        {
            _model.ApplyForcesToExternal();
        }

        public void OnPlatformerDescendSlope()
        {
            _model.DescendSlope();
        }

        public void OnPlatformerClimbSlope()
        {
            _model.OnClimbSlope();
        }

        public void OnPlatformerOnFirstSideHit()
        {
            _model.OnFirstSideHit();
        }

        public void OnPlatformerOnSideHit()
        {
            _model.OnSideHit();
        }

        public void OnPlatformerStopVerticalMovement()
        {
            _model.StopVerticalMovement();
        }

        public void OnPlatformerAdjustVerticalMovementToSlope()
        {
            _model.OnAdjustVerticalMovementToSlope();
        }

        public void OnPlatformerHitWall()
        {
            _model.OnHitWall();
        }

        public void OnPlatformerStopHorizontalSpeed()
        {
            _model.StopHorizontalSpeed();
        }

        public void OnPlatformerVerticalHit()
        {
            _model.OnVerticalHit();
        }

        public void OnPlatformerApplyGroundAngle()
        {
            _model.OnPlatformerApplyGroundAngle();
        }

        #endregion

        #endregion
    }
}
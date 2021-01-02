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
        private PhysicsModel Physics { get; set; }
        private RaycastData _raycast;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _raycast = GetComponent<RaycastController>().Data;
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            Physics = new PhysicsModel(ref character, ref settings, ref _raycast);
        }

        #endregion
        
        #endregion

        #endregion

        #region properties

        public PhysicsData Data => Physics.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            Physics.SetHorizontalMovementDirection();
        }

        public void OnPlatformerMoveExternalForce()
        {
            Physics.MoveExternalForceTowards();
        }

        public void OnPlatformerApplyGravity()
        {
            Physics.ApplyGravity();
        }

        public void OnPlatformerApplyForcesToExternal()
        {
            Physics.ApplyForcesToExternal();
        }

        public void OnPlatformerDescendSlope()
        {
            Physics.DescendSlope();
        }

        public void OnPlatformerClimbSlope()
        {
            Physics.OnClimbSlope();
        }

        public void OnPlatformerOnFirstSideHit()
        {
            Physics.OnFirstSideHit();
        }

        public void OnPlatformerOnSideHit()
        {
            Physics.OnSideHit();
        }

        public void OnPlatformerStopVerticalMovement()
        {
            Physics.StopVerticalMovement();
        }

        public void OnPlatformerAdjustVerticalMovementToSlope()
        {
            Physics.OnAdjustVerticalMovementToSlope();
        }

        public void OnPlatformerHitWall()
        {
            Physics.OnHitWall();
        }

        public void OnPlatformerStopHorizontalSpeed()
        {
            Physics.StopHorizontalSpeed();
        }

        public void OnPlatformerVerticalHit()
        {
            Physics.OnVerticalHit();
        }

        public void OnPlatformerApplyGroundAngle()
        {
            Physics.OnPlatformerApplyGroundAngle();
        }

        public void OnPlatformerClimbSteepSlope()
        {
            Physics.OnClimbSteepSlope();
        }

        public void OnPlatformerClimbMildSlope()
        {
            Physics.OnClimbMildSlope();
        }

        public void OnPlatformerDescendMildSlope()
        {
            Physics.OnDescendMildSlope();
        }

        public void OnPlatformerDescendSteepSlope()
        {
            Physics.OnDescendSteepSlope();
        }

        public void OnPlatformerTranslateMovement()
        {
            Physics.OnTranslateMovement();
        }

        public void OnPlatformerCeilingOrGroundCollision()
        {
            Physics.OnCeilingOrGroundCollision();
        }

        public void OnPlatformerResetFriction()
        {
            Physics.ResetFriction();
        }

        #endregion

        #endregion
    }
}
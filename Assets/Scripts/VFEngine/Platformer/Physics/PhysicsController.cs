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
        private PhysicsModel _physics;
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
            _physics = new PhysicsModel(character, settings, _raycastController);
        }

        #endregion
        
        #endregion

        #endregion

        #region properties

        public PhysicsData Data => _physics.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            _physics.SetHorizontalMovementDirection();
        }

        public void OnPlatformerSetExternalForce()
        {
            _physics.SetExternalForce();
        }

        public void OnPlatformerApplyGravity()
        {
            _physics.ApplyGravity();
        }

        public void OnPlatformerApplyForcesToExternal()
        {
            _physics.ApplyForcesToExternal();
        }

        public void OnPlatformerDescendSlope()
        {
            _physics.DescendSlope();
        }

        public void OnPlatformerClimbSlope()
        {
            _physics.ClimbSlope();
        }

        public void OnPlatformerOnFirstSideHit()
        {
            _physics.OnFirstSideHit();
        }

        public void OnPlatformerOnSideHit()
        {
            _physics.OnSideHit();
        }

        public void OnPlatformerStopVerticalMovement()
        {
            _physics.StopVerticalMovement();
        }

        public void OnPlatformerAdjustVerticalMovementToSlope()
        {
            _physics.OnAdjustVerticalMovementToSlope();
        }

        public void OnPlatformerHitWall()
        {
            _physics.OnHitWall();
        }

        #endregion

        #endregion
    }
}
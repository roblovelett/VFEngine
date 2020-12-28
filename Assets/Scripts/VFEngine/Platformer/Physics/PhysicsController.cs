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
        private PhysicsModel physics;
        private RaycastController raycastController;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            raycastController = GetComponent<RaycastController>();
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            physics = new PhysicsModel(character, settings, raycastController);
        }

        #endregion

        /*private void PlatformerSetExternalForce()
        {
            if (IgnoreFriction) return;
            p.ExternalForce = MoveTowards(ExternalForce, zero, ExternalForce.magnitude * Friction *deltaTime);
        }

        private void PlatformerSetGravity()
        {
            if (VerticalSpeed) p.SpeedY += GravitationalForce;
            else p.ExternalForceY += GravitationalForce;
        }

        private void PlatformerSetHorizontalExternalForce()
        {
            //if (!AddToHorizontalExternalForce) return;
            p.ExternalForceX += -Gravity * GroundFriction * GroundDirection * fixedDeltaTime / 4;
        }
      
        private void PlatformerDescendSlope()
        {
            //p.OnDescendSlope(/*GroundAngle* Distance);
        }

        private void PlatformerClimbSlope()
        {
            //if (!MetMinimumWallAngle || !ClimbSlope) return;
            //p.OnClimbSlope(VerticalMovement, GroundAngle, Distance);
        }*/

        #endregion

        #endregion

        #region properties

        public PhysicsData Data => physics.Data;

        #region public methods

        /*public void OnPlatformerSetExternalForce()
        {
            PlatformerSetExternalForce();
        }

        public void OnPlatformerSetGravity()
        {
            PlatformerSetGravity();
        }

        public void OnPlatformerSetHorizontalExternalForce()
        {
            PlatformerSetHorizontalExternalForce();
        }

        public void OnPlatformerDescendSlope()
        {
            PlatformerDescendSlope();
        }

        public void OnPlatformerClimbSlope()
        {
            PlatformerClimbSlope();
        }*/

        #endregion

        #endregion
    }
}
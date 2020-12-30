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
        
        #endregion

        #endregion

        #region properties

        public PhysicsData Data => physics.Data;

        #region public methods

        public void OnPlatformerInitializeFrame()
        {
            physics.SetHorizontalMovementDirection();
        }

        public void OnPlatformerSetExternalForce()
        {
            physics.SetExternalForce();
        }

        public void OnPlatformerApplyGravity()
        {
            physics.ApplyGravity();
        }

        public void OnPlatformerApplyForcesToExternal()
        {
            physics.ApplyForcesToExternal();
        }

        public void OnPlatformerDescendSlope()
        {
            physics.DescendSlope();
        }

        public void OnPlatformerClimbSlope()
        {
            physics.ClimbSlope();
        }

        #endregion

        #endregion
    }
}
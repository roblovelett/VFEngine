using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast.SafetyBoxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.StickyRaycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;
using VFEngine.Platformer.Physics.Gravity;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static DebugExtensions;

    public class PlatformerData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private PlatformerSettings settings;
        [SerializeField] private PhysicsController physicsController;
        [SerializeField] private GravityController gravityController;
        [SerializeField] private RaycastController raycastController;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private StickyRaycastController stickyRaycastController;
        [SerializeField] private SafetyBoxcastController safetyBoxcastController;
        [SerializeField] private LayerMaskController layerMaskController;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private BoolReference isCollidingWithMovingPlatform;
        [SerializeField] private Vector3Reference movingPlatformCurrentSpeed;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;

        /* fields */
        private bool DisplayWarnings => settings.displayWarningsControl;
        [SerializeField] private BoolReference setGravity;
        [SerializeField] private BoolReference applyAscentMultiplierToGravity;
        [SerializeField] private BoolReference applyFallMultiplierToGravity;
        [SerializeField] private BoolReference applyGravityToSpeed;
        [SerializeField] private BoolReference applyFallSlowFactorToSpeed;

        /* fields: methods */
        private void GetWarningMessage()
        {
            if (!DisplayWarnings) return;
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!physicsController) warningMessage += ControllerMessage("Physics");
            if (!gravityController) warningMessage += ControllerMessage("Gravity");
            if (!raycastController) warningMessage += ControllerMessage("Raycast");
            if (!raycastHitColliderController) warningMessage += ControllerMessage("Raycast Hit Collider");
            if (!stickyRaycastController) warningMessage += ControllerMessage("Sticky Raycast");
            if (!safetyBoxcastController) warningMessage += ControllerMessage("Safety Boxcast");
            if (!layerMaskController) warningMessage += ControllerMessage("Layer Mask");
            DebugLogWarning(warningMessageCount, warningMessage);

            string ControllerMessage(string controller)
            {
                warningMessageCount++;
                return
                    $"{controller} Controller field not set to {controller} Controller component of parent Character GameObject.@";
            }
        }

        /* properties: dependencies */
        public PhysicsModel PhysicsModel { get; private set; }
        public GravityModel GravityModel { get; private set; }
        public RaycastModel RaycastModel { get; private set; }
        public RaycastHitColliderModel RaycastHitColliderModel { get; private set; }
        public StickyRaycastModel StickyRaycastModel { get; private set; }
        public SafetyBoxcastModel SafetyBoxcastModel { get; private set; }
        public LayerMaskModel LayerMaskModel { get; private set; }
        public Vector2 Speed => speed.Value;
        public bool GravityActive => gravityActive.Value;
        public float FallSlowFactor => fallSlowFactor.Value;
        public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
        public Vector3 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;

        /* properties */

        /* properties: methods */
        public void Initialize()
        {
            PhysicsModel = physicsController.Model as PhysicsModel;
            GravityModel = gravityController.Model as GravityModel;
            RaycastModel = raycastController.Model as RaycastModel;
            RaycastHitColliderModel = raycastHitColliderController.Model as RaycastHitColliderModel;
            StickyRaycastModel = stickyRaycastController.Model as StickyRaycastModel;
            SafetyBoxcastModel = safetyBoxcastController.Model as SafetyBoxcastModel;
            LayerMaskModel = layerMaskController.Model as LayerMaskModel;
            GetWarningMessage();
        }
    }
}
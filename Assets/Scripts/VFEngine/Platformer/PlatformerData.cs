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
        [SerializeField] private RaycastsController raycastsManager;
        [SerializeField] private RaycastHitColliderController raycastHitCollidersManager;
        [SerializeField] private StickyRaycastController stickyRaycastController;
        [SerializeField] private SafetyBoxcastController safetyBoxcastController;
        [SerializeField] private LayerMaskController layerMaskController;
        [SerializeField] private Vector2Reference speed;
        [SerializeField] private BoolReference gravityActive;
        [SerializeField] private FloatReference fallSlowFactor;
        [SerializeField] private BoolReference isCollidingWithMovingPlatform;
        [SerializeField] private Vector3Reference movingPlatformCurrentSpeed;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;
        [SerializeField] private FloatReference movementDirectionThreshold;
        [SerializeField] private Vector2Reference externalForce;
        [SerializeField] private BoolReference castRaysOnBothSides;
        [SerializeField] private FloatReference tolerance;
        [SerializeField] private FloatReference movementDirection;

        /* fields */
        private bool DisplayWarnings => settings.displayWarningsControl;
        [SerializeField] private BoolReference setGravity;
        [SerializeField] private BoolReference applyAscentMultiplierToGravity;
        [SerializeField] private BoolReference applyFallMultiplierToGravity;
        [SerializeField] private BoolReference applyGravityToSpeed;
        [SerializeField] private BoolReference applyFallSlowFactorToSpeed;
        [SerializeField] private BoolReference castRaysRight;
        [SerializeField] private BoolReference castRaysLeft;

        private bool CastRaysRight
        {
            set => value = castRaysRight.Value;
        }

        private bool CastRaysLeft
        {
            set => value = castRaysLeft.Value;
        }

        /* fields: methods */
        private void GetWarningMessage()
        {
            if (!DisplayWarnings) return;
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!physicsController) warningMessage += ControllerMessage("Physics");
            if (!gravityController) warningMessage += ControllerMessage("Gravity");
            if (!raycastsManager) warningMessage += ControllerMessage("Raycast");
            if (!raycastHitCollidersManager) warningMessage += ControllerMessage("Raycast Hit Collider");
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
        public RaycastsModel RaycastsManagerModel { get; private set; }
        public RaycastHitColliderModel RaycastHitCollidersManagerModel { get; private set; }
        public StickyRaycastModel StickyRaycastModel { get; private set; }
        public SafetyBoxcastModel SafetyBoxcastModel { get; private set; }
        public LayerMaskModel LayerMaskModel { get; private set; }
        public Vector2 Speed => speed.Value;
        public bool GravityActive => gravityActive.Value;
        public float FallSlowFactor => fallSlowFactor.Value;
        public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
        public Vector3 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;
        public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;
        public float MovementDirectionThreshold => movementDirectionThreshold.Value;
        public Vector2 ExternalForce => externalForce.Value;
        public bool CastRaysOnBothSides => castRaysOnBothSides.Value;
        public float Tolerance => tolerance.Value;
        public float MovementDirection => movementDirection.Value;

        /* properties */
        public ModelState state = new ModelState();

        /* properties: methods */
        public void Initialize()
        {
            PhysicsModel = physicsController.Model as PhysicsModel;
            GravityModel = gravityController.Model as GravityModel;
            //RaycastsManagerModel = raycastsManager.Model as RaycastsManagerModel;
            //RaycastHitCollidersManagerModel = raycastHitCollidersManager.Model as RaycastHitCollidersManagerModel;
            StickyRaycastModel = stickyRaycastController.Model as StickyRaycastModel;
            SafetyBoxcastModel = safetyBoxcastController.Model as SafetyBoxcastModel;
            LayerMaskModel = layerMaskController.Model as LayerMaskModel;
            CastRaysRight = state.CastRaysRight;
            CastRaysLeft = state.CastRaysLeft;
            state.Reset();
            GetWarningMessage();
        }

        public class ModelState
        {
            public bool CastRaysRight { get; private set; }
            public bool CastRaysLeft { get; private set; }

            public void SetCastRaysRight(bool cast)
            {
                CastRaysRight = cast;
            }

            public void SetCastRaysLeft(bool cast)
            {
                CastRaysLeft = cast;
            }

            public void Reset()
            {
                CastRaysRight = false;
                CastRaysLeft = false;
            }
        }
    }
}
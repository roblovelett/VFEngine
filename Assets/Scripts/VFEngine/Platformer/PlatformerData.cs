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

        /* fields */
        private bool DisplayWarnings => settings.displayWarningsControl;

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

        /* properties */
        public PhysicsModel PhysicsModel { get; private set; }
        public GravityModel GravityModel { get; private set; }
        public RaycastModel RaycastModel { get; private set; }
        public StickyRaycastModel StickyRaycastModel { get; private set; }
        public SafetyBoxcastModel SafetyBoxcastModel { get; private set; }
        public LayerMaskModel LayerMaskModel { get; private set; }

        /* properties: methods */
        public void Initialize()
        {
            PhysicsModel = physicsController.Model as PhysicsModel;
            GravityModel = gravityController.Model as GravityModel;
            RaycastModel = raycastController.Model as RaycastModel;
            StickyRaycastModel = stickyRaycastController.Model as StickyRaycastModel;
            SafetyBoxcastModel = safetyBoxcastController.Model as SafetyBoxcastModel;
            LayerMaskModel = layerMaskController.Model as LayerMaskModel;
            GetWarningMessage();
        }
    }
}
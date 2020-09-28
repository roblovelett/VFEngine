using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static DebugExtensions;

    public class RaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastSettings settings;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector3Reference boxColliderBoundsCenter;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private BoolReference isCollidingWithMovingPlatform;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;
        [SerializeField] private Vector3Reference movingPlatformCurrentSpeed;

        /* fields */
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private IntReference numberOfVerticalRays;
        private bool DisplayWarnings => settings.displayWarningsControl;

        /* fields: methods */
        private void GetWarningMessage()
        {
            if (!DisplayWarnings) return;
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings) warningMessage += SettingsMessage("Settings", "Raycast Settings");
            if (NumberOfHorizontalRays < 0) warningMessage += NumberRaysMessage("Horizontal");
            if (NumberOfVerticalRays < 0) warningMessage += NumberRaysMessage("Vertical");
            if (DistanceToGroundRayMaximumLength <= 0)
                warningMessage += LtEqZeroMessage("Distance To Ground Ray Maximum Length");
            if (RayOffset <= 0) warningMessage += LtEqZeroMessage("Raycast Offset");
            DebugLogWarning(warningMessageCount, warningMessage);

            string SettingsMessage(string field, string scriptableObject)
            {
                warningMessageCount++;
                return $"{field} field not set to {scriptableObject} ScriptableObject.@";
            }

            string NumberRaysMessage(string direction)
            {
                warningMessageCount++;
                return
                    $"Number of {direction} Rays field in Raycast Settings ScriptableObject cannot be set to value less than zero.@";
            }

            string LtEqZeroMessage(string field)
            {
                warningMessageCount++;
                return
                    $"{field} field in Raycast Settings ScriptableObject cannot be set to value less than or equal to zero.@";
            }
        }

        private static Vector3 SetBoundsSide(Vector3 corner1, Vector3 corner2)
        {
            return (corner1 + corner2) / 2;
        }

        /* properties: dependencies */
        public Transform Transform => transform.Value;
        public Vector2 BoxColliderOffset => boxColliderOffset.Value;
        public Vector2 BoxColliderSize => boxColliderSize.Value;
        public Vector3 BoxColliderBoundsCenter => boxColliderBoundsCenter.Value;
        public bool IsCollidingWithMovingPlatform => isCollidingWithMovingPlatform.Value;
        public bool WasTouchingCeilingLastFrame => wasTouchingCeilingLastFrame.Value;
        public Vector3 MovingPlatformCurrentSpeed => movingPlatformCurrentSpeed.Value;

        /* properties */
        public bool DrawRaycastGizmos => settings.drawRaycastGizmosControl;
        public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
        public int NumberOfVerticalRays => numberOfVerticalRays.Value;
        public float DistanceToGroundRayMaximumLength => settings.distanceToGroundRayMaximumLength;
        public bool CastRaysOnBothSides => settings.castRaysOnBothSides;
        public float RayOffset => settings.rayOffset;
        public float HorizontalRayLength { get; set; }
        public float HorizontalRaySpacing { get; set; }
        public float HorizontalRayCount { get; set; }
        public float VerticalRaySpacing { get; set; }
        public float VerticalRayCount { get; set; }
        public float DistanceToGround { get; set; }
        public LayerMask PlatformsLayerMaskBelow { get; set; }
        public LayerMask NonOneWayPlatformsLayerMaskBelow { get; set; }
        public LayerMask NonMidHeightPlatformsLayerMaskBelow { get; set; }
        public Vector2 OriginalSizeRaycastOrigin { get; set; }
        public Vector2 HorizontalRayCastFromBottom { get; set; } = Vector2.zero;
        public Vector2 HorizontalRayCastToTop { get; set; } = Vector2.zero;
        public Vector2 VerticalRayCastFromLeft { get; set; } = Vector2.zero;
        public Vector2 VerticalRayCastToRight { get; set; } = Vector2.zero;
        public Vector2 AboveRayCastStart { get; set; } = Vector2.zero;
        public Vector2 AboveRayCastEnd { get; set; } = Vector2.zero;
        public Vector2 RayCastOrigin { get; set; } = Vector2.zero;
        public RaycastHit2D DistanceToGroundRaycast { get; set; }
        public float BoundsWidth { get; set; }
        public float BoundsHeight { get; set; }
        public Vector3 BoundsTopLeftCorner { get; set; }
        public Vector3 BoundsBottomLeftCorner { get; set; }
        public Vector3 BoundsTopRightCorner { get; set; }
        public Vector3 BoundsBottomRightCorner { get; set; }
        public Vector3 BoundsCenter { get; set; }
        public Vector3 BoundsTop => SetBoundsSide(BoundsTopLeftCorner, BoundsTopRightCorner);
        public Vector3 BoundsBottom => SetBoundsSide(BoundsBottomLeftCorner, BoundsBottomRightCorner);
        public Vector3 BoundsRight => SetBoundsSide(BoundsTopRightCorner, BoundsBottomRightCorner);
        public Vector3 BoundsLeft => SetBoundsSide(BoundsTopLeftCorner, BoundsBottomLeftCorner);
        public Vector2 Bounds => new Vector2 {x = BoundsWidth, y = BoundsHeight};

        /* properties: methods */
        public void Initialize()
        {
            numberOfHorizontalRays.Value = settings.numberOfHorizontalRays;
            numberOfVerticalRays.Value = settings.numberOfVerticalRays;
            GetWarningMessage();
        }
    }
}
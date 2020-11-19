using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Platformer.Event.Boxcast;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.Collider.RaycastHitCollider;

namespace VFEngine.Platformer
{
    public class PlatformerRuntimeData : SerializedScriptableObject
    {
        #region properties

        public Platformer platformer;
        public Boxcast boxcast;
        public SafetyBoxcast safetyBoxcast;
        public DistanceToGroundRaycast distanceToGroundRaycast;
        public DownRaycast downRaycast;
        public LeftRaycast leftRaycast;
        public RightRaycast rightRaycast;
        public StickyRaycast stickyRaycast;
        public LeftStickyRaycast leftStickyRaycast;
        public RightStickyRaycast rightStickyRaycast;
        public UpRaycast upRaycast;
        public Raycast raycast;
        public LayerMasks layerMask;
        public BoxCollider boxCollider;
        public DistanceToGroundRaycastHitCollider distanceToGroundRaycastHitCollider;
        public DownRaycastHitCollider downRaycastHitCollider;
        public LeftRaycastHitCollider leftRaycastHitCollider;
        public RightRaycastHitCollider rightRaycastHitCollider;
        public StickyRaycastHitCollider stickyRaycastHitCollider;
        public LeftStickyRaycastHitCollider leftStickyRaycastHitCollider;
        public RightStickyRaycastHitCollider rightStickyRaycastHitCollider;
        public UpRaycastHitCollider upRaycastHitCollider;
        public RaycastHitCollider raycastHitCollider;
        public Physics physics;

        public struct Boxcast
        {
            public BoxcastController BoxcastController { get; set; }
        }

        public struct SafetyBoxcast
        {
            public RaycastHit2D SafetyBoxcastHit { get; set; }
        }

        public struct DistanceToGroundRaycast
        {
            public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        }

        public struct DownRaycast
        {
            public float DownRayLength { get; set; }
            public Vector2 CurrentDownRaycastOrigin { get; set; }
            public Vector2 DownRaycastFromLeft { get; set; }
            public Vector2 DownRaycastToRight { get; set; }
            public RaycastHit2D CurrentDownRaycastHit { get; set; }
        }

        public struct LeftRaycast
        {
            public float LeftRayLength { get; set; }
            public Vector2 LeftRaycastFromBottomOrigin { get; set; }
            public Vector2 LeftRaycastToTopOrigin { get; set; }
            public RaycastHit2D CurrentLeftRaycastHit { get; set; }
        }

        public struct RightRaycast
        {
            public float RightRayLength { get; set; }
            public Vector2 RightRaycastFromBottomOrigin { get; set; }
            public Vector2 RightRaycastToTopOrigin { get; set; }
            public RaycastHit2D CurrentRightRaycastHit { get; set; }
        }

        public struct StickyRaycast
        {
            public bool IsCastingLeft { get; set; }
            public float StickToSlopesOffsetY { get; set; }
            public float StickyRaycastLength { get; set; }
        }

        public struct LeftStickyRaycast
        {
            public float LeftStickyRaycastLength { get; set; }
            public float LeftStickyRaycastOriginY { get; set; }
            public RaycastHit2D LeftStickyRaycastHit { get; set; }
        }

        public struct RightStickyRaycast
        {
            public float RightStickyRaycastLength { get; set; }
            public float RightStickyRaycastOriginY { get; set; }
            public RaycastHit2D RightStickyRaycastHit { get; set; }
        }

        public struct UpRaycast
        {
            public float UpRaycastSmallestDistance { get; set; }
            public Vector2 CurrentUpRaycastOrigin { get; set; }
            public RaycastHit2D CurrentUpRaycastHit { get; set; }
        }

        public struct Raycast
        {
            public RaycastController RaycastController { get; set; }
            public bool DisplayWarningsControl { get; set; }
            public bool DrawRaycastGizmosControl { get; set; }
            public bool CastRaysOnBothSides { get; set; }
            public bool PerformSafetyBoxcast { get; set; }
            public int NumberOfHorizontalRaysPerSide { get; set; }
            public int NumberOfVerticalRaysPerSide { get; set; }
            public float DistanceToGroundRayMaximumLength { get; set; }
            public float BoundsWidth { get; set; }
            public float BoundsHeight { get; set; }
            public float RayOffset { get; set; }
            public float ObstacleHeightTolerance { get; set; }
            public Vector2 Bounds { get; set; }
            public Vector2 BoundsCenter { get; set; }
            public Vector2 BoundsBottomLeftCorner { get; set; }
            public Vector2 BoundsBottomRightCorner { get; set; }
            public Vector2 BoundsBottomCenterPosition { get; set; }
            public Vector2 BoundsTopLeftCorner { get; set; }
            public Vector2 BoundsTopRightCorner { get; set; }
        }

        public struct LayerMasks
        {
            public LayerMaskController LayerMaskController { get; set; }
            public int SavedBelowLayer { get; set; }
            public LayerMask PlatformMask { get; set; }
            public LayerMask MovingPlatformMask { get; set; }
            public LayerMask OneWayPlatformMask { get; set; }
            public LayerMask MovingOneWayPlatformMask { get; set; }
            public LayerMask MidHeightOneWayPlatformMask { get; set; }
            public LayerMask StairsMask { get; set; }
            public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; set; }
            public LayerMask RaysBelowLayerMaskPlatformsWithoutMidHeight { get; set; }
            public LayerMask RaysBelowLayerMaskPlatforms { get; set; }
        }

        public struct BoxCollider
        {
            public BoxCollider2D BoxCollider2D { get; set; }
        }

        public struct DistanceToGroundRaycastHitCollider
        {
            public bool DistanceToGroundRaycastHitConnected { get; set; }
        }

        public struct DownRaycastHitCollider
        {
            public bool HasPhysicsMaterialClosestToDownHit { get; set; }
            public bool HasPathMovementClosestToDownHit { get; set; }
            public bool DownHitConnected { get; set; }
            public bool IsCollidingBelow { get; set; }
            public bool OnMovingPlatform { get; set; }
            public bool HasMovingPlatform { get; set; }
            public bool HasStandingOnLastFrame { get; set; }
            public bool HasGroundedLastFrame { get; set; }
            public bool GroundedEvent { get; set; }
            public int DownHitsStorageLength { get; set; }
            public int CurrentDownHitsStorageIndex { get; set; }
            public float CurrentDownHitSmallestDistance { get; set; }
            public float SmallestDistanceToDownHit { get; set; }
            public float MovingPlatformCurrentGravity { get; set; }
            public Vector2 MovingPlatformCurrentSpeed { get; set; }
            public Vector3 CrossBelowSlopeAngle { get; set; }
            public GameObject StandingOnLastFrame { get; set; }
            public Collider2D StandingOnCollider { get; set; }
            public LayerMask StandingOnWithSmallestDistanceLayer { get; set; }
            public RaycastHit2D RaycastDownHitAt { get; set; }
        }

        public struct LeftRaycastHitCollider
        {
            public bool LeftHitConnected { get; set; }
            public bool IsCollidingLeft { get; set; }
            public int LeftHitsStorageLength { get; set; }
            public int CurrentLeftHitsStorageIndex { get; set; }
            public float CurrentLeftHitAngle { get; set; }
            public float CurrentLeftHitDistance { get; set; }
            public float DistanceBetweenLeftHitAndRaycastOrigin { get; set; }
            public Collider2D CurrentLeftHitCollider { get; set; }
        }

        public struct RightRaycastHitCollider
        {
            public bool RightHitConnected { get; set; }
            public bool IsCollidingRight { get; set; }
            public int RightHitsStorageLength { get; set; }
            public int CurrentRightHitsStorageIndex { get; set; }
            public float CurrentRightHitAngle { get; set; }
            public float CurrentRightHitDistance { get; set; }
            public float DistanceBetweenRightHitAndRaycastOrigin { get; set; }
            public Collider2D CurrentRightHitCollider { get; set; }
        }

        public struct StickyRaycastHitCollider
        {
            public float BelowSlopeAngle { get; set; }
        }

        public struct LeftStickyRaycastHitCollider
        {
            public float BelowSlopeAngleLeft { get; set; }
            public Vector3 CrossBelowSlopeAngleLeft { get; set; }
        }

        public struct RightStickyRaycastHitCollider
        {
            public float BelowSlopeAngleRight { get; set; }
            public Vector3 CrossBelowSlopeAngleRight { get; set; }
        }

        public struct UpRaycastHitCollider
        {
            public bool UpHitConnected { get; set; }
            public bool IsCollidingAbove { get; set; }
            public bool WasTouchingCeilingLastFrame { get; set; }
            public int UpHitsStorageLength { get; set; }
            public int UpHitsStorageCollidingIndex { get; set; }
            public int CurrentUpHitsStorageIndex { get; set; }
            public RaycastHit2D RaycastUpHitAt { get; set; }
        }

        public struct RaycastHitCollider
        {
            public RaycastHitColliderController RaycastHitColliderController { get; set; }
            public Collider IgnoredCollider { get; set; }
            public RaycastHitColliderContactList ContactList { get; set; }
        }

        public struct Physics
        {
            public PhysicsController PhysicsController { get; set; }
            public bool StickToSlopesControl { get; set; }
            public bool SafetyBoxcastControl { get; set; }
            public bool Physics2DInteractionControl { get; set; }
            public bool IsJumping { get; set; }
            public bool IsFalling { get; set; }
            public bool GravityActive { get; set; }
            public bool SafeSetTransformControl { get; set; }
            public int HorizontalMovementDirection { get; set; }
            public float FallSlowFactor { get; set; }
            public float Physics2DPushForce { get; set; }
            public float MaximumSlopeAngle { get; set; }
            public float SmallValue { get; set; }
            public float Gravity { get; set; }
            public float MovementDirectionThreshold { get; set; }
            public float CurrentVerticalSpeedFactor { get; set; }
            public Vector2 Speed { get; set; }
            public Vector2 MaximumVelocity { get; set; }
            public Vector2 NewPosition { get; set; }
            public Vector2 ExternalForce { get; set; }
        }

        public struct Platformer
        {
            public Transform Transform { get; set; }
        }

        #endregion

        #region public methods

        public void SetPlatformer(Transform transform)
        {
            platformer.Transform = transform;
        }

        public void SetBoxcastController(BoxcastController controller)
        {
            boxcast.BoxcastController = controller;
        }

        public void SetSafetyBoxcast(RaycastHit2D hit)
        {
            safetyBoxcast.SafetyBoxcastHit = hit;
        }

        public void SetDistanceToGroundRaycast(RaycastHit2D hit)
        {
            distanceToGroundRaycast.DistanceToGroundRaycastHit = hit;
        }

        public void SetDownRaycast(float downRayLength, Vector2 currentDownRaycastOrigin, Vector2 downRaycastFromLeft,
            Vector2 downRaycastToRight, RaycastHit2D currentDownRaycastHit)
        {
            downRaycast.DownRayLength = downRayLength;
            downRaycast.CurrentDownRaycastOrigin = currentDownRaycastOrigin;
            downRaycast.DownRaycastFromLeft = downRaycastFromLeft;
            downRaycast.DownRaycastToRight = downRaycastToRight;
            downRaycast.CurrentDownRaycastHit = currentDownRaycastHit;
        }

        public void SetLeftRaycast(float leftRayLength, Vector2 leftRaycastFromBottomOrigin,
            Vector2 leftRaycastToTopOrigin, RaycastHit2D currentLeftRaycastHit)
        {
            leftRaycast.LeftRayLength = leftRayLength;
            leftRaycast.LeftRaycastFromBottomOrigin = leftRaycastFromBottomOrigin;
            leftRaycast.LeftRaycastToTopOrigin = leftRaycastToTopOrigin;
            leftRaycast.CurrentLeftRaycastHit = currentLeftRaycastHit;
        }

        public void SetRightRaycast(float rightRayLength, Vector2 rightRaycastFromBottomOrigin,
            Vector2 rightRaycastToTopOrigin, RaycastHit2D currentRightRaycastHit)
        {
            rightRaycast.RightRayLength = rightRayLength;
            rightRaycast.RightRaycastFromBottomOrigin = rightRaycastFromBottomOrigin;
            rightRaycast.RightRaycastToTopOrigin = rightRaycastToTopOrigin;
            rightRaycast.CurrentRightRaycastHit = currentRightRaycastHit;
        }

        public void SetStickyRaycast(bool isCastingLeft, float stickToSlopesOffsetY, float stickyRaycastLength)
        {
            stickyRaycast.IsCastingLeft = isCastingLeft;
            stickyRaycast.StickToSlopesOffsetY = stickToSlopesOffsetY;
            stickyRaycast.StickyRaycastLength = stickyRaycastLength;
        }

        public void SetLeftStickyRaycast(float leftStickyRaycastLength, float leftStickyRaycastOriginY,
            RaycastHit2D leftStickyRaycastHit)
        {
            leftStickyRaycast.LeftStickyRaycastLength = leftStickyRaycastLength;
            leftStickyRaycast.LeftStickyRaycastOriginY = leftStickyRaycastOriginY;
            leftStickyRaycast.LeftStickyRaycastHit = leftStickyRaycastHit;
        }

        public void SetRightStickyRaycast(float rightStickyRaycastLength, float rightStickyRaycastOriginY,
            RaycastHit2D rightStickyRaycastHit)
        {
            rightStickyRaycast.RightStickyRaycastLength = rightStickyRaycastLength;
            rightStickyRaycast.RightStickyRaycastOriginY = rightStickyRaycastOriginY;
            rightStickyRaycast.RightStickyRaycastHit = rightStickyRaycastHit;
        }

        public void SetUpRaycast(float upRaycastSmallestDistance, Vector2 currentUpRaycastOrigin,
            RaycastHit2D currentUpRaycastHit)
        {
            upRaycast.UpRaycastSmallestDistance = upRaycastSmallestDistance;
            upRaycast.CurrentUpRaycastOrigin = currentUpRaycastOrigin;
            upRaycast.CurrentUpRaycastHit = currentUpRaycastHit;
        }

        public void SetRaycastController(RaycastController controller)
        {
            raycast.RaycastController = controller;
        }

        public void SetRaycast(bool displayWarningsControl, bool drawRaycastGizmosControl, bool castRaysOnBothSides,
            bool performSafetyBoxcast, int numberOfHorizontalRaysPerSide, int numberOfVerticalRaysPerSide,
            float distanceToGroundRayMaximumLength, float boundsWidth, float boundsHeight, float rayOffset,
            float obstacleHeightTolerance, Vector2 bounds, Vector2 boundsCenter, Vector2 boundsBottomLeftCorner,
            Vector2 boundsBottomRightCorner, Vector2 boundsBottomCenterPosition, Vector2 boundsTopLeftCorner,
            Vector2 boundsTopRightCorner)
        {
            raycast.DisplayWarningsControl = displayWarningsControl;
            raycast.DrawRaycastGizmosControl = drawRaycastGizmosControl;
            raycast.CastRaysOnBothSides = castRaysOnBothSides;
            raycast.PerformSafetyBoxcast = performSafetyBoxcast;
            raycast.NumberOfHorizontalRaysPerSide = numberOfHorizontalRaysPerSide;
            raycast.NumberOfVerticalRaysPerSide = numberOfVerticalRaysPerSide;
            raycast.DistanceToGroundRayMaximumLength = distanceToGroundRayMaximumLength;
            raycast.BoundsWidth = boundsWidth;
            raycast.BoundsHeight = boundsHeight;
            raycast.RayOffset = rayOffset;
            raycast.ObstacleHeightTolerance = obstacleHeightTolerance;
            raycast.Bounds = bounds;
            raycast.BoundsCenter = boundsCenter;
            raycast.BoundsBottomLeftCorner = boundsBottomLeftCorner;
            raycast.BoundsBottomRightCorner = boundsBottomRightCorner;
            raycast.BoundsBottomCenterPosition = boundsBottomCenterPosition;
            raycast.BoundsTopLeftCorner = boundsTopLeftCorner;
            raycast.BoundsTopRightCorner = boundsTopRightCorner;
        }

        public void SetLayerMaskController(LayerMaskController controller)
        {
            layerMask.LayerMaskController = controller;
        }

        public void SetLayerMasks(int savedBelowLayer, LayerMask platformMask, LayerMask movingPlatformMask,
            LayerMask oneWayPlatformMask, LayerMask movingOneWayPlatformMask, LayerMask midHeightOneWayPlatformMask,
            LayerMask stairsMask, LayerMask raysBelowLayerMaskPlatformsWithoutOneWay,
            LayerMask raysBelowLayerMaskPlatformsWithoutMidHeight, LayerMask raysBelowLayerMaskPlatforms)
        {
            layerMask.SavedBelowLayer = savedBelowLayer;
            layerMask.PlatformMask = platformMask;
            layerMask.MovingPlatformMask = movingPlatformMask;
            layerMask.OneWayPlatformMask = oneWayPlatformMask;
            layerMask.MovingOneWayPlatformMask = movingOneWayPlatformMask;
            layerMask.MidHeightOneWayPlatformMask = midHeightOneWayPlatformMask;
            layerMask.StairsMask = stairsMask;
            layerMask.RaysBelowLayerMaskPlatformsWithoutOneWay = raysBelowLayerMaskPlatformsWithoutOneWay;
            layerMask.RaysBelowLayerMaskPlatformsWithoutMidHeight = raysBelowLayerMaskPlatformsWithoutMidHeight;
            layerMask.RaysBelowLayerMaskPlatforms = raysBelowLayerMaskPlatforms;
        }

        public void SetBoxCollider(BoxCollider2D collider)
        {
            boxCollider.BoxCollider2D = collider;
        }

        public void SetDistanceToGroundRaycastHitCollider(bool hitConnected)
        {
            distanceToGroundRaycastHitCollider.DistanceToGroundRaycastHitConnected = hitConnected;
        }

        public void SetDownRaycastHitCollider(bool hasPhysicsMaterialClosestToDownHit,
            bool hasPathMovementClosestToDownHit, bool downHitConnected, bool isCollidingBelow, bool onMovingPlatform,
            bool hasMovingPlatform, bool hasStandingOnLastFrame, bool hasGroundedLastFrame, bool groundedEvent,
            int downHitsStorageLength, int currentDownHitsStorageIndex, float currentDownHitSmallestDistance,
            float smallestDistanceToDownHit, float movingPlatformCurrentGravity, Vector2 movingPlatformCurrentSpeed,
            Vector2 crossBelowSlopeAngle, GameObject standingOnLastFrame, Collider2D standingOnCollider,
            LayerMask standingOnWithSmallestDistanceLayer, RaycastHit2D raycastDownHitAt)
        {
            downRaycastHitCollider.HasPhysicsMaterialClosestToDownHit = hasPhysicsMaterialClosestToDownHit;
            downRaycastHitCollider.HasPathMovementClosestToDownHit = hasPathMovementClosestToDownHit;
            downRaycastHitCollider.DownHitConnected = downHitConnected;
            downRaycastHitCollider.IsCollidingBelow = isCollidingBelow;
            downRaycastHitCollider.OnMovingPlatform = onMovingPlatform;
            downRaycastHitCollider.HasMovingPlatform = hasMovingPlatform;
            downRaycastHitCollider.HasStandingOnLastFrame = hasStandingOnLastFrame;
            downRaycastHitCollider.HasGroundedLastFrame = hasGroundedLastFrame;
            downRaycastHitCollider.GroundedEvent = groundedEvent;
            downRaycastHitCollider.DownHitsStorageLength = downHitsStorageLength;
            downRaycastHitCollider.CurrentDownHitsStorageIndex = currentDownHitsStorageIndex;
            downRaycastHitCollider.CurrentDownHitSmallestDistance = currentDownHitSmallestDistance;
            downRaycastHitCollider.SmallestDistanceToDownHit = smallestDistanceToDownHit;
            downRaycastHitCollider.MovingPlatformCurrentGravity = movingPlatformCurrentGravity;
            downRaycastHitCollider.MovingPlatformCurrentSpeed = movingPlatformCurrentSpeed;
            downRaycastHitCollider.CrossBelowSlopeAngle = crossBelowSlopeAngle;
            downRaycastHitCollider.StandingOnLastFrame = standingOnLastFrame;
            downRaycastHitCollider.StandingOnCollider = standingOnCollider;
            downRaycastHitCollider.StandingOnWithSmallestDistanceLayer = standingOnWithSmallestDistanceLayer;
            downRaycastHitCollider.RaycastDownHitAt = raycastDownHitAt;
        }

        public void SetLeftRaycastHitCollider(bool leftHitConnected, bool isCollidingLeft, int leftHitsStorageLength,
            int currentLeftHitsStorageIndex, float currentLeftHitAngle, float currentLeftHitDistance,
            float distanceBetweenLeftHitAndRaycastOrigin, Collider2D currentLeftHitCollider)
        {
            leftRaycastHitCollider.LeftHitConnected = leftHitConnected;
            leftRaycastHitCollider.IsCollidingLeft = isCollidingLeft;
            leftRaycastHitCollider.LeftHitsStorageLength = leftHitsStorageLength;
            leftRaycastHitCollider.CurrentLeftHitsStorageIndex = currentLeftHitsStorageIndex;
            leftRaycastHitCollider.CurrentLeftHitAngle = currentLeftHitAngle;
            leftRaycastHitCollider.CurrentLeftHitDistance = currentLeftHitDistance;
            leftRaycastHitCollider.DistanceBetweenLeftHitAndRaycastOrigin = distanceBetweenLeftHitAndRaycastOrigin;
            leftRaycastHitCollider.CurrentLeftHitCollider = currentLeftHitCollider;
        }

        public void SetRightRaycastHitCollider(bool rightHitConnected, bool isCollidingRight,
            int rightHitsStorageLength, int currentRightHitsStorageIndex, float currentRightHitAngle,
            float currentRightHitDistance, float distanceBetweenRightHitAndRaycastOrigin,
            Collider2D currentRightHitCollider)
        {
            rightRaycastHitCollider.RightHitConnected = rightHitConnected;
            rightRaycastHitCollider.IsCollidingRight = isCollidingRight;
            rightRaycastHitCollider.RightHitsStorageLength = rightHitsStorageLength;
            rightRaycastHitCollider.CurrentRightHitsStorageIndex = currentRightHitsStorageIndex;
            rightRaycastHitCollider.CurrentRightHitAngle = currentRightHitAngle;
            rightRaycastHitCollider.CurrentRightHitDistance = currentRightHitDistance;
            rightRaycastHitCollider.DistanceBetweenRightHitAndRaycastOrigin = distanceBetweenRightHitAndRaycastOrigin;
            rightRaycastHitCollider.CurrentRightHitCollider = currentRightHitCollider;
        }

        public void SetStickyRaycastHitCollider(float belowSlopeAngle)
        {
            stickyRaycastHitCollider.BelowSlopeAngle = belowSlopeAngle;
        }

        public void SetLeftStickyRaycastHitCollider(float belowSlopeAngleLeft, Vector3 crossBelowSlopeAngleLeft)
        {
            leftStickyRaycastHitCollider.BelowSlopeAngleLeft = belowSlopeAngleLeft;
            leftStickyRaycastHitCollider.CrossBelowSlopeAngleLeft = crossBelowSlopeAngleLeft;
        }

        public void SetRightStickyRaycastHitCollider(float belowSlopeAngleRight, Vector3 crossBelowSlopeAngleRight)
        {
            rightStickyRaycastHitCollider.BelowSlopeAngleRight = belowSlopeAngleRight;
            rightStickyRaycastHitCollider.CrossBelowSlopeAngleRight = crossBelowSlopeAngleRight;
        }

        public void SetUpRaycastHitCollider(bool upHitConnected, bool isCollidingAbove,
            bool wasTouchingCeilingLastFrame, int upHitsStorageLength, int upHitsStorageCollidingIndex,
            int currentUpHitsStorageIndex, RaycastHit2D raycastUpHitAt)
        {
            upRaycastHitCollider.UpHitConnected = upHitConnected;
            upRaycastHitCollider.IsCollidingAbove = isCollidingAbove;
            upRaycastHitCollider.WasTouchingCeilingLastFrame = wasTouchingCeilingLastFrame;
            upRaycastHitCollider.UpHitsStorageLength = upHitsStorageLength;
            upRaycastHitCollider.UpHitsStorageCollidingIndex = upHitsStorageCollidingIndex;
            upRaycastHitCollider.CurrentUpHitsStorageIndex = currentUpHitsStorageIndex;
            upRaycastHitCollider.RaycastUpHitAt = raycastUpHitAt;
        }

        public void SetRaycastHitColliderController(RaycastHitColliderController controller)
        {
            raycastHitCollider.RaycastHitColliderController = controller;
        }

        public void SetRaycastHitCollider(Collider ignoredCollider, RaycastHitColliderContactList contactList)
        {
            raycastHitCollider.IgnoredCollider = ignoredCollider;
            raycastHitCollider.ContactList = contactList;
        }

        public void SetPhysicsController(PhysicsController controller)
        {
            physics.PhysicsController = controller;
        }

        public void SetPhysics(bool stickToSlopesControl, bool safetyBoxcastControl, bool physics2DInteractionControl,
            bool isJumping, bool isFalling, bool gravityActive, bool safeSetTransformControl,
            int horizontalMovementDirection, float fallSlowFactor, float physics2DPushForce, float maximumSlopeAngle,
            float smallValue, float gravity, float movementDirectionThreshold, float currentVerticalSpeedFactor,
            Vector2 speed, Vector2 maximumVelocity, Vector2 newPosition, Vector2 externalForce, Transform transform)
        {
            physics.StickToSlopesControl = stickToSlopesControl;
            physics.SafetyBoxcastControl = safetyBoxcastControl;
            physics.Physics2DInteractionControl = physics2DInteractionControl;
            physics.IsJumping = isJumping;
            physics.IsFalling = isFalling;
            physics.GravityActive = gravityActive;
            physics.SafeSetTransformControl = safeSetTransformControl;
            physics.HorizontalMovementDirection = horizontalMovementDirection;
            physics.FallSlowFactor = fallSlowFactor;
            physics.Physics2DPushForce = physics2DPushForce;
            physics.MaximumSlopeAngle = maximumSlopeAngle;
            physics.SmallValue = smallValue;
            physics.Gravity = gravity;
            physics.MovementDirectionThreshold = movementDirectionThreshold;
            physics.CurrentVerticalSpeedFactor = currentVerticalSpeedFactor;
            physics.Speed = speed;
            physics.MaximumVelocity = maximumVelocity;
            physics.NewPosition = newPosition;
            physics.ExternalForce = externalForce;
        }

        #endregion
    }
}
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VFEngine.Platformer.Physics.Friction;
using VFEngine.Platformer.Physics.MovingPlatform;
using VFEngine.Tools;
using static UnityEngine.LayerMask;
using static UnityEngine.Vector3;

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local
namespace VFEngine.Platformer.Event.Raycast.ScriptableObjects
{
    using static ScriptableObjectExtensions.Platformer;
    using static Debug;
    using static Vector2;
    using static Color;
    using static Quaternion;
    using static Physics2D;
    using static RaycastData.Direction;
    using static Mathf;
    using static Time;
    using static MathsExtensions;

    [CreateAssetMenu(fileName = "RaycastData", menuName = RaycastDataPath, order = 0)]
    public class RaycastData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public bool CastRaysOnBothSides { get; private set; }
        public bool PerformSafetyBoxcast { get; private set; }
        public int NumberOfHorizontalRays { get; private set; }
        public int NumberOfVerticalRays { get; private set; }
        public float DistanceToGroundRaycastMaximumLength { get; private set; }
        public float StickToSlopeOffsetY { get; private set; }
        public float RayOffset { get; private set; }
        public Vector2 HorizontalRaycastFromBottom => collision.HorizontalRaycastFromBottom;
        public Vector2 HorizontalRaycastToTop => collision.HorizontalRaycastToTop;
        public Vector2 RaycastOrigin => collision.RaycastOrigin;
        public bool GroundedLastFrame => collision.GroundedLastFrame;
        public bool CollidingAbove => collision.Above;
        public bool Colliding => collision.Colliding;
        public bool StandingOnContainsBottomCenter => collision.StandingOnContainsBottomCenter;
        public bool StandingOnLastFrameNotNull => collision.StandingOnLastFrameNotNull;
        public bool IsGrounded => collision.IsGrounded;
        public bool OnMovingPlatform => collision.OnMovingPlatform;
        public bool MovingPlatformNotNull => collision.MovingPlatformNotNull;
        public bool CollidedWithCeilingLastFrame => collision.CollidedWithCeilingLastFrame;
        public bool FrictionNotNull => collision.FrictionNotNull;
        public float MovingPlatformCurrentGravity => collision.MovingPlatformCurrentGravity;
        public float BelowSlopeAngle => collision.BelowSlopeAngle;
        public float HorizontalHitAngle => collision.HorizontalHitAngle;
        public float BelowRaycastDistance => collision.BelowDistance;
        public float BoundsHeight => bounds.Height;
        public float Friction => collision.Friction;
        public float BelowSlopeAngleLeft => collision.BelowSlopeAngleLeft;
        public float BelowSlopeAngleRight => collision.BelowSlopeAngleRight;
        public float BoundsWidth => bounds.Width;
        public Vector2 MovingPlatformCurrentSpeed => collision.MovingPlatformCurrentSpeed;
        public Vector3 CrossBelowSlopeAngle => collision.CrossBelowSlopeAngle;
        public Vector3 CrossBelowSlopeAngleLeft => collision.CrossBelowSlopeAngleLeft;
        public Vector3 CrossBelowSlopeAngleRight => collision.CrossBelowSlopeAngleRight;
        public GameObject StandingOnLastFrame => collision.StandingOnLastFrame;
        public GameObject StandingOn => collision.StandingOn;
        public Collider2D IgnoredCollider => collision.IgnoredCollider;
        public Collider2D StandingOnCollider => collision.StandingOnCollider;
        public RaycastHit2D SafetyBoxcastHit => collision.SafetyBoxcast;
        public RaycastHit2D DistanceToGroundRaycastHit => collision.DistanceToGroundRaycast;
        public RaycastHit2D StickToSlopeRaycast => collision.StickToSlopeRaycast;
        public RaycastHit2D[] HorizontalHitStorage => collision.HorizontalHitStorage;
        public RaycastHit2D[] BelowHitStorage => collision.BelowHitStorage;
        public RaycastHit2D[] AboveHitStorage => collision.AboveHitStorage;
        public IEnumerable<RaycastHit2D> ContactList => collision.ContactList;
        public Direction CurrentDirection => collision.CurrentRaycastDirection;

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }

        #endregion

        #region fields

        private Vector2 originalColliderSize;
        private Vector2 originalColliderOffset;
        private Vector2 originalRaycastOriginSize;
        private Vector2 colliderLeftCenterPosition;
        private Vector2 colliderRightCenterPosition;
        private Vector2 colliderTopCenterPosition;
        private bool drawRaycastGizmosControl;
        private bool displayWarnings;
        private float crouchedRaycastLengthMultiplier;
        private BoxCollider2D boxCollider;
        private Transform transform;
        private const float PlatformerMovingPlatformGravity = -500f;
        private float obstacleHeightTolerance;
        private float horizontalRaycastLength;
        private float verticalRaycastLength;
        private float aboveRaycastLength;
        private float stickToSlopeRayLength;
        private Vector2 verticalRaycastFromLeft;
        private Vector2 verticalRaycastToRight;
        private Vector2 aboveRaycastStart;
        private Vector2 aboveRaycastEnd;
        private BoundsType bounds;
        private Collision collision;

        public RaycastData(Vector2 originalRaycastOriginSize, Vector2 colliderLeftCenterPosition,
            Vector2 colliderRightCenterPosition, Vector2 colliderTopCenterPosition)
        {
            this.originalRaycastOriginSize = originalRaycastOriginSize;
            this.colliderLeftCenterPosition = colliderLeftCenterPosition;
            this.colliderRightCenterPosition = colliderRightCenterPosition;
            this.colliderTopCenterPosition = colliderTopCenterPosition;
        }

        private struct Collision
        {
            public bool StandingOnContainsBottomCenter { get; set; }
            public bool StandingOnLastFrameNotNull { get; set; }
            public bool FrictionNotNull { get; set; }
            public bool MovingPlatformNotNull { get; set; }
            public bool Right { get; set; }
            public bool Left { get; set; }
            public bool Above { get; set; }
            public bool Below { get; set; }
            public bool PassedSlopeAngle { get; set; }
            public bool OnMovingPlatform { get; set; }
            public bool IsGrounded => Below;
            public bool GroundedLastFrame { get; private set; }
            public bool CollidedWithCeilingLastFrame { get; private set; }
            public bool GroundedEvent { get; set; }
            public bool StickToSlopeRaycastCastingLeft { get; set; }
            public bool CastStickToSlopeRaycastLeft { get; set; }
            public bool AboveRaycastHitConnected { get; set; }
            public bool Colliding => Right || Left || Above || Below;
            public float DistanceToLeftCollider { get; set; }
            public float DistanceToRightCollider { get; set; }
            public float LateralSlopeAngle { get; set; }
            public float BelowSlopeAngle { get; set; }
            public float Friction { get; set; }
            public float DistanceToGround { get; set; }
            public float BelowDistance { get; set; }
            public float HorizontalHitAngle { get; set; }
            public float MovingPlatformCurrentGravity { get; set; }
            public float BelowSlopeAngleLeft { get; set; }
            public float BelowSlopeAngleRight { get; set; }
            public Vector2 RaycastOrigin { get; set; }
            public Vector2 HorizontalRaycastToTop { get; set; }
            public Vector2 HorizontalRaycastFromBottom { get; set; }
            public Vector2 MovingPlatformCurrentSpeed { get; set; }
            public Vector3 CrossBelowSlopeAngle { get; set; }
            public Vector3 CrossBelowSlopeAngleLeft { get; set; }
            public Vector3 CrossBelowSlopeAngleRight { get; set; }
            public GameObject StandingOn { get; set; }
            public GameObject StandingOnLastFrame { get; private set; }
            [CanBeNull] public GameObject CurrentWallCollider { get; set; }
            public Collider2D StandingOnCollider { get; set; }
            public Collider2D IgnoredCollider { get; set; }
            public RaycastHit2D StickToSlopeRaycast { get; set; }
            public RaycastHit2D LeftStickToSlopeRaycast { get; set; }
            public RaycastHit2D RightStickToSlopeRaycast { get; set; }
            public RaycastHit2D DistanceToGroundRaycast { get; set; }
            public RaycastHit2D SafetyBoxcast { get; set; }
            public RaycastHit2D[] HorizontalHitStorage { get; set; }
            public RaycastHit2D[] BelowHitStorage { get; set; }
            public RaycastHit2D[] AboveHitStorage { get; set; }
            public List<RaycastHit2D> ContactList { get; set; }
            public FrictionController FrictionController { get; set; }
            public MovingPlatformController MovingPlatformController { get; set; }
            public Direction CurrentRaycastDirection { get; set; }

            public void Reset()
            {
                Left = false;
                Right = false;
                Above = false;
                DistanceToLeftCollider = -1;
                DistanceToRightCollider = 1;
                PassedSlopeAngle = false;
                GroundedEvent = false;
                LateralSlopeAngle = 0f;
            }

            public void InitializeFrame()
            {
                ContactList.Clear();
                GroundedLastFrame = Below;
                StandingOnLastFrame = StandingOn;
                CollidedWithCeilingLastFrame = Above;
                CurrentWallCollider = null;
                Reset();
            }
        }

        private struct BoundsType
        {
            public float Width { get; set; }
            public float Height { get; set; }
            public Vector2 RaycastBounds { get; set; }
            public Vector2 TopLeft { get; set; }
            public Vector2 TopRight { get; set; }
            public Vector2 BottomLeft { get; set; }
            public Vector2 BottomRight { get; set; }
            public Vector2 Top { get; set; }
            public Vector2 Bottom { get; set; }
            public Vector2 Right { get; set; }
            public Vector2 Left { get; set; }
            public Vector2 Center { get; set; }
            public Vector2 BoundsInternal { get; set; }
            public Vector2 BottomCenter { get; set; }
        }

        #endregion

        #region initialization

        private void Initialize(ref BoxCollider2D collider, ref GameObject character, RaycastSettings settings)
        {
            ApplySettings(settings);
            InitializeDefault(ref collider, ref character);
        }

        private void ApplySettings(RaycastSettings settings)
        {
            drawRaycastGizmosControl = settings.drawRaycastGizmosControl;
            displayWarnings = settings.displayWarnings;
            NumberOfHorizontalRays = settings.numberOfHorizontalRays;
            NumberOfVerticalRays = settings.numberOfVerticalRays;
            RayOffset = settings.rayOffset;
            crouchedRaycastLengthMultiplier = settings.crouchedRaycastLengthMultiplier;
            CastRaysOnBothSides = settings.castRaysOnBothSides;
            DistanceToGroundRaycastMaximumLength = settings.distanceToGroundRaycastMaximumLength;
            PerformSafetyBoxcast = settings.performSafetyBoxcast;
            stickToSlopeRayLength = settings.stickToSlopeRaycastLength;
            StickToSlopeOffsetY = settings.stickToSlopeOffsetY;
            obstacleHeightTolerance = settings.obstacleHeightTolerance;
        }

        private bool DisplayBoxColliderWarning => boxCollider.offset.x != 0 && displayWarnings;

        private void InitializeDefault(ref BoxCollider2D collider, ref GameObject character)
        {
            boxCollider = collider;
            transform = character.transform;
            originalColliderSize = collider.size;
            originalColliderOffset = collider.offset;
            if (DisplayBoxColliderWarning) LogWarning("collider x offset must be zero.");
            FrictionInternal(0);
            collision.ContactList = new List<RaycastHit2D>();
            HorizontalHitStorageInternal(new RaycastHit2D[NumberOfHorizontalRays]);
            BelowHitStorageInternal(new RaycastHit2D[NumberOfVerticalRays]);
            AboveHitStorageInternal(new RaycastHit2D[NumberOfVerticalRays]);
            CurrentWallColliderNull();
            collision.Reset();
            UpdateBounds(collider, character);
            RaycastBounds(new Vector2(bounds.Width, bounds.Height));
            BoundsTop((bounds.TopLeft + bounds.TopRight) / 2);
            BoundsBottom((bounds.BottomLeft + bounds.BottomRight) / 2);
            BoundsRight((bounds.TopRight + bounds.BottomRight) / 2);
            BoundsLeft((bounds.TopLeft + bounds.BottomLeft) / 2);
            HorizontalRaycastFromBottomInternal(zero);
            HorizontalRaycastToTopInternal(zero);
            VerticalRaycastFromLeft(zero);
            VerticalRaycastToRight(zero);
            AboveRaycastStartInternal(zero);
            AboveRaycastEnd(zero);
            RaycastOriginInternal(zero);
            CurrentRaycastDirection(None);
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void RaycastBounds(Vector2 raycastBounds)
        {
            bounds.RaycastBounds = raycastBounds;
        }

        private void BoundsTop(Vector2 top)
        {
            bounds.Top = top;
        }

        private void BoundsBottom(Vector2 bottom)
        {
            bounds.Bottom = bottom;
        }

        private void BoundsRight(Vector2 right)
        {
            bounds.Right = right;
        }

        private void BoundsLeft(Vector2 left)
        {
            bounds.Left = left;
        }

        private void UpdateBounds(BoxCollider2D collider, GameObject character)
        {
            var boundsInternal = collider.bounds;
            var offset = collider.offset;
            var size = collider.size;
            var top = offset.y + size.y / 2f;
            var bottom = offset.y - size.y / 2f;
            var left = offset.x - size.x / 2f;
            var right = offset.x + size.x / 2f;
            var center = boundsInternal.center;
            var bottomCenter = new Vector2(boundsInternal.center.x, boundsInternal.min.y);
            BoundsInternal(top, bottom, left, right);
            var topLeft = character.transform.TransformPoint(bounds.TopLeft);
            var topRight = character.transform.TransformPoint(bounds.TopRight);
            var bottomLeft = character.transform.TransformPoint(bounds.BottomLeft);
            var bottomRight = character.transform.TransformPoint(bounds.BottomRight);
            BoundsInternal(topLeft, topRight, bottomLeft, bottomRight);
            var width = Distance(bounds.BottomLeft, bounds.BottomRight);
            var height = Distance(bounds.BottomLeft, bounds.TopLeft);
            Bounds(width, height);
            BoundsCenter(center);
            BoundsBottomCenter(bottomCenter);
        }

        private void BoundsBottomCenter(Vector2 bottomCenter)
        {
            bounds.BottomCenter = bottomCenter;
        }

        private void BoundsCenter(Vector2 center)
        {
            bounds.Center = center;
        }

        private void Bounds(float width, float height)
        {
            BoundsWidthInternal(width);
            BoundsHeightInternal(height);
        }

        private void BoundsWidthInternal(float width)
        {
            bounds.Width = width;
        }

        private void BoundsHeightInternal(float height)
        {
            bounds.Height = height;
        }

        private void BoundsInternal(float boundsTop, float boundsBottom, float boundsLeft, float boundsRight)
        {
            BoundsTopLeft(boundsLeft, boundsTop);
            BoundsTopRight(boundsRight, boundsTop);
            BoundsBottomLeft(boundsLeft, boundsBottom);
            BoundsBottomRight(boundsRight, boundsBottom);
        }

        private void BoundsInternal(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {
            BoundsTopLeft(topLeft);
            BoundsTopRight(topRight);
            BoundsBottomLeft(bottomLeft);
            BoundsBottomRight(bottomRight);
        }

        private void BoundsTopLeft(float x, float y)
        {
            bounds.TopLeft = new Vector2(x, y);
        }

        private void BoundsTopLeft(Vector2 topLeft)
        {
            bounds.TopLeft = topLeft;
        }

        private void BoundsTopRight(float x, float y)
        {
            bounds.TopRight = new Vector2(x, y);
        }

        private void BoundsTopRight(Vector2 topRight)
        {
            bounds.TopRight = topRight;
        }

        private void BoundsBottomLeft(float x, float y)
        {
            bounds.BottomLeft = new Vector2(x, y);
        }

        private void BoundsBottomLeft(Vector2 bottomLeft)
        {
            bounds.BottomLeft = bottomLeft;
        }

        private void BoundsBottomRight(float x, float y)
        {
            bounds.BottomRight = new Vector2(x, y);
        }

        private void BoundsBottomRight(Vector2 bottomRight)
        {
            bounds.BottomRight = bottomRight;
        }

        private void InitializeFrame()
        {
            collision.InitializeFrame();
        }

        private void PerformSafetyBoxcastInternal(GameObject character, Vector2 newPosition, LayerMask platform)
        {
            collision.SafetyBoxcast = BoxCast(bounds.Center, bounds.RaycastBounds, Angle(character.transform.up, up),
                newPosition.normalized, newPosition.magnitude, platform, red, drawRaycastGizmosControl);
        }

        private static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float length,
            LayerMask mask, Color color, bool drawRaycastGizmosControl = false)
        {
            if (!drawRaycastGizmosControl) return Physics2D.BoxCast(origin, size, angle, direction, length, mask);
            var rotation = Euler(0f, 0f, angle);
            var points = new Vector3[8];
            var halfSizeX = size.x / 2f;
            var halfSizeY = size.y / 2f;
            points[0] = rotation * (origin + left * halfSizeX + up * halfSizeY); // top left
            points[1] = rotation * (origin + right * halfSizeX + up * halfSizeY); // top right
            points[2] = rotation * (origin + right * halfSizeX - up * halfSizeY); // bottom right
            points[3] = rotation * (origin + left * halfSizeX - up * halfSizeY); // bottom left
            points[4] = rotation * (origin + left * halfSizeX + up * halfSizeY + length * direction); // top left
            points[5] = rotation * (origin + right * halfSizeX + up * halfSizeY + length * direction); // top right
            points[6] = rotation * (origin + right * halfSizeX - up * halfSizeY + length * direction); // bottom right
            points[7] = rotation * (origin + left * halfSizeX - up * halfSizeY + length * direction); // bottom left
            DrawLine(points[0], points[1], color);
            DrawLine(points[1], points[2], color);
            DrawLine(points[2], points[3], color);
            DrawLine(points[3], points[0], color);
            DrawLine(points[4], points[5], color);
            DrawLine(points[5], points[6], color);
            DrawLine(points[6], points[7], color);
            DrawLine(points[7], points[4], color);
            DrawLine(points[0], points[4], color);
            DrawLine(points[1], points[5], color);
            DrawLine(points[2], points[6], color);
            DrawLine(points[3], points[7], color);
            return Physics2D.BoxCast(origin, size, angle, direction, length, mask);
        }

        private void GroundedEvent(bool groundedEvent = true)
        {
            collision.GroundedEvent = groundedEvent;
        }

        private Vector2 DistanceToGroundRaycastOrigin =>
            new Vector2(collision.BelowSlopeAngle < 0 ? bounds.BottomLeft.x : bounds.BottomRight.x, bounds.Center.y);

        private void DistanceToGroundRaycastInternal(GameObject character, LayerMask belowPlatforms)
        {
            RaycastOriginInternal(DistanceToGroundRaycastOrigin);
            DistanceToGroundRaycastInternal(DistanceToGroundRaycast(character, belowPlatforms));
        }

        private RaycastHit2D DistanceToGroundRaycast(GameObject character, LayerMask belowPlatforms)
        {
            return RayCast(collision.RaycastOrigin, -character.transform.up, DistanceToGroundRaycastMaximumLength,
                belowPlatforms, blue, drawRaycastGizmosControl);
        }

        private void DistanceToGroundRaycastInternal(RaycastHit2D raycast)
        {
            collision.DistanceToGroundRaycast = raycast;
        }

        private void RaycastOriginInternal(Vector2 origin)
        {
            collision.RaycastOrigin = origin;
        }

        private static RaycastHit2D RayCast(Vector2 origin, Vector2 direction, float distance, LayerMask layer,
            Color color, bool drawRaycastGizmosControl = false)
        {
            if (drawRaycastGizmosControl) DrawRay(origin, direction * distance, color);
            return Raycast(origin, direction, distance, layer);
        }

        private static float DistanceToGroundInternal => -1f;
        private float DistanceToGroundOnRaycastHit => collision.DistanceToGroundRaycast.distance - bounds.Height / 2;

        private void SetDistanceToGroundOnRaycastHit()
        {
            DistanceToGround(DistanceToGroundOnRaycastHit);
        }

        private void DistanceToGround()
        {
            DistanceToGround(DistanceToGroundInternal);
        }

        private void DistanceToGround(float distance)
        {
            collision.DistanceToGround = distance;
        }

        private void SetStandingOnLastFrameLayerToSavedBelow(LayerMask savedBelow)
        {
            SetStandingOnLastFrameLayer(savedBelow);
        }

        private void SetStandingOnLastFrameLayer(LayerMask layer)
        {
            collision.StandingOnLastFrame.layer = layer;
        }

        private void ApplyMovingPlatformBehavior()
        {
            SetOnMovingPlatform(true);
            MovingPlatformCurrentGravityInternal(PlatformerMovingPlatformGravity);
        }

        private void SetOnMovingPlatform(bool onMovingPlatform)
        {
            collision.OnMovingPlatform = onMovingPlatform;
        }

        private void MovingPlatformCurrentGravityInternal(float gravity)
        {
            collision.MovingPlatformCurrentGravity = gravity;
        }

        private void CurrentRaycastDirectionToLeft()
        {
            CurrentRaycastDirection(Left);
        }

        private void CurrentRaycastDirectionToRight()
        {
            CurrentRaycastDirection(Right);
        }

        private void CurrentRaycastDirection(Direction direction)
        {
            collision.CurrentRaycastDirection = direction;
        }

        private void HorizontalRaycast(GameObject character, float speedX)
        {
            HorizontalRaycastFromBottomInternal(HorizontalRaycastFromBottomInternal(character));
            HorizontalRaycastToTopInternal(HorizontalRaycastToTopInternal(character));
            HorizontalRaycastLengthInternal(HorizontalRaycastLength(speedX));
        }

        private Vector2 HorizontalRaycastFromBottomInternal(GameObject character)
        {
            return HorizontalRaycast(true, character);
        }

        private Vector2 HorizontalRaycastToTopInternal(GameObject character)
        {
            return HorizontalRaycast(false, character);
        }

        private Vector2 HorizontalRaycast(bool fromBottom, GameObject character)
        {
            Vector2 raycast;
            var properties = HorizontalRaycastProperties(character, obstacleHeightTolerance);
            if (fromBottom) raycast = InitialHorizontalRaycast(bounds.BottomRight, bounds.BottomLeft) + properties;
            else raycast = InitialHorizontalRaycast(bounds.TopLeft, bounds.TopRight) - properties;
            return raycast;
        }

        private static Vector2 InitialHorizontalRaycast(Vector2 bounds1, Vector2 bounds2)
        {
            return (bounds1 + bounds2) / 2;
        }

        private static Vector2 HorizontalRaycastProperties(GameObject character, float tolerance)
        {
            return (Vector2) character.transform.up * tolerance;
        }

        private void HorizontalRaycastFromBottomInternal(Vector2 raycast)
        {
            collision.HorizontalRaycastFromBottom = raycast;
        }

        private void HorizontalRaycastToTopInternal(Vector2 raycast)
        {
            collision.HorizontalRaycastToTop = raycast;
        }

        private void HorizontalRaycastLengthInternal(float length)
        {
            horizontalRaycastLength = length;
        }

        private float HorizontalRaycastLength(float speedX)
        {
            return Abs(speedX * deltaTime) + bounds.Width / 2 + RayOffset * 2;
        }

        private void ResizeHorizontalHitStorage()
        {
            collision.HorizontalHitStorage = new RaycastHit2D[NumberOfHorizontalRays];
        }

        private void HorizontalRaycastOriginInternal(int index)
        {
            RaycastOriginInternal(HorizontalRaycastOrigin(index));
        }

        private Vector2 HorizontalRaycastOrigin(int index)
        {
            return Lerp(collision.HorizontalRaycastFromBottom, collision.HorizontalRaycastToTop,
                index / (float) (NumberOfHorizontalRays - 1));
        }

        private void HorizontalHitAngleInternal(int index, GameObject character)
        {
            collision.HorizontalHitAngle = Abs(Angle(HorizontalHitStorage[index].normal, character.transform.up));
        }

        private void FrictionTest(int smallestDistanceIndex)
        {
            var testFrictionController = BelowHitStorage[smallestDistanceIndex].collider.gameObject
                .GetComponentNoAlloc<FrictionController>();
            var frictionNotNull = testFrictionController != null;
            FrictionNotNullInternal(frictionNotNull);
            if (frictionNotNull) FrictionController(testFrictionController);
        }

        private void FrictionController(FrictionController controller)
        {
            collision.FrictionController = controller;
        }

        private void FrictionInternal()
        {
            FrictionInternal(collision.FrictionController.Data.Friction);
        }

        private void FrictionNotNullInternal(bool notNull)
        {
            collision.FrictionNotNull = notNull;
        }

        private void FrictionInternal(float friction)
        {
            collision.Friction = friction;
        }

        private void MovingPlatformTest(int smallestDistanceIndex)
        {
            var testMovingPlatformController = BelowHitStorage[smallestDistanceIndex].collider.gameObject
                .GetComponentNoAlloc<MovingPlatformController>();
            var movingPlatformNotNull = testMovingPlatformController != null;
            MovingPlatformNotNullInternal(movingPlatformNotNull);
            if (movingPlatformNotNull && collision.IsGrounded) MovingPlatformController(testMovingPlatformController);
        }

        private void MovingPlatformNotNullInternal(bool notNull)
        {
            collision.MovingPlatformNotNull = notNull;
        }

        private void MovingPlatformController(MovingPlatformController controller)
        {
            collision.MovingPlatformController = controller;
        }

        private int CurrentDirectionNumerical
        {
            get
            {
                switch (collision.CurrentRaycastDirection)
                {
                    case Left:
                    case Down: return -1;
                    case Right:
                    case Up: return 1;
                    default: return 0;
                }
            }
        }

        private void HorizontalRaycastForPlatform(int index, GameObject character, LayerMask layer)
        {
            HorizontalHitStorageInternal(index, CurrentDirectionNumerical * character.transform.right, layer);
        }

        private void HorizontalHitStorageInternal(int index, Vector2 direction, LayerMask layer)
        {
            collision.HorizontalHitStorage[index] = RayCast(collision.RaycastOrigin, direction, horizontalRaycastLength,
                layer, blue, drawRaycastGizmosControl);
        }

        private void HorizontalHitStorageInternal(RaycastHit2D[] hitStorage)
        {
            collision.HorizontalHitStorage = hitStorage;
        }

        private void HorizontalRaycastForSpecialPlatforms(int index, GameObject character, LayerMask layer1,
            LayerMask layer2, LayerMask layer3)
        {
            HorizontalHitStorageInternal(index, CurrentDirectionNumerical * character.transform.right,
                layer1 & ~layer2 & ~layer3);
        }

        private void LateralSlopeAngle()
        {
            LateralSlopeAngle(HorizontalHitAngle);
        }

        private void LateralSlopeAngle(float angle)
        {
            collision.LateralSlopeAngle = angle;
        }

        private void SetCollisionOnHitWall(int index)
        {
            if (collision.CurrentRaycastDirection == Left) SetCollisionLeftOnHitWall(index);
            else SetCollisionRightOnHitWall(index);
        }

        private void SetCollisionLeftOnHitWall(int index)
        {
            CollisionLeft(true);
            DistanceToLeftCollider(HorizontalHitStorage[index].distance);
        }

        private void SetCollisionRightOnHitWall(int index)
        {
            CollisionRight(true);
            DistanceToRightCollider(HorizontalHitStorage[index].distance);
        }

        private void CollisionLeft(bool isColliding)
        {
            collision.Left = isColliding;
        }

        private void CollisionRight(bool isColliding)
        {
            collision.Right = isColliding;
        }

        private void DistanceToLeftCollider(float distance)
        {
            collision.DistanceToLeftCollider = distance;
        }

        private void DistanceToRightCollider(float distance)
        {
            collision.DistanceToRightCollider = distance;
        }

        private void SetCollisionOnHitWallInMovementDirection(int index)
        {
            CurrentWallCollider(HorizontalHitStorage[index].collider.gameObject);
            PassedSlopeAngle(true);
        }

        private void PassedSlopeAngle(bool passed)
        {
            collision.PassedSlopeAngle = passed;
        }

        private void CurrentWallCollider(GameObject wallCollider)
        {
            collision.CurrentWallCollider = wallCollider;
        }

        private void CurrentWallColliderNull()
        {
            collision.CurrentWallCollider = null;
        }

        private void AddHorizontalContact(int index)
        {
            collision.ContactList.Add(HorizontalHitStorage[index]);
        }

        private void CurrentRaycastDirectionToDown()
        {
            CurrentRaycastDirection(Down);
        }

        private void InitializeFriction()
        {
            FrictionInternal(0);
        }

        private void NotCollidingBelow()
        {
            CollisionBelow(false);
        }

        private void CollisionBelow(bool collisionBelow)
        {
            collision.Below = collisionBelow;
        }

        private float VerticalRaycastLength => bounds.Height / 2 + RayOffset;

        private void VerticalRaycastLengthInternal()
        {
            VerticalRaycastLengthInternal(VerticalRaycastLength);
        }

        private void VerticalRaycastLengthInternal(float length)
        {
            verticalRaycastLength = length;
        }

        private void SetVerticalRaycastLengthOnMovingPlatform()
        {
            ApplyFactorToVerticalRaycastLength(2);
        }

        private void ApplyFactorToVerticalRaycastLength(float factor)
        {
            verticalRaycastLength *= factor;
        }

        private void ApplyNewPositionYToVerticalRaycastLength(float newPositionY)
        {
            verticalRaycastLength += VerticalRaycastLengthNewPositionYApplied(newPositionY);
        }

        private static float VerticalRaycastLengthNewPositionYApplied(float newPositionY)
        {
            return Abs(newPositionY);
        }

        private void VerticalRaycast(GameObject character, float newPositionX)
        {
            VerticalRaycastFromLeft(VerticalRaycast(bounds.BottomLeft, bounds.TopLeft, character, newPositionX));
            VerticalRaycastToRight(VerticalRaycast(bounds.BottomRight, bounds.TopRight, character, newPositionX));
        }

        private void VerticalRaycastFromLeft(Vector2 raycastFromLeft)
        {
            verticalRaycastFromLeft = raycastFromLeft;
        }

        private void VerticalRaycastToRight(Vector2 raycastToRight)
        {
            verticalRaycastToRight = raycastToRight;
        }

        private Vector2 VerticalRaycast(Vector2 bounds1, Vector2 bounds2, GameObject character, float newPositionX)
        {
            return bounds1 + bounds2 / 2 + (Vector2) character.transform.up * RayOffset +
                   (Vector2) character.transform.right * newPositionX;
        }

        private void ResizeBelowHitStorage()
        {
            BelowHitStorageInternal(new RaycastHit2D[NumberOfVerticalRays]);
        }

        private void BelowHitStorageInternal(RaycastHit2D[] hitStorage)
        {
            collision.BelowHitStorage = hitStorage;
        }

        private static LayerMask Platforms => NameToLayer("Platforms");

        private void SetStandingOnLastFrameLayerToPlatforms()
        {
            SetStandingOnLastFrameLayer(Platforms);
        }

        private void BelowRaycastOriginInternal(int index)
        {
            RaycastOriginInternal(BelowRaycastOrigin(index));
        }

        private Vector2 BelowRaycastOrigin(int index)
        {
            return Lerp(verticalRaycastFromLeft, verticalRaycastToRight, index / (float) (NumberOfVerticalRays - 1));
        }

        private void SetBelowRaycastToBelowPlatformsWithoutOneWay(int index, GameObject character, LayerMask layer)
        {
            BelowHitStorageInternal(index, character, layer);
        }

        private void BelowHitStorageInternal(int index, GameObject character, LayerMask layer)
        {
            collision.BelowHitStorage[index] = RayCast(collision.RaycastOrigin, -character.transform.up,
                verticalRaycastLength, layer, blue, drawRaycastGizmosControl);
        }

        private void BelowRaycastToBelowPlatforms(int index, GameObject character, LayerMask layer)
        {
            BelowHitStorageInternal(index, character, layer);
        }

        private void BelowRaycastDistanceInternal(int smallestDistanceIndex)
        {
            collision.BelowDistance = DistanceBetweenPointAndLine(
                collision.BelowHitStorage[smallestDistanceIndex].point, verticalRaycastFromLeft,
                verticalRaycastToRight);
        }

        private void SetCollisionOnBelowRaycastHit(int index, GameObject character)
        {
            BelowSlopeAngleInternal(BelowSlopeAngleInternal(index, character));
            CrossBelowSlopeAngleInternal(CrossBelowSlopeAngleInternal(index, character));
        }

        private float BelowSlopeAngleInternal(int index, GameObject character)
        {
            return Angle(BelowHitStorage[index].normal, character.transform.up);
        }

        private Vector3 CrossBelowSlopeAngleInternal(int index, GameObject character)
        {
            return Cross(character.transform.up, BelowHitStorage[index].normal);
        }

        private void BelowSlopeAngleInternal(float angle)
        {
            collision.BelowSlopeAngle = angle;
        }

        private void CrossBelowSlopeAngleInternal(Vector3 cross)
        {
            collision.CrossBelowSlopeAngle = cross;
        }

        private void NegativeBelowSlopeAngle()
        {
            BelowSlopeAngleInternal(-collision.BelowSlopeAngle);
        }

        private void SetStandingOnOnSmallestHitConnected(int smallestDistanceIndex)
        {
            SetStandingOn(BelowHitStorage[smallestDistanceIndex].collider.gameObject);
            SetStandingOnCollider(BelowHitStorage[smallestDistanceIndex].collider);
        }

        private void SetStandingOn(GameObject gameObject)
        {
            collision.StandingOn = gameObject;
        }

        private void SetStandingOnCollider(Collider2D collider)
        {
            collision.StandingOnCollider = collider;
        }

        private void CollidingBelow()
        {
            CollisionBelow(false);
        }

        private void SetBelowRaycastDistanceOnSmallestDistanceHit(int smallestDistanceIndex)
        {
            collision.BelowDistance = DistanceBetweenPointAndLine(BelowHitStorage[smallestDistanceIndex].point,
                verticalRaycastFromLeft, verticalRaycastToRight);
        }

        private void SetCollisionOnDetachFromMovingPlatform()
        {
            SetOnMovingPlatform(false);
            MovingPlatformController(null);
            MovingPlatformCurrentGravityInternal(0);
        }

        private void StickToSlopeRayLengthInternal(float maximumSlopeAngle)
        {
            stickToSlopeRayLength = StickToSlopeRayLength(maximumSlopeAngle);
        }

        private float StickToSlopeRayLength(float maximumSlopeAngle)
        {
            return bounds.Width * Abs(maximumSlopeAngle) + (bounds.Height / 2 + RayOffset);
        }

        private void StickToSlopeRaycastInternal(float newPositionX, GameObject character, LayerMask layer)
        {
            RaycastOriginInternal(LeftStickToSlopeRaycastOrigin(newPositionX));
            LeftStickToSlopeRaycast(StickToSlopeRaycastInternal(character, layer));
            RaycastOriginInternal(RightStickToSlopeRaycastOrigin(newPositionX));
            RightStickToSlopeRaycast(StickToSlopeRaycastInternal(character, layer));
            StickToSlopeRaycastCastingLeft(false);
            BelowSlopeAngleLeftInternal(
                StickToSlopeBelowSlopeAngle(collision.LeftStickToSlopeRaycast.normal, character));
            CrossBelowSlopeAngleLeftInternal(
                StickToSlopeCrossBelowSlopeAngle(character, collision.LeftStickToSlopeRaycast.normal));
            BelowSlopeAngleRightInternal(StickToSlopeBelowSlopeAngle(collision.RightStickToSlopeRaycast.normal,
                character));
            CrossBelowSlopeAngleRightInternal(
                StickToSlopeCrossBelowSlopeAngle(character, collision.RightStickToSlopeRaycast.normal));
            BelowSlopeAngleInternal(0);
        }

        private Vector2 LeftStickToSlopeRaycastOrigin(float newPositionX)
        {
            return new Vector2(bounds.BottomLeft.x + newPositionX, bounds.Center.y);
        }

        private Vector2 RightStickToSlopeRaycastOrigin(float newPositionX)
        {
            return new Vector2(bounds.BottomRight.x + newPositionX, bounds.Center.y);
        }

        private RaycastHit2D StickToSlopeRaycastInternal(GameObject character, LayerMask layer)
        {
            return RayCast(collision.RaycastOrigin, -character.transform.up, stickToSlopeRayLength, layer, cyan,
                drawRaycastGizmosControl);
        }

        private void LeftStickToSlopeRaycast(RaycastHit2D raycast)
        {
            collision.LeftStickToSlopeRaycast = raycast;
        }

        private void RightStickToSlopeRaycast(RaycastHit2D raycast)
        {
            collision.RightStickToSlopeRaycast = raycast;
        }

        private void StickToSlopeRaycastCastingLeft(bool castingLeft)
        {
            collision.StickToSlopeRaycastCastingLeft = castingLeft;
        }

        private void BelowSlopeAngleLeftInternal(float angle)
        {
            collision.BelowSlopeAngleLeft = angle;
        }

        private static float StickToSlopeBelowSlopeAngle(Vector2 normal, GameObject character)
        {
            return Angle(normal, character.transform.up);
        }

        private static Vector3 StickToSlopeCrossBelowSlopeAngle(GameObject character, Vector2 normal)
        {
            return Cross(character.transform.up, normal);
        }

        private void CrossBelowSlopeAngleLeftInternal(Vector3 cross)
        {
            collision.CrossBelowSlopeAngleLeft = cross;
        }

        private void NegativeBelowSlopeAngleLeft()
        {
            BelowSlopeAngleLeftInternal(-collision.BelowSlopeAngleLeft);
        }

        private void BelowSlopeAngleRightInternal(float angle)
        {
            collision.BelowSlopeAngleRight = angle;
        }

        private void CrossBelowSlopeAngleRightInternal(Vector3 cross)
        {
            collision.CrossBelowSlopeAngleRight = cross;
        }

        private void NegativeBelowSlopeAngleRight()
        {
            BelowSlopeAngleRightInternal(-collision.BelowSlopeAngleRight);
        }

        private bool CastStickToSlopeRaycastLeft =>
            Abs(collision.BelowSlopeAngleLeft) > Abs(collision.BelowSlopeAngleRight);

        private void CastStickToSlopeRaycastLeftInternal()
        {
            CastStickToSlopeRaycastLeftInternal(CastStickToSlopeRaycastLeft);
        }

        private void CastStickToSlopeRaycastLeftInternal(bool castLeft)
        {
            collision.CastStickToSlopeRaycastLeft = castLeft;
        }

        private bool CastStickToSlopeRaycastLeftOnSlope => collision.BelowSlopeAngle < 0;

        private void SetStickToSlopeRaycastOnSlope()
        {
            BelowSlopeAngleInternal(collision.BelowSlopeAngleLeft);
            CastStickToSlopeRaycastLeftInternal(CastStickToSlopeRaycastLeftOnSlope);
        }

        private bool CastStickToSlopeRaycastLeftOnRightSlopeOnLeftGround => collision.BelowSlopeAngleRight < 0;

        private void SetStickToSlopeRaycastOnRightSlopeOnLeftGround()
        {
            BelowSlopeAngleInternal(collision.BelowSlopeAngleLeft);
            CastStickToSlopeRaycastLeftInternal(CastStickToSlopeRaycastLeftOnRightSlopeOnLeftGround);
        }

        private bool CastStickToSlopeRaycastLeftOnLeftSlopeOnRightGround => collision.BelowSlopeAngleLeft < 0;

        private void SetStickToSlopeRaycastOnLeftSlopeOnRightGround()
        {
            BelowSlopeAngleInternal(collision.BelowSlopeAngleRight);
            CastStickToSlopeRaycastLeftInternal(CastStickToSlopeRaycastLeftOnLeftSlopeOnRightGround);
        }

        private bool CastStickToSlopeRaycastLeftOnSlopes => collision.LeftStickToSlopeRaycast.distance <
                                                            collision.RightStickToSlopeRaycast.distance;

        private float BelowSlopeAngleOnStickToSlopeRaycastOnSlopes => collision.CastStickToSlopeRaycastLeft
            ? collision.BelowSlopeAngleLeft
            : collision.BelowSlopeAngleRight;

        private void SetStickToSlopeRaycastOnSlopes()
        {
            CastStickToSlopeRaycastLeftInternal(CastStickToSlopeRaycastLeftOnSlopes);
            BelowSlopeAngleInternal(BelowSlopeAngleOnStickToSlopeRaycastOnSlopes);
        }

        private void SetStickToSlopeRaycastOnMaximumAngle(GameObject character, LayerMask layer)
        {
            StickToSlopeRaycastInternal(StickToSlopeRaycastOnMaximumAngle(character, layer));
        }

        private RaycastHit2D StickToSlopeRaycastOnMaximumAngle(GameObject character, LayerMask layer)
        {
            var transformUp = character.transform.up;
            return BoxCast(bounds.Center, bounds.BoundsInternal, Angle(transformUp, up), -transformUp,
                stickToSlopeRayLength, layer, red, drawRaycastGizmosControl);
        }

        private void StickToSlopeRaycastInternal(RaycastHit2D raycast)
        {
            collision.StickToSlopeRaycast = raycast;
        }

        private void SetCollisionOnStickToSlopeRaycastHit()
        {
            CollisionBelow(true);
        }

        private RaycastHit2D StickToSlopeRaycastUpdated => collision.CastStickToSlopeRaycastLeft
            ? collision.LeftStickToSlopeRaycast
            : collision.RightStickToSlopeRaycast;

        private void UpdateStickToSlopeRaycast()
        {
            StickToSlopeRaycastInternal(StickToSlopeRaycastUpdated);
        }

        private void CurrentRaycastDirectionToUp()
        {
            CurrentRaycastDirection(Up);
        }

        private void AboveRaycastInternal(Vector2 newPosition, GameObject character) //float newPositionY)
        {
            AboveRaycastLengthInternal(AboveRaycastLength(newPosition.y));
            AboveRaycastHitConnected(false);
            AboveRaycastStartInternal(AboveRaycastStart(character, newPosition.x));
            AboveRaycastEnd(AboveRaycastEnd(character, newPosition.x));
        }

        private float AboveRaycastLength(float newPositionY)
        {
            return (collision.IsGrounded ? RayOffset : newPositionY) + bounds.Height / 2;
        }

        private void AboveRaycastHitConnected(bool hitConnected)
        {
            collision.AboveRaycastHitConnected = hitConnected;
        }

        private void AboveRaycastLengthInternal(float length)
        {
            aboveRaycastLength = length;
        }

        private Vector2 AboveRaycastStart(GameObject character, float newPositionX)
        {
            return AboveRaycast(bounds.BottomLeft, bounds.TopLeft, character, newPositionX);
        }

        private Vector2 AboveRaycastEnd(GameObject character, float newPositionX)
        {
            return AboveRaycast(bounds.BottomRight, bounds.TopRight, character, newPositionX);
        }

        private static Vector2 AboveRaycast(Vector2 bounds1, Vector2 bounds2, GameObject character, float newPositionX)
        {
            return (bounds1 + bounds2) / 2 + (Vector2) character.transform.right * newPositionX;
        }

        private void AboveRaycastStartInternal(Vector2 start)
        {
            aboveRaycastStart = start;
        }

        private void AboveRaycastEnd(Vector2 end)
        {
            aboveRaycastEnd = end;
        }

        private void ResizeAboveHitStorage()
        {
            AboveHitStorageInternal(new RaycastHit2D[NumberOfVerticalRays]);
        }

        private void AboveHitStorageInternal(RaycastHit2D[] aboveHitStorage)
        {
            collision.AboveHitStorage = aboveHitStorage;
        }

        private void UpdateAboveRaycast(int index, GameObject character, LayerMask layer)
        {
            RaycastOriginInternal(AboveRaycastOrigin(index));
            AboveHitStorageInternal(index, AboveRaycast(character, layer));
        }

        private RaycastHit2D AboveRaycast(GameObject character, LayerMask layer)
        {
            return RayCast(collision.RaycastOrigin, character.transform.up, aboveRaycastLength, layer, cyan,
                drawRaycastGizmosControl);
        }

        private void AboveHitStorageInternal(int index, RaycastHit2D raycast)
        {
            collision.AboveHitStorage[index] = raycast;
        }

        private Vector2 AboveRaycastOrigin(int index)
        {
            return Lerp(aboveRaycastStart, aboveRaycastEnd, (float) index / (NumberOfVerticalRays - 1));
        }

        private void SetCollisionOnAboveRaycastSmallestDistanceHit()
        {
            CollisionAbove(true);
        }

        private void CollisionAbove(bool above)
        {
            collision.Above = above;
        }

        private void SetStandingOnColliderContainsBottomCenterPosition()
        {
            var bottomCenter = new Vector3(bounds.BottomCenter.x, bounds.BottomCenter.y, 0);
            if (collision.StandingOnCollider == null)
                SetStandingOnColliderContainsBottomCenterPosition(
                    false); //StandingOnColliderContainsBottomCenterPosition = false;
            else if (collision.StandingOnCollider.bounds.Contains(bottomCenter))
                SetStandingOnColliderContainsBottomCenterPosition(true); // = true;
            else SetStandingOnColliderContainsBottomCenterPosition(false); // = false;
        }

        private void SetStandingOnColliderContainsBottomCenterPosition(bool containsBottomCenterPosition)
        {
            collision.StandingOnContainsBottomCenter = containsBottomCenterPosition;
        }

        private void SetStandingOnLastFrameNotNull()
        {
            collision.StandingOnLastFrameNotNull = collision.StandingOnLastFrame != null;
        }

        #endregion

        #region event handlers

        public void OnInitialize(ref BoxCollider2D collider, ref GameObject character, RaycastSettings settings)
        {
            Initialize(ref collider, ref character, settings);
        }

        public void OnInitializeFrame()
        {
            InitializeFrame();
        }

        public void OnUpdateBounds(BoxCollider2D collider, GameObject character)
        {
            UpdateBounds(collider, character);
        }

        public void OnPerformSafetyBoxcast(GameObject character, Vector2 newPosition, LayerMask platform)
        {
            PerformSafetyBoxcastInternal(character, newPosition, platform);
        }

        public void OnSetGroundedEvent()
        {
            GroundedEvent();
        }

        public void OnSetDistanceToGroundRaycast(GameObject character, LayerMask belowPlatforms)
        {
            DistanceToGroundRaycastInternal(character, belowPlatforms);
        }

        public void OnSetDistanceToGroundOnRaycastHit()
        {
            SetDistanceToGroundOnRaycastHit();
        }

        public void OnSetDistanceToGround()
        {
            DistanceToGround();
        }

        public void OnSetStandingOnLastFrameLayerToSavedBelow(LayerMask savedBelow)
        {
            SetStandingOnLastFrameLayerToSavedBelow(savedBelow);
        }

        public void OnApplyMovingPlatformBehavior()
        {
            ApplyMovingPlatformBehavior();
        }

        public void OnSetCurrentRaycastDirectionToLeft()
        {
            CurrentRaycastDirectionToLeft();
        }

        public void OnSetCurrentRaycastDirectionToRight()
        {
            CurrentRaycastDirectionToRight();
        }

        public void OnSetHorizontalRaycast(GameObject character, float speedX)
        {
            HorizontalRaycast(character, speedX);
        }

        public void OnResizeHorizontalHitStorage()
        {
            ResizeHorizontalHitStorage();
        }

        public void OnSetHorizontalHitAngle(int index, GameObject character)
        {
            HorizontalHitAngleInternal(index, character);
        }

        public void OnFrictionTest(int smallestDistanceIndex)
        {
            FrictionTest(smallestDistanceIndex);
        }

        public void OnSetFriction()
        {
            FrictionInternal();
        }

        public void OnMovingPlatformTest(int smallestDistanceIndex)
        {
            MovingPlatformTest(smallestDistanceIndex);
        }

        public void OnSetHorizontalRaycastOrigin(int index)
        {
            HorizontalRaycastOriginInternal(index);
        }

        public void OnSetHorizontalRaycastForPlatform(int index, GameObject character, LayerMask layer)
        {
            HorizontalRaycastForPlatform(index, character, layer);
        }

        public void OnSetHorizontalRaycastForSpecialPlatforms(int index, GameObject character, LayerMask layer1,
            LayerMask layer2, LayerMask layer3)
        {
            HorizontalRaycastForSpecialPlatforms(index, character, layer1, layer2, layer3);
        }

        public void OnSetLateralSlopeAngle()
        {
            LateralSlopeAngle();
        }

        public void OnSetCollisionOnHitWall(int index)
        {
            SetCollisionOnHitWall(index);
        }

        public void OnSetCollisionOnHitWallInMovementDirection(int index)
        {
            SetCollisionOnHitWallInMovementDirection(index);
        }

        public void OnAddHorizontalContact(int index)
        {
            AddHorizontalContact(index);
        }

        public void OnSetCurrentRaycastDirectionToDown()
        {
            CurrentRaycastDirectionToDown();
        }

        public void OnInitializeFriction()
        {
            InitializeFriction();
        }

        public void OnSetNotCollidingBelow()
        {
            NotCollidingBelow();
        }

        public void OnSetVerticalRaycastLength()
        {
            VerticalRaycastLengthInternal();
        }

        public void OnSetVerticalRaycastLengthOnMovingPlatform()
        {
            SetVerticalRaycastLengthOnMovingPlatform();
        }

        public void OnApplyNewPositionYToVerticalRaycastLength(float newPositionY)
        {
            ApplyNewPositionYToVerticalRaycastLength(newPositionY);
        }

        public void OnSetVerticalRaycast(GameObject character, float newPositionX)
        {
            VerticalRaycast(character, newPositionX);
        }

        public void OnResizeBelowHitStorage()
        {
            ResizeBelowHitStorage();
        }

        public void OnSetStandingOnLastFrameLayerToPlatforms()
        {
            SetStandingOnLastFrameLayerToPlatforms();
        }

        public void OnSetBelowRaycastOrigin(int index)
        {
            BelowRaycastOriginInternal(index);
        }

        public void OnSetBelowRaycastToBelowPlatformsWithoutOneWay(int index, GameObject character, LayerMask layer)
        {
            SetBelowRaycastToBelowPlatformsWithoutOneWay(index, character, layer);
        }

        public void OnSetBelowRaycastToBelowPlatforms(int index, GameObject character, LayerMask layer)
        {
            BelowRaycastToBelowPlatforms(index, character, layer);
        }

        public void OnSetBelowRaycastDistance(int smallestDistanceIndex)
        {
            BelowRaycastDistanceInternal(smallestDistanceIndex);
        }

        public void OnSetCollisionOnBelowRaycastHit(int index, GameObject character)
        {
            SetCollisionOnBelowRaycastHit(index, character);
        }

        public void OnSetNegativeBelowSlopeAngle()
        {
            NegativeBelowSlopeAngle();
        }

        public void OnSetStandingOnOnSmallestHitConnected(int smallestDistanceIndex)
        {
            SetStandingOnOnSmallestHitConnected(smallestDistanceIndex);
        }

        public void OnSetCollidingBelow()
        {
            CollidingBelow();
        }

        public void OnSetBelowRaycastDistanceOnSmallestDistanceHit(int smallestDistanceIndex)
        {
            SetBelowRaycastDistanceOnSmallestDistanceHit(smallestDistanceIndex);
        }

        public void OnSetCollisionOnDetachFromMovingPlatform()
        {
            SetCollisionOnDetachFromMovingPlatform();
        }

        public void OnSetStickToSlopeRayLength(float maximumSlopeAngle)
        {
            StickToSlopeRayLengthInternal(maximumSlopeAngle);
        }

        public void OnSetStickToSlopeRaycast(float newPositionX, GameObject character, LayerMask layer)
        {
            StickToSlopeRaycastInternal(newPositionX, character, layer);
        }

        public void OnSetNegativeBelowSlopeAngleLeft()
        {
            NegativeBelowSlopeAngleLeft();
        }

        public void OnSetNegativeBelowSlopeAngleRight()
        {
            NegativeBelowSlopeAngleRight();
        }

        public void OnSetCastStickToSlopeRaycastLeft()
        {
            CastStickToSlopeRaycastLeftInternal();
        }

        public void OnSetStickToSlopeRaycastOnSlope()
        {
            SetStickToSlopeRaycastOnSlope();
        }

        public void OnSetStickToSlopeRaycastOnRightSlopeOnLeftGround()
        {
            SetStickToSlopeRaycastOnRightSlopeOnLeftGround();
        }

        public void OnSetStickToSlopeRaycastOnLeftSlopeOnRightGround()
        {
            SetStickToSlopeRaycastOnLeftSlopeOnRightGround();
        }

        public void OnSetStickToSlopeRaycastOnSlopes()
        {
            SetStickToSlopeRaycastOnSlopes();
        }

        public void OnSetStickToSlopeRaycastOnMaximumAngle(GameObject character, LayerMask layer)
        {
            SetStickToSlopeRaycastOnMaximumAngle(character, layer);
        }

        public void OnSetCollisionOnStickToSlopeRaycastHit()
        {
            SetCollisionOnStickToSlopeRaycastHit();
        }

        public void OnUpdateStickToSlopeRaycast()
        {
            UpdateStickToSlopeRaycast();
        }

        public void OnSetCurrentRaycastDirectionToUp()
        {
            CurrentRaycastDirectionToUp();
        }

        public void OnSetAboveRaycast(Vector2 newPosition, GameObject character)
        {
            AboveRaycastInternal(newPosition, character);
        }

        public void OnResizeAboveHitStorage()
        {
            ResizeAboveHitStorage();
        }

        public void OnUpdateAboveRaycast(int index, GameObject character, LayerMask layer)
        {
            UpdateAboveRaycast(index, character, layer);
        }

        public void OnSetCollisionOnAboveRaycastSmallestDistanceHit()
        {
            SetCollisionOnAboveRaycastSmallestDistanceHit();
        }

        public void OnSetStandingOnColliderContainsBottomCenterPosition()
        {
            SetStandingOnColliderContainsBottomCenterPosition();
        }

        public void OnSetStandingOnLastFrameNotNull()
        {
            SetStandingOnLastFrameNotNull();
        }

        #endregion
    }
}
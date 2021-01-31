using System.Collections.Generic;
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
    using static ScriptableObjectExtensions;
    using static Debug;
    using static Vector2;
    using static Color;
    using static Quaternion;
    using static Physics2D;
    using static RaycastData.Direction;
    using static Mathf;
    using static Time;
    using static MathsExtensions;

    [CreateAssetMenu(fileName = "RaycastData", menuName = PlatformerRaycastDataPath, order = 0)]
    public class RaycastData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public bool CastRaysOnBothSides { get; private set; }
        public float MovingPlatformCurrentGravity { get; private set; }
        public bool PerformSafetyBoxcast { get; private set; }
        public RaycastHit2D SafetyBoxcastHit => collision.SafetyBoxcast;
        public bool IsGrounded => collision.IsGrounded;
        public bool OnMovingPlatform => collision.OnMovingPlatform;
        public float BelowSlopeAngle => collision.BelowSlopeAngle;
        public bool GroundedLastFrame => collision.GroundedLastFrame;
        public bool CollidingAbove => collision.Above;
        public bool Colliding => collision.Colliding;
        public IEnumerable<RaycastHit2D> ContactList => collision.ContactList;
        public float DistanceToGroundRaycastMaximumLength { get; private set; }
        public RaycastHit2D DistanceToGroundRaycastHit => collision.DistanceToGroundRaycast;
        public Collider2D IgnoredCollider => collision.IgnoredCollider;
        public GameObject StandingOnLastFrame => collision.StandingOnLastFrame;
        public bool StandingOnLastFrameNotNull => collision.StandingOnLastFrame != null;
        public bool MovingPlatformNotNull => collision.MovingPlatformNotNull;
        public Vector2 MovingPlatformCurrentSpeed => collision.MovingPlatformCurrentSpeed;
        public bool CollidedWithCeilingLastFrame => collision.CollidedWithCeilingLastFrame;
        public Direction CurrentDirection { get; private set; }
        public RaycastHit2D[] HorizontalHitStorage => collision.HorizontalHitStorage;
        public int NumberOfHorizontalRays { get; private set; }
        public float HorizontalHitAngle => collision.HorizontalHitAngle;
        public int NumberOfVerticalRays { get; private set; }
        public RaycastHit2D[] BelowHitStorage => collision.BelowHitStorage;
        public Collider2D StandingOnCollider => collision.StandingOnCollider;
        //public Vector2 ColliderBottomCenterPosition { get; private set; }
        public Vector3 CrossBelowSlopeAngle => collision.CrossBelowSlopeAngle;
        public float BelowRaycastDistance => collision.BelowDistance;
        public float BoundsHeight => bounds.Height;
        public GameObject StandingOn => collision.StandingOn;
        public bool FrictionNotNull => collision.FrictionNotNull;
        public float Friction => collision.Friction;
        public float StickToSlopeOffsetY { get; private set; }
        public float StickToSlopeRayLength { get; private set; }
        public Vector3 CrossBelowSlopeAngleLeft => collision.CrossBelowSlopeAngleLeft;
        public Vector3 CrossBelowSlopeAngleRight => collision.CrossBelowSlopeAngleRight;
        public float BelowSlopeAngleLeft => collision.BelowSlopeAngleLeft;
        public float BelowSlopeAngleRight => collision.BelowSlopeAngleRight;
        public RaycastHit2D StickToSlopeRaycast => collision.StickToSlopeRaycast;
        public RaycastHit2D[] AboveHitStorage => collision.AboveHitStorage;
        public Vector2 HorizontalRaycastFromBottom { get; private set; }
        public Vector2 HorizontalRaycastToTop { get; private set; }
        public float BoundsWidth => bounds.Width;
        public float RayOffset { get; private set; }
        public Vector2 RaycastOrigin { get; private set; }
        public bool StandingOnColliderContainsBottomCenterPosition { get; private set; }

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
        private const float MovingPlatformGravity = -500f;
        private float obstacleHeightTolerance;
        private float horizontalRaycastLength;
        private float verticalRaycastLength;
        private float aboveRaycastLength;
        private Vector2 verticalRaycastFromLeft;
        private Vector2 verticalRaycastToRight;
        private Vector2 aboveRaycastStart;
        private Vector2 aboveRaycastEnd;
        private Bounds bounds;
        private Collision collision;

        private struct Collision
        {
            public bool FrictionNotNull { get; set; }
            public bool MovingPlatformNotNull { get; set; }
            public bool Right { get; set; }
            public bool Left { get; set; }
            public bool Above { get; set; }
            public bool Below { get; set; }
            public bool Colliding => Right || Left || Above || Below;
            public float DistanceToLeftCollider { get; set; }
            public float DistanceToRightCollider { get; set; }
            public float LateralSlopeAngle { get; set; }
            public float BelowSlopeAngle { get; set; }
            public bool PassedSlopeAngle { get; set; }
            public bool OnMovingPlatform { get; set; }
            public bool IsGrounded => Below;
            public bool GroundedLastFrame { get; set; }
            public bool CollidedWithCeilingLastFrame { get; set; }
            public bool GroundedEvent { get; set; }
            public GameObject StandingOn { get; set; }
            public GameObject StandingOnLastFrame { get; set; }
            public Collider2D StandingOnCollider { get; set; }
            public GameObject CurrentWallCollider { get; set; }
            public FrictionController FrictionController { get; set; }
            public float Friction { get; set; }
            public float DistanceToGround { get; set; }
            public float BelowDistance { get; set; }
            public float HorizontalHitAngle { get; set; }
            public float MovingPlatformCurrentGravity { get; set; }
            public Collider2D IgnoredCollider { get; set; }
            public Vector3 CrossBelowSlopeAngle { get; set; }
            public Vector3 CrossBelowSlopeAngleLeft { get; set; }
            public Vector3 CrossBelowSlopeAngleRight { get; set; }
            public bool CastStickToSlopeRaycastLeft { get; set; }
            public bool AboveRaycastHitConnected { get; set; }
            public float BelowSlopeAngleLeft { get; set; }
            public float BelowSlopeAngleRight { get; set; }
            public MovingPlatformController MovingPlatformController { get; set; }
            public Vector2 MovingPlatformCurrentSpeed { get; set; }
            public RaycastHit2D[] HorizontalHitStorage { get; set; }
            public RaycastHit2D[] BelowHitStorage { get; set; }
            public RaycastHit2D[] AboveHitStorage { get; set; }
            public RaycastHit2D StickToSlopeRaycast { get; set; }
            public bool StickToSlopeRaycastCastingLeft { get; set; }
            public RaycastHit2D LeftStickToSlopeRaycast { get; set; }
            public RaycastHit2D RightStickToSlopeRaycast { get; set; }
            public RaycastHit2D DistanceToGroundRaycast { get; set; }
            public RaycastHit2D SafetyBoxcast { get; set; }
            public List<RaycastHit2D> ContactList { get; set; }

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

        private struct Bounds
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
            public Vector2 BoundsInternal => new Vector2(Width, Height);
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
            StickToSlopeRayLength = settings.stickToSlopeRaycastLength;
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
            collision.Friction = 0;
            collision.ContactList = new List<RaycastHit2D>();
            collision.HorizontalHitStorage = new RaycastHit2D[NumberOfHorizontalRays];
            collision.BelowHitStorage = new RaycastHit2D[NumberOfVerticalRays];
            collision.AboveHitStorage = new RaycastHit2D[NumberOfVerticalRays];
            collision.CurrentWallCollider = null;
            collision.Reset();
            UpdateBounds(collider, character);
            bounds.RaycastBounds = new Vector2(bounds.Width, bounds.Height);
            bounds.Top = (bounds.TopLeft + bounds.TopRight) / 2;
            bounds.Bottom = (bounds.BottomLeft + bounds.BottomRight) / 2;
            bounds.Right = (bounds.TopRight + bounds.BottomRight) / 2;
            bounds.Left = (bounds.TopLeft + bounds.BottomLeft) / 2;
            HorizontalRaycastFromBottom = zero;
            HorizontalRaycastToTop = zero;
            verticalRaycastFromLeft = zero;
            verticalRaycastToRight = zero;
            aboveRaycastStart = zero;
            aboveRaycastEnd = zero;
            RaycastOrigin = zero;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

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
            SetBounds(top, bottom, left, right);
            var topLeft = character.transform.TransformPoint(bounds.TopLeft);
            var topRight = character.transform.TransformPoint(bounds.TopRight);
            var bottomLeft = character.transform.TransformPoint(bounds.BottomLeft);
            var bottomRight = character.transform.TransformPoint(bounds.BottomRight);
            SetBounds(topLeft, topRight, bottomLeft, bottomRight);
            var width = Distance(bounds.BottomLeft, bounds.BottomRight);
            var height = Distance(bounds.BottomLeft, bounds.TopLeft);
            SetBounds(width, height);
            SetBoundsCenter(center);
            SetBoundsBottomCenter(bottomCenter);
        }
        
        private void SetBoundsBottomCenter(Vector2 bottomCenter)
        {
            bounds.BottomCenter = bottomCenter;
        }

        private void SetBoundsCenter(Vector2 center)
        {
            bounds.Center = center;
        }

        private void SetBounds(float width, float height)
        {
            SetBoundsWidth(width);
            SetBoundsHeight(height);
        }

        private void SetBoundsWidth(float width)
        {
            bounds.Width = width;
        }

        private void SetBoundsHeight(float height)
        {
            bounds.Height = height;
        }

        private void SetBounds(float boundsTop, float boundsBottom, float boundsLeft, float boundsRight)
        {
            SetBoundsTopLeft(boundsLeft, boundsTop);
            SetBoundsTopRight(boundsRight, boundsTop);
            SetBoundsBottomLeft(boundsLeft, boundsBottom);
            SetBoundsBottomRight(boundsRight, boundsBottom);
        }

        private void SetBounds(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {
            SetBoundsTopLeft(topLeft);
            SetBoundsTopRight(topRight);
            SetBoundsBottomLeft(bottomLeft);
            SetBoundsBottomRight(bottomRight);
        }

        private void SetBoundsTopLeft(float x, float y)
        {
            bounds.TopLeft = new Vector2(x, y);
        }

        private void SetBoundsTopLeft(Vector2 topLeft)
        {
            bounds.TopLeft = topLeft;
        }

        private void SetBoundsTopRight(float x, float y)
        {
            bounds.TopRight = new Vector2(x, y);
        }

        private void SetBoundsTopRight(Vector2 topRight)
        {
            bounds.TopRight = topRight;
        }

        private void SetBoundsBottomLeft(float x, float y)
        {
            bounds.BottomLeft = new Vector2(x, y);
        }

        private void SetBoundsBottomLeft(Vector2 bottomLeft)
        {
            bounds.BottomLeft = bottomLeft;
        }

        private void SetBoundsBottomRight(float x, float y)
        {
            bounds.BottomRight = new Vector2(x, y);
        }

        private void SetBoundsBottomRight(Vector2 bottomRight)
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

        private void SetGroundedEvent(bool groundedEvent = true)
        {
            collision.GroundedEvent = groundedEvent;
        }

        private Vector2 DistanceToGroundRaycastOrigin =>
            new Vector2(collision.BelowSlopeAngle < 0 ? bounds.BottomLeft.x : bounds.BottomRight.x, bounds.Center.y);

        private void SetDistanceToGroundRaycast(GameObject character, LayerMask belowPlatforms)
        {
            SetRaycastOrigin(DistanceToGroundRaycastOrigin);
            SetDistanceToGroundRaycast(DistanceToGroundRaycast(character, belowPlatforms));
        }

        private RaycastHit2D DistanceToGroundRaycast(GameObject character, LayerMask belowPlatforms)
        {
            return RayCast(RaycastOrigin, -character.transform.up, DistanceToGroundRaycastMaximumLength, belowPlatforms,
                blue, drawRaycastGizmosControl);
        }

        private void SetDistanceToGroundRaycast(RaycastHit2D raycast)
        {
            collision.DistanceToGroundRaycast = raycast;
        }

        private void SetRaycastOrigin(Vector2 origin)
        {
            RaycastOrigin = origin;
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
            SetDistanceToGround(DistanceToGroundOnRaycastHit);
        }

        private void SetDistanceToGround()
        {
            SetDistanceToGround(DistanceToGroundInternal);
        }

        private void SetDistanceToGround(float distance)
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
            SetMovingPlatformCurrentGravity(MovingPlatformGravity);
        }

        private void SetOnMovingPlatform(bool onMovingPlatform)
        {
            collision.OnMovingPlatform = onMovingPlatform;
        }

        private void SetMovingPlatformCurrentGravity(float gravity)
        {
            collision.MovingPlatformCurrentGravity = gravity;
        }

        private void SetCurrentRaycastDirectionToLeft()
        {
            SetDirection(Left);
        }

        private void SetCurrentRaycastDirectionToRight()
        {
            SetDirection(Right);
        }

        private void SetDirection(Direction direction)
        {
            CurrentDirection = direction;
        }

        private void SetHorizontalRaycast(GameObject character, float speedX)
        {
            SetHorizontalRaycastFromBottom(HorizontalRaycastFromBottomInternal(character));
            SetHorizontalRaycastToTop(HorizontalRaycastToTopInternal(character));
            SetHorizontalRaycastLength(HorizontalRaycastLength(speedX));
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

        private void SetHorizontalRaycastFromBottom(Vector2 raycast)
        {
            HorizontalRaycastFromBottom = raycast;
        }

        private void SetHorizontalRaycastToTop(Vector2 raycast)
        {
            HorizontalRaycastToTop = raycast;
        }

        private void SetHorizontalRaycastLength(float length)
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

        private void SetHorizontalRaycastOrigin(int index)
        {
            SetRaycastOrigin(HorizontalRaycastOrigin(index));
        }

        private Vector2 HorizontalRaycastOrigin(int index)
        {
            return Lerp(HorizontalRaycastFromBottom, HorizontalRaycastToTop,
                index / (float) (NumberOfHorizontalRays - 1));
        }

        private void SetHorizontalHitAngle(int index, GameObject character)
        {
            SetHorizontalHitAngle(HorizontalHitAngleInternal(index, character));
        }

        private float HorizontalHitAngleInternal(int index, GameObject character)
        {
            return Abs(Angle(HorizontalHitStorage[index].normal, character.transform.up));
        }

        private void SetHorizontalHitAngle(float angle)
        {
            collision.HorizontalHitAngle = angle;
        }

        private void FrictionTest(int smallestDistanceIndex)
        {
            var testFrictionController = BelowHitStorage[smallestDistanceIndex].collider.gameObject
                .GetComponentNoAlloc<FrictionController>();
            var frictionNotNull = testFrictionController != null;
            SetFrictionNotNull(frictionNotNull);
            if (frictionNotNull) SetFrictionController(testFrictionController);
        }

        private void SetFrictionController(FrictionController controller)
        {
            collision.FrictionController = controller;
        }

        private void SetFriction()
        {
            SetFriction(collision.FrictionController.Data.Friction);
        }

        private void SetFrictionNotNull(bool notNull)
        {
            collision.FrictionNotNull = notNull;
        }

        private void SetFriction(float friction)
        {
            collision.Friction = friction;
        }

        private void MovingPlatformTest(int smallestDistanceIndex)
        {
            var testMovingPlatformController = BelowHitStorage[smallestDistanceIndex].collider.gameObject
                .GetComponentNoAlloc<MovingPlatformController>();
            var movingPlatformNotNull = testMovingPlatformController != null;
            SetMovingPlatformNotNull(movingPlatformNotNull);
            if (movingPlatformNotNull && collision.IsGrounded)
                SetMovingPlatformController(testMovingPlatformController);
        }

        private void SetMovingPlatformNotNull(bool notNull)
        {
            collision.MovingPlatformNotNull = notNull;
        }

        private void SetMovingPlatformController(MovingPlatformController controller)
        {
            collision.MovingPlatformController = controller;
        }

        private int CurrentDirectionNumerical
        {
            get
            {
                switch (CurrentDirection)
                {
                    case Left:
                    case Down: return -1;
                    case Right:
                    case Up: return 1;
                    default: return 0;
                }
            }
        }

        private void SetHorizontalRaycastForPlatform(int index, GameObject character, LayerMask layer)
        {
            SetHorizontalHitStorage(index, CurrentDirectionNumerical * character.transform.right, layer);
        }

        private void SetHorizontalHitStorage(int index, Vector2 direction, LayerMask layer)
        {
            collision.HorizontalHitStorage[index] = RayCast(RaycastOrigin, direction, horizontalRaycastLength, layer,
                blue, drawRaycastGizmosControl);
        }

        private void SetHorizontalRaycastForSpecialPlatforms(int index, GameObject character, LayerMask layer1,
            LayerMask layer2, LayerMask layer3)
        {
            SetHorizontalHitStorage(index, CurrentDirectionNumerical * character.transform.right,
                layer1 & ~layer2 & ~layer3);
        }

        private void SetLateralSlopeAngle()
        {
            SetLateralSlopeAngle(HorizontalHitAngle);
        }

        private void SetLateralSlopeAngle(float angle)
        {
            collision.LateralSlopeAngle = angle;
        }

        private void SetCollisionOnHitWall(int index)
        {
            if (CurrentDirection == Left) SetCollisionLeftOnHitWall(index);
            else SetCollisionRightOnHitWall(index);
        }

        private void SetCollisionLeftOnHitWall(int index)
        {
            SetCollisionLeft(true);
            SetDistanceToLeftCollider(HorizontalHitStorage[index].distance);
        }

        private void SetCollisionRightOnHitWall(int index)
        {
            SetCollisionRight(true);
            SetDistanceToRightCollider(HorizontalHitStorage[index].distance);
        }

        private void SetCollisionLeft(bool isColliding)
        {
            collision.Left = isColliding;
        }

        private void SetCollisionRight(bool isColliding)
        {
            collision.Right = isColliding;
        }

        private void SetDistanceToLeftCollider(float distance)
        {
            collision.DistanceToLeftCollider = distance;
        }

        private void SetDistanceToRightCollider(float distance)
        {
            collision.DistanceToRightCollider = distance;
        }

        private void SetCollisionOnHitWallInMovementDirection(int index)
        {
            SetCurrentWallCollider(HorizontalHitStorage[index].collider.gameObject);
            SetPassedSlopeAngle(true);
        }

        private void SetPassedSlopeAngle(bool passed)
        {
            collision.PassedSlopeAngle = passed;
        }

        private void SetCurrentWallCollider(GameObject wallCollider)
        {
            collision.CurrentWallCollider = wallCollider;
        }

        private void AddHorizontalContact(int index)
        {
            collision.ContactList.Add(HorizontalHitStorage[index]);
        }

        private void SetCurrentRaycastDirectionToDown()
        {
            SetDirection(Down);
        }

        private void InitializeFriction()
        {
            SetFriction(0);
        }

        private void SetNotCollidingBelow()
        {
            SetCollisionBelow(false);
        }

        private void SetCollisionBelow(bool collisionBelow)
        {
            collision.Below = collisionBelow;
        }

        private float VerticalRaycastLength => bounds.Height / 2 + RayOffset;

        private void SetVerticalRaycastLength()
        {
            SetVerticalRaycastLength(VerticalRaycastLength);
        }

        private void SetVerticalRaycastLength(float length)
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

        private void SetVerticalRaycast(GameObject character, float newPositionX)
        {
            SetVerticalRaycastFromLeft(VerticalRaycast(bounds.BottomLeft, bounds.TopLeft, character, newPositionX));
            SetVerticalRaycastToRight(VerticalRaycast(bounds.BottomRight, bounds.TopRight, character, newPositionX));
        }

        private void SetVerticalRaycastFromLeft(Vector2 raycastFromLeft)
        {
            verticalRaycastFromLeft = raycastFromLeft;
        }

        private void SetVerticalRaycastToRight(Vector2 raycastToRight)
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
            SetBelowHitStorage(new RaycastHit2D[NumberOfVerticalRays]);
        }

        private void SetBelowHitStorage(RaycastHit2D[] hitStorage)
        {
            collision.BelowHitStorage = hitStorage;
        }

        private static LayerMask Platforms => NameToLayer("Platforms");

        private void SetStandingOnLastFrameLayerToPlatforms()
        {
            SetStandingOnLastFrameLayer(Platforms);
        }

        private void SetBelowRaycastOrigin(int index)
        {
            SetRaycastOrigin(BelowRaycastOrigin(index));
        }

        private Vector2 BelowRaycastOrigin(int index)
        {
            return Lerp(verticalRaycastFromLeft, verticalRaycastToRight, index / (float) (NumberOfVerticalRays - 1));
        }

        private void SetBelowRaycastToBelowPlatformsWithoutOneWay(int index, GameObject character, LayerMask layer)
        {
            SetBelowHitStorage(index, character, layer);
        }

        private void SetBelowHitStorage(int index, GameObject character, LayerMask layer)
        {
            collision.BelowHitStorage[index] = RayCast(RaycastOrigin, -character.transform.up, verticalRaycastLength,
                layer, blue, drawRaycastGizmosControl);
        }

        private void SetBelowRaycastToBelowPlatforms(int index, GameObject character, LayerMask layer)
        {
            SetBelowHitStorage(index, character, layer);
        }

        private void SetBelowRaycastDistance(int smallestDistanceIndex)
        {
            SetBelowRaycastDistance(BelowRaycastDistanceInternal(smallestDistanceIndex));
        }

        private float BelowRaycastDistanceInternal(int smallestDistanceIndex)
        {
            return DistanceBetweenPointAndLine(collision.BelowHitStorage[smallestDistanceIndex].point,
                verticalRaycastFromLeft, verticalRaycastToRight);
        }

        private void SetBelowRaycastDistance(float distance)
        {
            collision.BelowDistance = distance;
        }

        private void SetCollisionOnBelowRaycastHit(int index, GameObject character)
        {
            SetBelowSlopeAngle(BelowSlopeAngleInternal(index, character));
            SetCrossBelowSlopeAngle(CrossBelowSlopeAngleInternal(index, character));
        }

        private float BelowSlopeAngleInternal(int index, GameObject character)
        {
            return Angle(BelowHitStorage[index].normal, character.transform.up);
        }

        private Vector3 CrossBelowSlopeAngleInternal(int index, GameObject character)
        {
            return Cross(character.transform.up, BelowHitStorage[index].normal);
        }

        private void SetBelowSlopeAngle(float angle)
        {
            collision.BelowSlopeAngle = angle;
        }

        private void SetCrossBelowSlopeAngle(Vector3 cross)
        {
            collision.CrossBelowSlopeAngle = cross;
        }

        private void SetNegativeBelowSlopeAngle()
        {
            SetBelowSlopeAngle(-collision.BelowSlopeAngle);
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

        private void SetCollidingBelow()
        {
            SetCollisionBelow(false);
        }

        private void SetBelowRaycastDistanceOnSmallestDistanceHit(int smallestDistanceIndex)
        {
            SetBelowRaycastDistance(BelowRaycastDistanceOnSmallestDistanceHit(smallestDistanceIndex));
        }

        private float BelowRaycastDistanceOnSmallestDistanceHit(int smallestDistanceIndex)
        {
            return DistanceBetweenPointAndLine(BelowHitStorage[smallestDistanceIndex].point, verticalRaycastFromLeft,
                verticalRaycastToRight);
        }

        private void SetCollisionOnDetachFromMovingPlatform()
        {
            SetOnMovingPlatform(false);
            SetMovingPlatformController(null);
            SetMovingPlatformCurrentGravity(0);
        }

        private void SetStickToSlopeRayLength(float length)
        {
            StickToSlopeRayLength = length;
        }

        private void SetStickToSlopeRayLengthInternal(float maximumSlopeAngle)
        {
            SetStickToSlopeRayLength(StickToSlopeRayLengthInternal(maximumSlopeAngle));
        }

        private float StickToSlopeRayLengthInternal(float maximumSlopeAngle)
        {
            return bounds.Width * Abs(maximumSlopeAngle) + (bounds.Height / 2 + RayOffset);
        }

        private void SetStickToSlopeRaycast(float newPositionX, GameObject character, LayerMask layer)
        {
            SetRaycastOrigin(LeftStickToSlopeRaycastOrigin(newPositionX));
            SetLeftStickToSlopeRaycast(StickToSlopeRaycastInternal(character, layer));
            SetRaycastOrigin(RightStickToSlopeRaycastOrigin(newPositionX));
            SetRightStickToSlopeRaycast(StickToSlopeRaycastInternal(character, layer));
            SetStickToSlopeRaycastCastingLeft(false);
            SetBelowSlopeAngleLeft(StickToSlopeBelowSlopeAngle(collision.LeftStickToSlopeRaycast.normal, character));
            SetCrossBelowSlopeAngleLeft(
                StickToSlopeCrossBelowSlopeAngle(character, collision.LeftStickToSlopeRaycast.normal));
            SetBelowSlopeAngleRight(StickToSlopeBelowSlopeAngle(collision.RightStickToSlopeRaycast.normal, character));
            SetCrossBelowSlopeAngleRight(
                StickToSlopeCrossBelowSlopeAngle(character, collision.RightStickToSlopeRaycast.normal));
            SetBelowSlopeAngle(0);
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
            return RayCast(RaycastOrigin, -character.transform.up, StickToSlopeRayLength, layer, cyan,
                drawRaycastGizmosControl);
        }

        private void SetLeftStickToSlopeRaycast(RaycastHit2D raycast)
        {
            collision.LeftStickToSlopeRaycast = raycast;
        }

        private void SetRightStickToSlopeRaycast(RaycastHit2D raycast)
        {
            collision.RightStickToSlopeRaycast = raycast;
        }

        private void SetStickToSlopeRaycastCastingLeft(bool castingLeft)
        {
            collision.StickToSlopeRaycastCastingLeft = castingLeft;
        }

        private void SetBelowSlopeAngleLeft(float angle)
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

        private void SetCrossBelowSlopeAngleLeft(Vector3 cross)
        {
            collision.CrossBelowSlopeAngleLeft = cross;
        }

        private void SetNegativeBelowSlopeAngleLeft()
        {
            SetBelowSlopeAngleLeft(-collision.BelowSlopeAngleLeft);
        }

        private void SetBelowSlopeAngleRight(float angle)
        {
            collision.BelowSlopeAngleRight = angle;
        }

        private void SetCrossBelowSlopeAngleRight(Vector3 cross)
        {
            collision.CrossBelowSlopeAngleRight = cross;
        }

        private void SetNegativeBelowSlopeAngleRight()
        {
            SetBelowSlopeAngleRight(-collision.BelowSlopeAngleRight);
        }

        private bool CastStickToSlopeRaycastLeft =>
            Abs(collision.BelowSlopeAngleLeft) > Abs(collision.BelowSlopeAngleRight);

        private void SetCastStickToSlopeRaycastLeft()
        {
            SetCastStickToSlopeRaycastLeft(CastStickToSlopeRaycastLeft);
        }

        private void SetCastStickToSlopeRaycastLeft(bool castLeft)
        {
            collision.CastStickToSlopeRaycastLeft = castLeft;
        }

        private bool CastStickToSlopeRaycastLeftOnSlope => collision.BelowSlopeAngle < 0;

        private void SetStickToSlopeRaycastOnSlope()
        {
            SetBelowSlopeAngle(collision.BelowSlopeAngleLeft);
            SetCastStickToSlopeRaycastLeft(CastStickToSlopeRaycastLeftOnSlope);
        }

        private bool CastStickToSlopeRaycastLeftOnRightSlopeOnLeftGround => collision.BelowSlopeAngleRight < 0;

        private void SetStickToSlopeRaycastOnRightSlopeOnLeftGround()
        {
            SetBelowSlopeAngle(collision.BelowSlopeAngleLeft);
            SetCastStickToSlopeRaycastLeft(CastStickToSlopeRaycastLeftOnRightSlopeOnLeftGround);
        }

        private bool CastStickToSlopeRaycastLeftOnLeftSlopeOnRightGround => collision.BelowSlopeAngleLeft < 0;

        private void SetStickToSlopeRaycastOnLeftSlopeOnRightGround()
        {
            SetBelowSlopeAngle(collision.BelowSlopeAngleRight);
            SetCastStickToSlopeRaycastLeft(CastStickToSlopeRaycastLeftOnLeftSlopeOnRightGround);
        }

        private bool CastStickToSlopeRaycastLeftOnSlopes => collision.LeftStickToSlopeRaycast.distance <
                                                            collision.RightStickToSlopeRaycast.distance;

        private float BelowSlopeAngleOnStickToSlopeRaycastOnSlopes => collision.CastStickToSlopeRaycastLeft
            ? collision.BelowSlopeAngleLeft
            : collision.BelowSlopeAngleRight;

        private void SetStickToSlopeRaycastOnSlopes()
        {
            SetCastStickToSlopeRaycastLeft(CastStickToSlopeRaycastLeftOnSlopes);
            SetBelowSlopeAngle(BelowSlopeAngleOnStickToSlopeRaycastOnSlopes);
        }

        private void SetStickToSlopeRaycastOnMaximumAngle(GameObject character, LayerMask layer)
        {
            SetStickToSlopeRaycast(StickToSlopeRaycastOnMaximumAngle(character, layer));
        }

        private RaycastHit2D StickToSlopeRaycastOnMaximumAngle(GameObject character, LayerMask layer)
        {
            var transformUp = character.transform.up;
            return BoxCast(bounds.Center, bounds.BoundsInternal, Angle(transformUp, up), -transformUp,
                StickToSlopeRayLength, layer, red, drawRaycastGizmosControl);
        }

        private void SetStickToSlopeRaycast(RaycastHit2D raycast)
        {
            collision.StickToSlopeRaycast = raycast;
        }

        private void SetCollisionOnStickToSlopeRaycastHit()
        {
            SetCollisionBelow(true);
        }

        private RaycastHit2D StickToSlopeRaycastUpdated => collision.CastStickToSlopeRaycastLeft
            ? collision.LeftStickToSlopeRaycast
            : collision.RightStickToSlopeRaycast;

        private void UpdateStickToSlopeRaycast()
        {
            SetStickToSlopeRaycast(StickToSlopeRaycastUpdated);
        }

        private void SetCurrentRaycastDirectionToUp()
        {
            SetDirection(Up);
        }

        private void SetAboveRaycast(Vector2 newPosition, GameObject character) //float newPositionY)
        {
            SetAboveRaycastLength(AboveRaycastLength(newPosition.y));
            SetAboveRaycastHitConnected(false);
            SetAboveRaycastStart(AboveRaycastStart(character, newPosition.x));
            SetAboveRaycastEnd(AboveRaycastEnd(character, newPosition.x));
        }

        private float AboveRaycastLength(float newPositionY)
        {
            return (collision.IsGrounded ? RayOffset : newPositionY) + bounds.Height / 2;
        }

        private void SetAboveRaycastHitConnected(bool hitConnected)
        {
            collision.AboveRaycastHitConnected = hitConnected;
        }

        private void SetAboveRaycastLength(float length)
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

        private void SetAboveRaycastStart(Vector2 start)
        {
            aboveRaycastStart = start;
        }

        private void SetAboveRaycastEnd(Vector2 end)
        {
            aboveRaycastEnd = end;
        }

        private void ResizeAboveHitStorage()
        {
            SetAboveHitStorage(new RaycastHit2D[NumberOfVerticalRays]);
        }

        private void SetAboveHitStorage(RaycastHit2D[] aboveHitStorage)
        {
            collision.AboveHitStorage = aboveHitStorage;
        }

        private void UpdateAboveRaycast(int index, GameObject character, LayerMask layer)
        {
            SetRaycastOrigin(AboveRaycastOrigin(index));
            SetAboveHitStorage(index, AboveRaycast(character, layer));
        }

        private RaycastHit2D AboveRaycast(GameObject character, LayerMask layer)
        {
            return RayCast(RaycastOrigin, character.transform.up, aboveRaycastLength, layer, cyan,
                drawRaycastGizmosControl);
        }

        private void SetAboveHitStorage(int index, RaycastHit2D raycast)
        {
            collision.AboveHitStorage[index] = raycast;
        }

        private Vector2 AboveRaycastOrigin(int index)
        {
            return Lerp(aboveRaycastStart, aboveRaycastEnd, (float) index / (NumberOfVerticalRays - 1));
        }

        private void SetCollisionOnAboveRaycastSmallestDistanceHit()
        {
            SetCollisionAbove(true);
        }

        private void SetCollisionAbove(bool above)
        {
            collision.Above = above;
        }

        private void SetStandingOnColliderContainsBottomCenterPosition()
        {
            var bottomCenter = new Vector3(bounds.BottomCenter.x, bounds.BottomCenter.y, 0);
            if (collision.StandingOnCollider == null) StandingOnColliderContainsBottomCenterPosition = false;
            else if (collision.StandingOnCollider.bounds.Contains(bottomCenter)) StandingOnColliderContainsBottomCenterPosition = true;
            else StandingOnColliderContainsBottomCenterPosition = false;
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
            SetGroundedEvent();
        }

        public void OnSetDistanceToGroundRaycast(GameObject character, LayerMask belowPlatforms)
        {
            SetDistanceToGroundRaycast(character, belowPlatforms);
        }

        public void OnSetDistanceToGroundOnRaycastHit()
        {
            SetDistanceToGroundOnRaycastHit();
        }

        public void OnSetDistanceToGround()
        {
            SetDistanceToGround();
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
            SetCurrentRaycastDirectionToLeft();
        }

        public void OnSetCurrentRaycastDirectionToRight()
        {
            SetCurrentRaycastDirectionToRight();
        }

        public void OnSetHorizontalRaycast(GameObject character, float speedX)
        {
            SetHorizontalRaycast(character, speedX);
        }

        public void OnResizeHorizontalHitStorage()
        {
            ResizeHorizontalHitStorage();
        }

        public void OnSetHorizontalHitAngle(int index, GameObject character)
        {
            SetHorizontalHitAngle(index, character);
        }

        public void OnFrictionTest(int smallestDistanceIndex)
        {
            FrictionTest(smallestDistanceIndex);
        }

        public void OnSetFriction()
        {
            SetFriction();
        }

        public void OnMovingPlatformTest(int smallestDistanceIndex)
        {
            MovingPlatformTest(smallestDistanceIndex);
        }

        public void OnSetHorizontalRaycastOrigin(int index)
        {
            SetHorizontalRaycastOrigin(index);
        }

        public void OnSetHorizontalRaycastForPlatform(int index, GameObject character, LayerMask layer)
        {
            SetHorizontalRaycastForPlatform(index, character, layer);
        }

        public void OnSetHorizontalRaycastForSpecialPlatforms(int index, GameObject character, LayerMask layer1,
            LayerMask layer2, LayerMask layer3)
        {
            SetHorizontalRaycastForSpecialPlatforms(index, character, layer1, layer2, layer3);
        }

        public void OnSetLateralSlopeAngle()
        {
            SetLateralSlopeAngle();
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
            SetCurrentRaycastDirectionToDown();
        }

        public void OnInitializeFriction()
        {
            InitializeFriction();
        }

        public void OnSetNotCollidingBelow()
        {
            SetNotCollidingBelow();
        }

        public void OnSetVerticalRaycastLength()
        {
            SetVerticalRaycastLength();
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
            SetVerticalRaycast(character, newPositionX);
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
            SetBelowRaycastOrigin(index);
        }

        public void OnSetBelowRaycastToBelowPlatformsWithoutOneWay(int index, GameObject character, LayerMask layer)
        {
            SetBelowRaycastToBelowPlatformsWithoutOneWay(index, character, layer);
        }

        public void OnSetBelowRaycastToBelowPlatforms(int index, GameObject character, LayerMask layer)
        {
            SetBelowRaycastToBelowPlatforms(index, character, layer);
        }

        public void OnSetBelowRaycastDistance(int smallestDistanceIndex)
        {
            SetBelowRaycastDistance(smallestDistanceIndex);
        }

        public void OnSetCollisionOnBelowRaycastHit(int index, GameObject character)
        {
            SetCollisionOnBelowRaycastHit(index, character);
        }

        public void OnSetNegativeBelowSlopeAngle()
        {
            SetNegativeBelowSlopeAngle();
        }

        public void OnSetStandingOnOnSmallestHitConnected(int smallestDistanceIndex)
        {
            SetStandingOnOnSmallestHitConnected(smallestDistanceIndex);
        }

        public void OnSetCollidingBelow()
        {
            SetCollidingBelow();
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
            SetStickToSlopeRayLengthInternal(maximumSlopeAngle);
        }

        public void OnSetStickToSlopeRaycast(float newPositionX, GameObject character, LayerMask layer)
        {
            SetStickToSlopeRaycast(newPositionX, character, layer);
        }

        public void OnSetNegativeBelowSlopeAngleLeft()
        {
            SetNegativeBelowSlopeAngleLeft();
        }

        public void OnSetNegativeBelowSlopeAngleRight()
        {
            SetNegativeBelowSlopeAngleRight();
        }

        public void OnSetCastStickToSlopeRaycastLeft()
        {
            SetCastStickToSlopeRaycastLeft();
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
            SetCurrentRaycastDirectionToUp();
        }
        public void OnSetAboveRaycast(Vector2 newPosition, GameObject character)
        {
            SetAboveRaycast(newPosition, character);
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

        #endregion
    }
}
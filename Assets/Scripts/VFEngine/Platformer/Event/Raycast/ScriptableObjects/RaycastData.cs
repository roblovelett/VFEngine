using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static Mathf;
    using static Vector2;
    using static Physics2D;
    using static Debug;
    using static Color;

    [CreateAssetMenu(fileName = "RaycastData", menuName = PlatformerRaycastDataPath, order = 0)]
    public class RaycastData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public int Index { get; private set; }
        public int VerticalRays { get; private set; }
        public float SkinWidth { get; private set; }
        public float VerticalSpacing { get; private set; }
        public float HitAngle => Angle(Hit.normal, up);
        public RaycastHit2D Hit { get; private set; }
        public Vector2 BoundsBottomLeft => bounds.BottomLeft;
        public Vector2 BoundsBottomRight => bounds.BottomRight;
        public Vector2 Origin { get; private set; }
        public bool OnGround => collision.OnGround;
        public bool OnSlope => collision.OnSlope;
        public float GroundAngle => collision.GroundAngle;
        public int GroundDirectionAxis => collision.GroundDirectionAxis;
        public int HorizontalRays { get; private set; }
        public float HorizontalSpacing { get; private set; }
        public Vector2 BoundsTopLeft => bounds.TopLeft;
        public const float Tolerance = 0;
        public LayerMask GroundLayer => collision.GroundLayer;
        public RaycastHit2D VerticalHit => collision.VerticalHit;
        public bool CollidingBelow => collision.Below;
        public bool CollidingAbove => collision.Above;
        public float IgnorePlatformsTime { get; private set; }
        public float Length { get; private set; }

        #endregion

        #region fields

        private bool displayWarnings;
        private bool drawGizmos;
        private int totalHorizontalRays;
        private int totalVerticalRays;
        private float spacing;
        private float oneWayPlatformDelay;
        private float ladderClimbThreshold;
        private float ladderDelay;
        private Vector2 origin;
        private Bounds bounds;
        private Collision collision;

        private struct Bounds
        {
            public UnityEngine.Bounds _ { get; set; }
            public Vector2 TopLeft { get; set; }
            public Vector2 TopRight { get; set; }
            public Vector2 BottomLeft { get; set; }
            public Vector2 BottomRight { get; set; }
            public Vector2 Size { get; set; }
        }

        private struct Collision
        {
            public bool Above { get; set; }
            public bool Right { get; set; }
            public bool Below { get; set; }
            public bool Left { get; set; }
            public bool OnGround { get; set; }
            public bool OnSlope { get; set; }
            public int GroundDirectionAxis { get; set; }
            public float GroundAngle { get; set; }
            public LayerMask GroundLayer { get; set; }
            public RaycastHit2D HorizontalHit { get; set; }
            public RaycastHit2D VerticalHit { get; set; }
        }

        #endregion

        #region initialization

        private void Initialize(Collider2D boxCollider, RaycastSettings settings)
        {
            ApplySettings(settings);
            InitializeDefault();
            SetRaycastBounds(boxCollider);
            InitializeRaycastCollision();
            InitializeRaycastCount();
            InitializeRaycastSpacing();
        }

        private void ApplySettings(RaycastSettings settings)
        {
            spacing = settings.spacing;
            SkinWidth = settings.skinWidth;
            oneWayPlatformDelay = settings.oneWayPlatformDelay;
            ladderClimbThreshold = settings.ladderClimbThreshold;
            ladderDelay = settings.ladderDelay;
        }

        private void InitializeDefault()
        {
            Index = 0;
            Length = 0;
            IgnorePlatformsTime = 0;
            Origin = new Vector2();
            Hit = new RaycastHit2D();
        }

        private void InitializeRaycastCollision()
        {
            ResetRaycastCollision();
            InitializeRaycastCollisionDefault();
        }

        private bool OnSlopeCollision => collision.OnGround && collision.GroundAngle != 0;

        private void InitializeRaycastCollisionDefault()
        {
            collision.GroundLayer = 0;
            collision.OnSlope = OnSlopeCollision;
        }

        private void InitializeRaycastCount()
        {
            SetRaycastCount(bounds.Size);
        }

        private void InitializeRaycastSpacing()
        {
            SetRaycastSpacing(bounds.Size);
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void SetRaycastCount(Vector2 size)
        {
            HorizontalRays = RaycastCount(size.y);
            VerticalRays = RaycastCount(size.x);
        }

        private int RaycastCount(float size)
        {
            return (int) Round(size / spacing);
        }

        private void SetRaycastSpacing(Vector2 size)
        {
            HorizontalSpacing = RaycastSpacing(size.y, HorizontalRays);
            VerticalSpacing = RaycastSpacing(size.x, VerticalRays);
        }

        private static float RaycastSpacing(float axis, int rays)
        {
            return axis / (rays - 1);
        }

        private void ResetRaycastCollision()
        {
            collision.Above = false;
            collision.Right = false;
            SetCollisionBelow(false);
            collision.Left = false;
            collision.OnGround = false;
            collision.GroundDirectionAxis = 0;
            collision.GroundAngle = 0;
            collision.HorizontalHit = new RaycastHit2D();
            collision.VerticalHit = new RaycastHit2D();
        }

        private void SetRaycastBounds(Collider2D boxCollider)
        {
            bounds._ = boxCollider.bounds;
            bounds._.Expand(SkinWidth * -2);
            var min = bounds._.min;
            var max = bounds._.max;
            bounds.BottomLeft = new Vector2(min.x, min.y);
            bounds.BottomRight = new Vector2(max.x, min.y);
            bounds.TopLeft = new Vector2(min.x, max.y);
            bounds.TopRight = new Vector2(max.x, max.y);
        }

        private void InitializeFrame(Collider2D boxCollider)
        {
            ResetRaycastCollision();
            SetRaycastBounds(boxCollider);
        }

        private void SetGroundCollisionRaycast(int deltaMoveXDirectionAxis, int index, LayerMask layer)
        {
            SetIndex(index);
            SetRaycastOrigin(GroundCollisionRaycastOrigin(deltaMoveXDirectionAxis));
            SetRaycastHit(GroundCollisionRaycastHit(layer));
        }

        private float GroundCollisionRaycastOriginY => SkinWidth * 2;

        private Vector2 GroundCollisionRaycastOrigin(int deltaMoveXDirectionAxis)
        {
            var groundCollisionOrigin =
                RaycastOrigin(deltaMoveXDirectionAxis == 1, right, left, VerticalSpacing, Index);
            return ApplyToVectorY(groundCollisionOrigin, GroundCollisionRaycastOriginY);
        }

        private Vector2 RaycastOrigin(bool directionAxis, Vector2 direction1, Vector2 direction2, float raycastSpacing,
            int index)
        {
            return (directionAxis ? bounds.BottomLeft : bounds.BottomRight) +
                   (directionAxis ? direction1 : direction2) * raycastSpacing * index;
        }

        private static Vector2 ApplyToVectorY(Vector2 vector, float y)
        {
            return new Vector2(vector.x, vector.y + y);
        }

        private RaycastHit2D GroundCollisionRaycastHit(LayerMask layer)
        {
            return Raycast(Origin, down, SkinWidth * 4f, layer);
        }

        private Vector2 GroundCollisionRaycastDirection => down * SkinWidth * 2;

        private void OnGroundCollisionRaycastHitInternal()
        {
            SetCollisionOnGroundCollisionRaycastHit();
            CastRay(Origin, GroundCollisionRaycastDirection, blue);
        }

        private int RaycastHitDirection => (int) Sign(Hit.normal.x);
        private LayerMask RaycastHitLayer => Hit.collider.gameObject.layer;

        private void SetCollisionOnGroundCollisionRaycastHit()
        {
            SetCollisionOnRaycastHitGround(true);
            collision.GroundLayer = RaycastHitLayer;
            collision.VerticalHit = Hit;
            SetCollisionBelow(true);
        }

        private void SetCollisionOnRaycastHitGround(bool onGround)
        {
            collision.OnGround = onGround;
            collision.GroundAngle = HitAngle;
            collision.GroundDirectionAxis = RaycastHitDirection;
        }

        private static void CastRay(Vector2 start, Vector2 direction, Color color)
        {
            DrawRay(start, direction, color);
        }

        private void SlopeBehavior()
        {
            SetCollisionBelow(true);
        }

        private void SetCollisionBelow(bool colliding)
        {
            collision.Below = colliding;
        }

        private void SetHorizontalCollisionRaycast(int index, int deltaMoveXDirectionAxis, float deltaMoveX,
            LayerMask layer)
        {
            SetIndex(index);
            SetRaycastLength(HorizontalCollisionRaycastLength(deltaMoveX));
            SetRaycastOrigin(HorizontalCollisionRaycastOrigin(deltaMoveXDirectionAxis));
            SetRaycastHit(HorizontalCollisionRaycastHit(deltaMoveXDirectionAxis, layer));
            CastRay(Origin, right * deltaMoveXDirectionAxis * Length, red);
        }

        private void SetRaycastLength(float length)
        {
            Length = length;
        }

        private float HorizontalCollisionRaycastLength(float deltaMoveX)
        {
            return Abs(deltaMoveX) + SkinWidth;
        }

        private RaycastHit2D HorizontalCollisionRaycastHit(int deltaMoveXDirectionAxis, LayerMask layer)
        {
            return RaycastHit(Origin, right * deltaMoveXDirectionAxis, Length, layer);
        }

        private static RaycastHit2D RaycastHit(Vector2 raycastOrigin, Vector2 direction, float distance,
            LayerMask layer)
        {
            return Raycast(raycastOrigin, direction, distance, layer);
        }

        private Vector2 HorizontalCollisionRaycastOrigin(int deltaMoveXDirectionAxis)
        {
            return RaycastOrigin(deltaMoveXDirectionAxis == -1, Index, HorizontalSpacing, up);
        }

        private Vector2 RaycastOrigin(bool directionAxis, int index, float raycastSpacing, Vector2 direction)
        {
            return (directionAxis ? bounds.BottomLeft : bounds.BottomRight) + direction * (raycastSpacing * index);
            ;
        }

        private void SetRaycastOrigin(Vector2 raycastOrigin)
        {
            Origin = raycastOrigin;
        }

        private void SetRaycastHit(RaycastHit2D hit)
        {
            Hit = hit;
        }

        private void SetIndex(int index)
        {
            Index = index;
        }
        
        private float HorizontalCollisionHitClimbingSlopeLength(float deltaMoveDistanceX)
        {
            return Min(deltaMoveDistanceX + SkinWidth, Hit.distance);
        }

        #endregion

        #region event handlers

        public void OnInitialize(BoxCollider2D boxCollider, RaycastSettings settings)
        {
            Initialize(boxCollider, settings);
        }

        public void OnInitializeFrame(BoxCollider2D boxCollider)
        {
            InitializeFrame(boxCollider);
        }

        public void OnSetGroundCollisionRaycast(int deltaMoveXDirectionAxis, int index, LayerMask layer)
        {
            SetGroundCollisionRaycast(deltaMoveXDirectionAxis, index, layer);
        }

        public void OnSetGroundCollisionRaycastHit(LayerMask layer)
        {
            SetRaycastHit(GroundCollisionRaycastHit(layer));
        }

        public void OnGroundCollisionRaycastHit()
        {
            OnGroundCollisionRaycastHitInternal();
        }

        public void OnSlopeBehavior()
        {
            SlopeBehavior();
        }

        public void OnSetHorizontalCollisionRaycast(int index, int deltaMoveXDirectionAxis, float deltaMoveX,
            LayerMask layer)
        {
            SetHorizontalCollisionRaycast(index, deltaMoveXDirectionAxis, deltaMoveX, layer);
        }

        public void OnHorizontalCollisionRaycastHitClimbingSlope()
        {
            SetCollisionOnRaycastHitGround(true);
        }

        public void OnHorizontalCollisionRaycastHitClimbingSlope(float deltaMoveDistanceX)
        {
            SetRaycastLength(HorizontalCollisionHitClimbingSlopeLength(deltaMoveDistanceX));
        }

        public void OnHorizontalCollisionRaycastHitMaximumSlope(float deltaMoveDistanceX)
        {
            SetRaycastLength(HorizontalCollisionHitClimbingSlopeLength(deltaMoveDistanceX));
        }


        #endregion
    }
}
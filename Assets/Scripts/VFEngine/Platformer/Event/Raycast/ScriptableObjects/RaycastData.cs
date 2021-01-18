using UnityEngine;
using VFEngine.Tools;

// ReSharper disable NotAccessedField.Local
// ReSharper disable UnusedAutoPropertyAccessor.Local
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
            SetIndex(0);
            SetRaycastLength(0);
            IgnorePlatformsTime = 0;
            SetRaycastOrigin(new Vector2());
            SetRaycastHit(new RaycastHit2D());
        }

        private void InitializeRaycastCollision()
        {
            ResetRaycastCollision();
            InitializeRaycastCollisionDefault();
        }

        private bool OnSlopeCollision => collision.OnGround && collision.GroundAngle != 0;

        private void InitializeRaycastCollisionDefault()
        {
            SetCollisionGroundLayer(0);
            collision.OnSlope = OnSlopeCollision;
        }

        private void SetCollisionGroundLayer(LayerMask layer)
        {
            collision.GroundLayer = layer;
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
            SetCollisionAbove(false);
            SetCollisionRight(false);
            SetCollisionBelow(false);
            SetCollisionLeft(false);
            SetCollisionOnGround(false);
            SetGroundCollision(0, 0);
            SetCollisionHorizontalHit(new RaycastHit2D());
            SetCollisionVerticalHit(new RaycastHit2D());
        }

        private void SetCollisionRight(bool colliding)
        {
            collision.Right = colliding;
        }

        private void SetCollisionLeft(bool colliding)
        {
            collision.Left = colliding;
        }

        private void SetCollisionOnGround(bool colliding)
        {
            collision.OnGround = colliding;
        }

        private void SetCollisionGroundDirectionAxis(int axis)
        {
            collision.GroundDirectionAxis = axis;
        }

        private void SetCollisionHorizontalHit(RaycastHit2D hit)
        {
            collision.HorizontalHit = hit;
        }

        private void SetCollisionVerticalHit(RaycastHit2D hit)
        {
            collision.VerticalHit = hit;
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
            SetCollisionGroundLayer(RaycastHitLayer);
            SetCollisionVerticalHit(Hit);
            SetCollisionBelow(true);
        }

        private void SetCollisionOnRaycastHitGround(bool onGround)
        {
            SetCollisionOnGround(onGround);
            SetGroundCollision(HitAngle, RaycastHitDirection);
        }

        private void SetCollisionGroundAngle(float angle)
        {
            collision.GroundAngle = angle;
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

        private void SetHorizontalCollisionRaycast(int index, int deltaMoveXDirectionAxis, float deltaMoveDistanceX,
            LayerMask layer)
        {
            SetIndex(index);
            SetRaycastLength(RaycastLength(deltaMoveDistanceX));
            SetRaycastOrigin(HorizontalCollisionRaycastOrigin(deltaMoveXDirectionAxis));
            SetRaycastHit(HorizontalCollisionRaycastHit(deltaMoveXDirectionAxis, layer));
            CastRay(Origin, right * deltaMoveXDirectionAxis * Length, red);
        }

        private void SetRaycastLength(float length)
        {
            Length = length;
        }

        private float RaycastLength(float deltaMoveDistance)
        {
            return deltaMoveDistance + SkinWidth;
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

        private void SetCollisionOnHorizontalCollisionHitMaximumSlope(int deltaMoveXDirectionAxis)
        {
            SetCollisionLeft(deltaMoveXDirectionAxis < 0);
            SetCollisionRight(deltaMoveXDirectionAxis > 0);
            SetCollisionHorizontalHit(Hit);
        }

        private void StopHorizontalSpeed(LayerMask layer)
        {
            SetRaycastOrigin(bounds.BottomRight);
            SetCollisionHorizontalHit(StopHorizontalSpeedHit(layer));
        }

        private Vector2 StopHorizontalSpeedRaycastDirection => left * collision.GroundDirectionAxis;

        private RaycastHit2D StopHorizontalSpeedHit(LayerMask layer)
        {
            return Raycast(Origin, StopHorizontalSpeedRaycastDirection, 1f, layer);
        }

        private void SetVerticalCollisionRaycast(int index, int deltaMoveYDirectionAxis, float deltaMoveDistanceY,
            float deltaMoveX, LayerMask layer)
        {
            SetIndex(index);
            SetRaycastLength(RaycastLength(deltaMoveDistanceY));
            SetRaycastOrigin(VerticalCollisionRaycastOrigin(deltaMoveYDirectionAxis, deltaMoveX));
            SetRaycastHit(VerticalCollisionRaycast(deltaMoveYDirectionAxis, layer));
            CastRay(Origin, up * deltaMoveYDirectionAxis * Length, red);
        }

        private Vector2 VerticalCollisionRaycastOrigin(int deltaMoveYDirectionAxis, float deltaMoveX)
        {
            return (deltaMoveYDirectionAxis == -1 ? bounds.BottomLeft : bounds.TopLeft) +
                   right * (VerticalSpacing * Index * deltaMoveX);
        }

        private RaycastHit2D VerticalCollisionRaycast(int deltaMoveYDirectionAxis, LayerMask layer)
        {
            return Raycast(Origin, up * deltaMoveYDirectionAxis, Length, layer);
        }

        private void SetCollisionOnVerticalCollisionRaycastHit(int deltaMoveYDirectionAxis)
        {
            SetCollisionAbove(deltaMoveYDirectionAxis > 0);
            SetCollisionBelow(deltaMoveYDirectionAxis < 0);
            SetCollisionVerticalHit(Hit);
        }

        private void SetCollisionAbove(bool colliding)
        {
            collision.Above = colliding;
        }

        private void ClimbSteepSlope(int deltaMoveXDirectionAxis, float deltaMoveDistanceX, float deltaMoveY,
            LayerMask layer)
        {
            SetRaycastLength(ClimbSteepSlopeLength(deltaMoveDistanceX));
            SetRaycastOrigin(ClimbSteepSlopeOrigin(deltaMoveXDirectionAxis, deltaMoveY));
            SetRaycastHit(ClimbSteepSlopeHit(deltaMoveXDirectionAxis, layer));
        }

        private float ClimbSteepSlopeLength(float deltaMoveDistanceX)
        {
            return deltaMoveDistanceX + SkinWidth * 2;
        }

        private Vector2 ClimbSteepSlopeOrigin(int deltaMoveXDirectionAxis, float deltaMoveY)
        {
            return (deltaMoveXDirectionAxis == -1 ? bounds.BottomLeft : bounds.BottomRight) + up * deltaMoveY;
        }

        private RaycastHit2D ClimbSteepSlopeHit(int deltaMoveXDirectionAxis, LayerMask layer)
        {
            return Raycast(Origin, right * deltaMoveXDirectionAxis, Length, layer);
        }

        private void SetGroundCollision(float angle, int direction)
        {
            SetCollisionGroundAngle(angle);
            SetCollisionGroundDirectionAxis(direction);
        }

        private void ClimbSteepSlopeHit()
        {
            SetGroundCollision(HitAngle, RaycastHitDirection);
        }

        private void ClimbMildSlope(int deltaMoveXDirectionAxis, Vector2 deltaMove, LayerMask layer)
        {
            SetRaycastOrigin(ClimbMildSlopeOrigin(deltaMoveXDirectionAxis, deltaMove));
            SetRaycastHit(ClimbMildSlopeHit(layer));
            CastRay(Origin, down, yellow);
        }

        private Vector2 ClimbMildSlopeOrigin(int deltaMoveXDirectionAxis, Vector2 deltaMove)
        {
            return (deltaMoveXDirectionAxis == -1 ? bounds.BottomLeft : bounds.BottomRight) + deltaMove;
        }

        private RaycastHit2D ClimbMildSlopeHit(LayerMask layer)
        {
            return Raycast(Origin, down, 1f, layer);
        }

        private void DescendMildSlope(int deltaMoveXDirectionAxis, float deltaMoveDistanceY, float deltaMoveX,
            LayerMask layer)
        {
            SetRaycastLength(DescendMildSlopeLength(deltaMoveDistanceY));
            SetRaycastOrigin(DescendMildSlopeOrigin(deltaMoveXDirectionAxis, deltaMoveX));
            SetRaycastHit(DescendMildSlopeHit(layer));
        }

        private float DescendMildSlopeLength(float deltaMoveDistanceY)
        {
            return deltaMoveDistanceY + SkinWidth;
        }

        private Vector2 DescendMildSlopeOrigin(int deltaMoveXDirectionAxis, float deltaMoveX)
        {
            return (deltaMoveXDirectionAxis == -1 ? bounds.BottomRight : bounds.BottomLeft) + right * deltaMoveX;
        }

        private RaycastHit2D DescendMildSlopeHit(LayerMask layer)
        {
            return Raycast(Origin, down, Length, layer);
        }

        private void DescendMildSlopeHit()
        {
            SetGroundCollision(HitAngle, (int) Sign(Hit.normal.x));
        }
        private void DescendSteepSlope(int deltaMoveXDirectionAxis, Vector2 deltaMove, LayerMask layer)
        {
            SetRaycastOrigin(DescendSteepSlopeOrigin(deltaMoveXDirectionAxis, deltaMove));
            SetRaycastHit(DescendSteepSlopeHit(layer));
            CastRay(Origin, down, yellow);
        }

        private Vector2 DescendSteepSlopeOrigin(int deltaMoveXDirectionAxis, Vector2 deltaMove)
        {
            return (deltaMoveXDirectionAxis == 1 ? bounds.BottomLeft : bounds.BottomRight) + deltaMove;
        }

        private RaycastHit2D DescendSteepSlopeHit(LayerMask layer)
        {
            return Raycast(Origin, down, 1f, layer);
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

        public void OnSetHorizontalCollisionRaycast(int index, int deltaMoveXDirectionAxis, float deltaMoveDistanceX,
            LayerMask layer)
        {
            SetHorizontalCollisionRaycast(index, deltaMoveXDirectionAxis, deltaMoveDistanceX, layer);
        }

        public void OnHorizontalCollisionRaycastHitClimbingSlope()
        {
            SetCollisionOnRaycastHitGround(true);
        }

        public void OnHorizontalCollisionRaycastHitClimbingSlope(float deltaMoveDistanceX)
        {
            SetRaycastLength(HorizontalCollisionHitClimbingSlopeLength(deltaMoveDistanceX));
        }

        public void OnHorizontalCollisionRaycastHitMaximumSlope(int deltaMoveXDirectionAxis)
        {
            SetCollisionOnHorizontalCollisionHitMaximumSlope(deltaMoveXDirectionAxis);
        }

        public void OnStopHorizontalSpeed(LayerMask layer)
        {
            StopHorizontalSpeed(layer);
        }

        public void OnSetVerticalCollisionRaycast(int index, int deltaMoveYDirectionAxis, float deltaMoveDistanceY,
            float deltaMoveX, LayerMask layer)
        {
            SetVerticalCollisionRaycast(index, deltaMoveYDirectionAxis, deltaMoveDistanceY, deltaMoveX, layer);
        }

        public void OnSetVerticalCollisionRaycastHit(int deltaMoveYDirectionAxis, LayerMask layer)
        {
            SetRaycastHit(VerticalCollisionRaycast(deltaMoveYDirectionAxis, layer));
        }

        public void OnSetVerticalCollisionRaycastLengthOnHit()
        {
            SetRaycastLength(Hit.distance);
        }

        public void OnVerticalCollisionRaycastHit(int deltaMoveYDirectionAxis)
        {
            SetCollisionOnVerticalCollisionRaycastHit(deltaMoveYDirectionAxis);
        }

        public void OnClimbSteepSlope(int deltaMoveXDirectionAxis, float deltaMoveDistanceX, float deltaMoveY,
            LayerMask layer)
        {
            ClimbSteepSlope(deltaMoveXDirectionAxis, deltaMoveDistanceX, deltaMoveY, layer);
        }

        public void OnClimbSteepSlopeHit()
        {
            ClimbSteepSlopeHit();
        }

        public void OnClimbMildSlope(int deltaMoveXDirectionAxis, Vector2 deltaMove, LayerMask layer)
        {
            ClimbMildSlope(deltaMoveXDirectionAxis, deltaMove, layer);
        }

        public void OnDescendMildSlope(int deltaMoveXDirectionAxis, float deltaMoveDistanceY, float deltaMoveX,
            LayerMask layer)
        {
            DescendMildSlope(deltaMoveXDirectionAxis, deltaMoveDistanceY, deltaMoveX, layer);
        }

        public void OnDescendMildSlopeHit()
        {
            DescendMildSlopeHit();
        }

        public void OnDescendSteepSlope(int deltaMoveXDirectionAxis, Vector2 deltaMove, LayerMask layer)
        {
            DescendSteepSlope(deltaMoveXDirectionAxis, deltaMove, layer);
        }

        #endregion
    }
}
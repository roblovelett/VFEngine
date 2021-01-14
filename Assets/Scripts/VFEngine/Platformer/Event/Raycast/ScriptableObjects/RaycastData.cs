using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.ScriptableObjects
{
    using static ScriptableObjectExtensions;
    using static Mathf;
    using static Vector2;
    using static Physics2D;
    using static RaycastData.RaycastState;
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
        public RaycastHit2D Hit { get; private set; }
        public Vector2 BoundsBottomLeft => bounds.BottomLeft;
        public Vector2 BoundsBottomRight => bounds.BottomRight;
        public Vector2 Origin { get; private set; }
        public bool OnGround => collision.OnGround;
        public bool OnSlope => collision.OnSlope;
        public float GroundAngle => collision.GroundAngle;
        public int GroundDirectionAxis => collision.GroundDirection;
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
        public RaycastState State { get; private set; } = None;

        public enum RaycastState
        {
            None,
            Initialized,
            PlatformerInitializedFrame,
            PlatformerGroundCollisionRaycastHit
        }

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
            public int GroundDirection { get; set; }
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
            SetRaycastState(Initialized);
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

        private void SetRaycastState(RaycastState state)
        {
            State = state;
        }

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
            collision.Below = false;
            collision.Left = false;
            collision.OnGround = false;
            collision.GroundDirection = 0;
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
            SetRaycastState(PlatformerInitializedFrame);
        }

        private void SetGroundCollisionRaycast(int deltaMoveXDirectionAxis, int index, LayerMask layer)
        {
            SetGroundCollisionRaycastOrigin(deltaMoveXDirectionAxis, index);
            SetGroundCollisionRaycastHit(layer);
        }

        private float GroundCollisionRaycastOriginY => SkinWidth * 2;

        private void SetGroundCollisionRaycastOrigin(int deltaMoveXDirectionAxis, int index)
        {
            Origin = GroundCollisionRaycastOrigin(deltaMoveXDirectionAxis, index);
            ApplyToRaycastOriginY(GroundCollisionRaycastOriginY);
        }

        private Vector2 GroundCollisionRaycastOrigin(int deltaMoveXDirectionAxis, int index)
        {
            var positiveXDirection = deltaMoveXDirectionAxis == 1;
            var initialOrigin = positiveXDirection ? bounds.BottomLeft : bounds.BottomRight;
            var xDirection = positiveXDirection ? right : left;
            return initialOrigin + xDirection * VerticalSpacing * index;
        }

        private void ApplyToRaycastOriginY(float y)
        {
            origin = Origin;
            origin.y += y;
            Origin = origin;
        }

        private void SetGroundCollisionRaycastHit(LayerMask layer)
        {
            Hit = GroundCollisionRaycastHit(layer);
        }

        private RaycastHit2D GroundCollisionRaycastHit(LayerMask layer)
        {
            return Raycast(Origin, down, SkinWidth * 4f, layer);
        }

        private Vector2 GroundCollisionRaycastDirection => down * SkinWidth * 2;

        private void GroundCollisionRaycastHit()
        {
            SetCollisionOnGroundCollisionRaycastHit();
            CastRay(Origin, GroundCollisionRaycastDirection, blue);
            SetRaycastState(PlatformerGroundCollisionRaycastHit);
        }

        private float RaycastHitAngle => Angle(Hit.normal, up);
        private int RaycastHitDirection => (int) Sign(Hit.normal.x);
        private LayerMask RaycastHitLayer => Hit.collider.gameObject.layer;

        private void SetCollisionOnGroundCollisionRaycastHit()
        {
            collision.OnGround = true;
            collision.GroundAngle = RaycastHitAngle;
            collision.GroundDirection = RaycastHitDirection;
            collision.GroundLayer = RaycastHitLayer;
            collision.VerticalHit = Hit;
            collision.Below = true;
        }

        private static void CastRay(Vector2 start, Vector2 direction, Color color)
        {
            DrawRay(start, direction, color);
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
            SetGroundCollisionRaycastHit(layer);
        }

        public void OnGroundCollisionRaycastHit()
        {
            GroundCollisionRaycastHit();
        }

        #endregion
    }
}
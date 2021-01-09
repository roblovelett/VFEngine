using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;
    using static Mathf;
    using static RaycastData.RaycastHitType;
    using static Vector2;

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
        public bool OnGround => collision.OnGround;
        public bool OnSlope => collision.OnSlope;
        public float GroundAngle => collision.GroundAngle;
        public int GroundDirection => collision.GroundDirection;
        public int HorizontalRays { get; private set; }
        public float HorizontalSpacing { get; private set; }

        public enum RaycastHitType
        {
            Ground,
            HorizontalSlope,
            HorizontalWall,
            Vertical,
            ClimbMildSlope,
            ClimbSteepSlope,
            DescendMildSlope,
            DescendSteepSlope,
            None
        }

        #endregion

        #region fields

        private bool displayWarnings;
        private bool drawGizmos;
        private int totalHorizontalRays;
        private int totalVerticalRays;
        private float length;
        private float spacing;
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

        private void InitializeInternal(Collider2D boxCollider, RaycastSettings settings)
        {
            ApplySettings(settings);
            InitializeDefault();
            InitializeBounds(boxCollider);
            InitializeCollision();
            InitializeCount();
            InitializeSpacing();
        }

        private void ApplySettings(RaycastSettings settings)
        {
            displayWarnings = settings.displayWarnings;
            drawGizmos = settings.drawGizmos;
            totalHorizontalRays = settings.totalHorizontalRays;
            totalVerticalRays = settings.totalVerticalRays;
            spacing = settings.spacing;
            SkinWidth = settings.skinWidth;
        }

        private void InitializeDefault()
        {
            Index = 0;
            length = 0;
            origin = new Vector2();
            Hit = new RaycastHit2D();
        }

        private void InitializeBounds(Collider2D boxCollider)
        {
            bounds._ = boxCollider.bounds;
            bounds._.Expand(SkinWidth * -2);
            bounds.Size = bounds._.size;
            bounds.BottomLeft = new Vector2(bounds._.min.x, bounds._.min.y);
            bounds.BottomRight = new Vector2(bounds._.max.x, bounds._.min.y);
            bounds.TopLeft = new Vector2(bounds._.min.x, bounds._.max.y);
            bounds.TopRight = new Vector2(bounds._.max.x, bounds._.max.y);
        }

        private void InitializeCollision()
        {
            ResetCollisionInternal();
            collision.GroundLayer = 0;
            collision.OnSlope = collision.OnGround && collision.GroundAngle != 0;
        }

        private void ResetCollisionInternal()
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

        private void InitializeCount()
        {
            HorizontalRays = (int) Round(bounds.Size.y / spacing);
            VerticalRays = (int) Round(bounds.Size.x / spacing);
        }

        private void InitializeSpacing()
        {
            HorizontalSpacing = bounds.Size.y / (HorizontalRays - 1);
            VerticalSpacing = bounds.Size.x / (VerticalRays - 1);
        }

        #endregion

        #region public methods

        public void Initialize(BoxCollider2D boxCollider, RaycastSettings settings)
        {
            InitializeInternal(boxCollider, settings);
        }

        public void ResetCollision()
        {
            ResetCollisionInternal();
        }

        public void SetBounds(BoxCollider2D boxCollider)
        {
            InitializeBounds(boxCollider);
        }

        public void SetIndex(int index)
        {
            Index = index;
        }

        public void SetOrigin(Vector2 rayOrigin)
        {
            origin = rayOrigin;
        }

        public void SetHit(RaycastHit2D hit)
        {
            Hit = hit;
        }

        public void SetCollision(RaycastHitType type, RaycastHit2D hit)
        {
            var normal = hit.normal;
            var angle = Angle(normal, up);
            var direction = (int) Sign(normal.x);
            var layer = hit.collider.gameObject.layer;
            switch (type)
            {
                case Ground:
                    SetGroundCollision(true, angle, direction);
                    collision.GroundLayer = layer;
                    collision.VerticalHit = hit;
                    collision.Below = true;
                    break;
                case HorizontalSlope:
                    SetGroundCollision(true, angle, direction);
                    break;
            }
        }

        public void SetCollision(RaycastHitType type, RaycastHit2D hit, float deltaMoveXDirectionAxis)
        {
            var left = deltaMoveXDirectionAxis < 0;
            var right = deltaMoveXDirectionAxis > 0;
            switch (type)
            {
                case HorizontalWall:
                    collision.Left = left;
                    collision.Right = right;
                    collision.HorizontalHit = hit;
                    break;
            }
        }

        public void SetCollisionBelow(bool collisionBelow)
        {
            collision.Below = collisionBelow;
        }

        public void SetLength(float raycastLength)
        {
            length = raycastLength;
        }

        #endregion

        #region private methods

        private void SetGroundCollision(bool on, float angle, int direction)
        {
            collision.OnGround = on;
            collision.GroundAngle = angle;
            collision.GroundDirection = direction;
        }

        #endregion

        #region event handlers

        #endregion
    }
}
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
            SetBounds(boxCollider);
            InitializeCollision();
            InitializeCount();
            InitializeSpacing();
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

        private void InitializeCollision()
        {
            ResetCollision();
            collision.GroundLayer = 0;
            collision.OnSlope = collision.OnGround && collision.GroundAngle != 0;
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

        public void OnInitializeFrame(BoxCollider2D boxCollider)
        {
            ResetCollision();
            SetBounds(boxCollider);
        }

        private bool SetHitForOneWayPlatform => !Hit && IgnorePlatformsTime <= 0;
        private bool CastNextRay => Hit.distance <= 0;
        public void OnGroundCollision(int deltaMoveXDirectionAxis, LayerMask collisionLayer, LayerMask oneWayPlatformLayer)
        {
            for (var i = 0; i < VerticalRays; i++)
            {
                SetGroundCollisionRaycast(deltaMoveXDirectionAxis, collisionLayer);
                if (SetHitForOneWayPlatform)
                {
                    SetGroundCollisionRaycast(oneWayPlatformLayer);
                    if (CastNextRay) continue;
                }
                if (!Hit) continue;
                SetCollisionOnGround();
                SetLengthOnGroundCollision();
                CastRay(Origin, down * Length, blue);
                break;
            }
        }

        private void SetGroundCollisionRaycast(int deltaMoveXDirectionAxis, LayerMask collisionLayer)
        {
            Origin = GroundCollisionOrigin(deltaMoveXDirectionAxis);
            SetGroundCollisionRaycast(collisionLayer);
        }

        private void SetGroundCollisionRaycast(LayerMask layerMask)
        {
            Hit = GroundCollisionHit(layerMask);
        }
        
        private static void CastRay(Vector2 start, Vector2 direction, Color color)
        {
            DrawRay(start, direction, color);
        }

        public void OnSlopeBehaviorCollision()
        {
            collision.Below = true;
        }

        public void OnInitializeHorizontalCollisionRaycast(float deltaMoveX)
        {
            Length = HorizontalCollisionLength(deltaMoveX);
        }

        private float HorizontalCollisionLength(float deltaMoveX)
        {
            return Abs(deltaMoveX) + SkinWidth;
        }

        public void OnHorizontalCollision(int index, int deltaMoveXDirectionAxis, LayerMask collisionLayer)
        {
            SetHorizontalCollisionRaycast(index, deltaMoveXDirectionAxis, collisionLayer);
        }

        private void SetHorizontalCollisionRaycast(int index, int deltaMoveXDirectionAxis, LayerMask collisionLayer)
        {
            Index = index;
            Origin = HorizontalCollisionOrigin(deltaMoveXDirectionAxis);
            Hit = HorizontalCollisionHit(deltaMoveXDirectionAxis, collisionLayer);
            CastRay(Origin, right * deltaMoveXDirectionAxis * Length, red);
        }

        private Vector2 HorizontalCollisionOrigin(int deltaMoveXDirectionAxis)
        {
            return (deltaMoveXDirectionAxis == -1 ? bounds.BottomLeft : bounds.BottomRight) +
                   up * (HorizontalSpacing * Index);
        }

        private RaycastHit2D HorizontalCollisionHit(int deltaMoveXDirectionAxis, LayerMask collisionLayer)
        {
            return Raycast(Origin, right * deltaMoveXDirectionAxis, Length, collisionLayer);
        }

        public void OnHorizontalCollisionHitClimbingSlope()
        {
            SetCollisionOnGround(true);
        }

        private float HitAngle => Angle(Hit.normal, up);
        
        /*private void SetCollisionOnGround()
        {
            collision.OnGround = true;
            collision.GroundAngle = HitAngle;
            collision.GroundDirection = HitDirection;
            collision.GroundLayer = HitLayer;
            collision.VerticalHit = Hit;
            collision.Below = true;
        }*/

        #endregion

        #region private methods

        private void ResetCollision()
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

        private void SetBounds(Collider2D boxCollider)
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

        private Vector2 GroundCollisionOrigin(int deltaMoveXDirectionAxis)
        {
            var origin = (deltaMoveXDirectionAxis == 1 ? bounds.BottomLeft : bounds.BottomRight) +
                         (deltaMoveXDirectionAxis == 1 ? right : left) * VerticalSpacing * Index;
            origin.y += SkinWidth * 2;
            return origin;
        }

        private RaycastHit2D GroundCollisionHit(LayerMask layerMask)
        {
            return Raycast(Origin, down, SkinWidth * 4, layerMask);
        }

        private int HitDirection => (int) Sign(Hit.normal.x);
        private LayerMask HitLayer => Hit.collider.gameObject.layer;
        private void SetCollisionOnGround()
        {
            SetCollisionOnGround(true);
            collision.GroundLayer = HitLayer;
            collision.VerticalHit = Hit;
            collision.Below = true;
        }
        
        private void SetCollisionOnGround(bool onGround)
        {
            collision.OnGround = onGround;
            collision.GroundAngle = HitAngle;
            collision.GroundDirection = HitDirection;
        }

        private void SetLengthOnGroundCollision()
        {
            Length = SkinWidth * 2;
        }

        #endregion

        #region event handlers

        #endregion
    }
}
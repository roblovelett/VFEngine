namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    public class RaycastHitColliderData
    {
        
    }
}

/*
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Platformer.Physics.Movement.PathMovement;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.Manager
{
    using static DebugExtensions;

    public class RaycastHitCollidersManagerData : MonoBehaviour
    {
        /* fields: dependencies */
/*  
[SerializeField] private RaycastHitCollidersManagerSettings settings;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private IntReference numberOfVerticalRays;

        /* fields */
  /*      [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector3Reference boxColliderBoundsCenter;
        [SerializeField] private FloatReference movingPlatformCurrentGravity;
        [SerializeField] private BoolReference isCollidingWithMovingPlatform;
        [SerializeField] private Vector3Reference movingPlatformCurrentSpeed;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;
        private bool DisplayWarnings => settings.displayWarningsControl;

        /* fields: methods */
    /*    private void GetWarningMessage()
        {
            if (!DisplayWarnings) return;
            var warningMessage = "";
            var warningMessageCount = 0;
            if (!settings)
            {
                warningMessage += "Settings field not set to Raycast Hit Collider Settings ScriptableObject.@";
                warningMessageCount++;
            }

            if (!boxCollider)
            {
                warningMessage += "Box Collider field not set to parent GameObject's Box Collider 2D Component.@";
                warningMessageCount++;
            }

            if (BoxColliderOffset.x != 0)
            {
                warningMessage = "Box Collider Offset not set to zero. May cause issues moving direction near walls.@";
                warningMessageCount++;
            }

            DebugLogWarning(warningMessageCount, warningMessage);
        }

        /* properties: dependencies */
      /*  public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
        public int NumberOfVerticalRays => numberOfVerticalRays.Value;
        public BoxCollider2D BoxCollider => boxCollider;

        /* properties */
       /* public const float Tolerance = 0;
        public const float SmallValue = 0.0001f;
        public float movingPlatformGravity = -500;
        public const float ObstacleHeightTolerance = 0.05f;
        public bool HasBoxCollider => boxCollider;
        public bool DrawColliderGizmos => settings.drawColliderGizmosControl;
        public ModelState State { get; } = new ModelState();
        public List<RaycastHit2D> ContactList { get; set; } = new List<RaycastHit2D>();
        public Vector2 OriginalColliderSize { get; set; }
        public Vector2 OriginalColliderOffset { get; set; }
        public Vector3 ColliderBottomCenterPosition { get; set; }
        public Vector3 ColliderLeftCenterPosition { get; set; }
        public Vector3 ColliderRightCenterPosition { get; set; }
        public Vector3 ColliderTopCenterPosition { get; set; }
        public Vector3 CrossBelowSlopeAngle { get; set; }
        public GameObject StandingOn { get; set; }
        public GameObject StandingOnLastFrame { get; set; }
        public Collider2D StandingOnCollider { get; set; }
        public GameObject CurrentWallColliderGameObject { get; set; }
        public int SavedBelowLayer { get; set; }
        public Collider2D IgnoredCollider { get; set; }
        public float BelowSlopeAngle { get; set; }
        public int SmallestDistanceIndex { get; set; }
        public float DistanceToWall { get; set; }
        public float Friction { get; set; }
        public PathMovementController MovingPlatform { get; set; }

        public float MovingPlatformCurrentGravity
        {
            get => movingPlatformCurrentGravity.Value;
            set => value = movingPlatformCurrentGravity.Value;
        }

        public Vector3 MovingPlatformCurrentSpeed
        {
            get => movingPlatformCurrentSpeed.Value;
            set => value = movingPlatformCurrentSpeed.Value;
        }

        public RaycastHit2D[] RightHitsStorage { get; set; }
        public RaycastHit2D[] LeftHitsStorage { get; set; }
        public RaycastHit2D[] BelowHitsStorage { get; set; }
        public RaycastHit2D[] AboveHitsStorage { get; set; }
        public RaycastHit2D RaycastHitAbove { get; set; }
        public RaycastHit2D RaycastHitBelow { get; set; }
        public RaycastHit2D RaycastHitLeft { get; set; }
        public RaycastHit2D RaycastHitRight { get; set; }
        public Vector3 ColliderSize => Vector3.Scale(transform.localScale, boxCollider.size);
        public Vector3 ColliderCenterPosition => boxCollider.bounds.center;
        public Vector2 ColliderOffset => boxCollider.offset;

        public Vector2 BoxColliderOffset
        {
            get => boxColliderOffset.Value;
            set => value = boxColliderOffset.Value;
        }

        public Vector2 BoxColliderSize
        {
            get => boxColliderSize.Value;
            set => value = boxColliderSize.Value;
        }

        public Vector3 ColliderBottomPosition
        {
            get
            {
                var bounds = boxCollider.bounds;
                return new Vector3 {x = bounds.center.x, y = bounds.min.y, z = 0};
            }
        }

        public Vector3 ColliderLeftPosition
        {
            get
            {
                var bounds = boxCollider.bounds;
                return new Vector3 {x = bounds.min.x, y = bounds.center.y, z = 0};
            }
        }

        public Vector3 ColliderTopPosition
        {
            get
            {
                var bounds = boxCollider.bounds;
                return new Vector3 {x = bounds.center.x, y = bounds.max.y, z = 0};
            }
        }

        public Vector3 ColliderRightPosition
        {
            get
            {
                var bounds = boxCollider.bounds;
                return new Vector3 {x = bounds.max.x, y = bounds.center.y, z = 0};
            }
        }

        /* properties: methods */
       /* public void Initialize()
        {
            boxColliderOffset.Value = boxCollider.offset;
            boxColliderSize.Value = boxCollider.size;
            boxColliderBoundsCenter.Value = boxCollider.bounds.center;
            isCollidingWithMovingPlatform.Value = State.IsCollidingWithMovingPlatform;
            wasTouchingCeilingLastFrame.Value = State.WasTouchingCeilingLastFrame;
            GetWarningMessage();
        }

        public class ModelState
        {
            public bool IsCollidingWithMovingPlatform { get; private set; }
            public bool IsCollidingWithStairs { get; private set; }
            public bool IsCollidingWithFrictionSurface { get; private set; }
            public bool IsCollidingRight { get; private set; }
            public bool IsCollidingLeft { get; private set; }
            public bool IsCollidingAbove { get; private set; }
            public bool IsCollidingBelow { get; private set; }
            public bool IsCollidingWithLevelBounds { get; private set; }
            public bool OnMovingPlatform { get; private set; }
            public bool IsPassingSlopeAngle { get; private set; }
            public bool WasGroundedLastFrame { get; private set; }
            public bool WasTouchingCeilingLastFrame { get; private set; }
            public bool ColliderResized { get; private set; }
            public bool GroundedEvent { get; private set; }
            public float LateralSlopeAngle { get; private set; }
            public float BelowSlopeAngle { get; private set; }
            public float DistanceToLeftRaycastHit { get; private set; }
            public float DistanceToRightRaycastHit { get; private set; }
            public bool IsGrounded => IsCollidingBelow;
            public bool HasCollisions => IsCollidingRight || IsCollidingLeft || IsCollidingAbove || IsCollidingBelow;

            public void SetIsCollidingWithMovingPlatform(bool isCollidingWithMovingPlatform)
            {
                IsCollidingWithMovingPlatform = isCollidingWithMovingPlatform;
            }

            public void SetIsCollidingWithStairs(bool isCollidingWithStairs)
            {
                IsCollidingWithStairs = isCollidingWithStairs;
            }

            public void SetIsCollidingWithFrictionSurface(bool isCollidingWithFrictionSurface)
            {
                IsCollidingWithFrictionSurface = isCollidingWithFrictionSurface;
            }

            public void SetIsCollidingRight(bool isCollidingRight)
            {
                IsCollidingRight = isCollidingRight;
            }

            public void SetIsCollidingLeft(bool isCollidingLeft)
            {
                IsCollidingLeft = isCollidingLeft;
            }

            public void SetIsCollidingAbove(bool isCollidingAbove)
            {
                IsCollidingAbove = isCollidingAbove;
            }

            public void SetIsCollidingBelow(bool isCollidingBelow)
            {
                IsCollidingBelow = isCollidingBelow;
            }

            public void SetIsCollidingWithLevelBounds(bool isCollidingWithLevelBounds)
            {
                IsCollidingWithLevelBounds = isCollidingWithLevelBounds;
            }

            public void SetOnMovingPlatform(bool onMovingPlatform)
            {
                OnMovingPlatform = onMovingPlatform;
            }

            public void SetWasGroundedLastFrame(bool wasGroundedLastFrame)
            {
                WasGroundedLastFrame = wasGroundedLastFrame;
            }

            public void SetWasTouchingCeilingLastFrame(bool wasTouchingCeilingLastFrame)
            {
                WasTouchingCeilingLastFrame = wasTouchingCeilingLastFrame;
            }

            public void SetColliderResized(bool colliderResized)
            {
                ColliderResized = colliderResized;
            }

            public void SetLateralSlopeAngle(float lateralSlopeAngle)
            {
                LateralSlopeAngle = lateralSlopeAngle;
            }

            public void SetBelowSlopeAngle(float belowSlopeAngle)
            {
                BelowSlopeAngle = belowSlopeAngle;
            }

            public void SetDistanceToLeftRaycastHit(float distanceToLeftRaycastHit)
            {
                DistanceToLeftRaycastHit = distanceToLeftRaycastHit;
            }

            public void SetDistanceToRightRaycastHit(float distanceToRightRaycastHit)
            {
                DistanceToRightRaycastHit = distanceToRightRaycastHit;
            }
/*
            public void Reset()
            {
                IsCollidingLeft = false;
                IsCollidingRight = false;
                IsCollidingAbove = false;
                IsPassingSlopeAngle = false;
                GroundedEvent = false;
                DistanceToLeftRaycastHit = -1;
                DistanceToRightRaycastHit = -1;
                LateralSlopeAngle = 0;
            }
        }
    }
}*/
using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Physics
{
    using static Vector2;
    using static Mathf;
    using static Time;

    public struct PhysicsData
    {
        #region fields

        #region private methods

        private void InitializeDependencies(PhysicsSettings settings, GameObject character)
        {
            InitializeSettings(settings);
            InitializeTransform(character);
        }

        private void InitializeSettings(PhysicsSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            MaximumSlopeAngle = settings.maximumSlopeAngle;
            MinimumWallAngle = settings.minimumWallAngle;
            MinimumMovementThreshold = settings.minimumMovementThreshold;
            Gravity = settings.gravity;
            AirFriction = settings.airFriction;
            GroundFriction = settings.groundFriction;
            StaggerSpeedFalloff = settings.staggerSpeedFalloff;
        }

        private void InitializeTransform(GameObject character)
        {
            Transform = character.transform;
        }

        private void Initialize()
        {
            InitializeInternal();
        }

        private void InitializeInternal()
        {
            FacingRight = false;
            IgnoreFriction = false;
            GravityScale = 1;
            Speed = zero;
            ExternalForce = zero;
            ExternalForce = zero;
            TotalSpeed = Speed * ExternalForce;
            Movement = TotalSpeed * fixedDeltaTime;
            SetMovementDirection();
        }

        private void SetMovementDirection()
        {
            SetHorizontalMovementDirection();
            SetVerticalMovementDirection();
        }

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public float MinimumWallAngle { get; private set; }
        public float MinimumMovementThreshold { get; private set; }
        public float Gravity { get; private set; }
        public float AirFriction { get; private set; }
        public float GroundFriction { get; private set; }
        public float StaggerSpeedFalloff { get; private set; }
        public Transform Transform { get; private set; }

        #endregion

        public bool FacingRight { get; set; }
        public bool IgnoreFriction { get; set; }
        public int HorizontalMovementDirection { get; set; }
        public int VerticalMovementDirection { get; set; }
        public float GravityScale { get; set; }
        public Vector2 Speed { get; set; }

        public float HorizontalSpeed
        {
            get => Speed.x;
            set => value = Speed.x;
        }
        public float VerticalSpeed
        {
            get => Speed.y;
            set => value = Speed.y;
        }

        public Vector2 ExternalForce { get; set; }

        public float HorizontalExternalForce
        {
            get => ExternalForce.x;
            set => value = ExternalForce.x;
        }
        public float VerticalExternalForce
        {
            get => ExternalForce.y;
            set => value = ExternalForce.y;
        }

        public Vector2 TotalSpeed { get; set; }
        public Vector2 Movement { get; set; }

        public float HorizontalMovement
        {
            get => Movement.x;
            set => value = Movement.x;
        }

        public float VerticalMovement
        {
            get => Movement.y;
            set => value = Movement.y;
        }

        #region public methods

        #region constructors

        public PhysicsData(PhysicsSettings settings, GameObject character) : this()
        {
            InitializeDependencies(settings, character);
            Initialize();
        }

        #endregion

        public void SetHorizontalMovementDirection()
        {
            HorizontalMovementDirection = (int) Sign(HorizontalMovement);
        }

        public void SetVerticalMovementDirection()
        {
            VerticalMovementDirection = (int) Sign(VerticalMovement);
        }

        public void SetExternalForce(float friction)
        {
            var maxDistanceDelta = ExternalForce.magnitude * friction * deltaTime;
            ExternalForce = MoveTowards(ExternalForce, zero, maxDistanceDelta);
        }

        public void ApplyGravityToSpeed(float gravity)
        {
            VerticalSpeed += gravity;
        }

        public void ApplyGravityToExternalForce(float gravity)
        {
            VerticalExternalForce += gravity;
        }

        public void ApplyForcesToHorizontalExternalForce(int groundDirection)
        {
            HorizontalExternalForce += -Gravity * GroundFriction * groundDirection * deltaTime / 4;
        }

        public void SetMovement(Vector2 movement)
        {
            Movement = movement;
        }

        public void SetVerticalSpeed(float speed)
        {
            VerticalSpeed = speed;
        }

        public void SetVerticalExternalForce(float force)
        {
            VerticalExternalForce = force;
        }

        public void OnPreClimbSlopeBehavior(float distance)
        {
            HorizontalMovement -= distance * HorizontalMovementDirection;
        }

        public void OnPostClimbSlopeBehavior(float distance)
        {
            HorizontalMovement += distance * HorizontalMovementDirection;
        }

        public void SetHorizontalMovement(float movement)
        {
            HorizontalMovement = movement;
        }

        public void SetVerticalMovement(float movement)
        {
            VerticalMovement = movement;
        }

        public void OnHitWall()
        {
            SetHorizontalSpeed(0);
            SetHorizontalExternalForce(0);
        }

        public void SetHorizontalSpeed(float speed)
        {
            HorizontalSpeed = speed;
        }

        public void SetHorizontalExternalForce(float force)
        {
            HorizontalExternalForce = force;
        }

        public void OnApplyGroundAngle(float angleRad)
        {
            var horizontalMovement = VerticalMovement / Tan(angleRad) * Sign(HorizontalMovement);
            SetHorizontalMovement(horizontalMovement);
            SetHorizontalSpeed(0);
            SetHorizontalExternalForce(0);
        }

        #endregion

        #endregion
    }
}
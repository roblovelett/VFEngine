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

        private void InitializeDependencies(PhysicsSettings settings)
        {
            InitializeSettings(settings);
            InitializeTransform();
        }

        private void InitializeDependencies(GameObject character)
        {
            InitializeSettings();
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

        private void InitializeSettings()
        {
            DisplayWarningsControl = false;
            MaximumSlopeAngle = 0;
            MinimumWallAngle = 0;
            MinimumMovementThreshold = 0;
            Gravity = 0;
            AirFriction = 0;
            GroundFriction = 0;
            StaggerSpeedFalloff = 0;
        }

        private void InitializeTransform(GameObject character)
        {
            Transform = character.transform;
        }

        private void InitializeTransform()
        {
            var character = new GameObject();
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
            DeltaMovement = TotalSpeed * fixedDeltaTime;
            HorizontalMovementDirection = (int) Sign(DeltaMovement.x);
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
        public float GravityScale { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 ExternalForce { get; set; }
        public Vector2 TotalSpeed { get; set; }
        public Vector2 DeltaMovement { get; set; }

        #region public methods

        #region constructors

        public PhysicsData(PhysicsSettings settings, GameObject character) : this()
        {
            InitializeDependencies(settings, character);
            Initialize();
        }

        public PhysicsData(PhysicsSettings settings) : this()
        {
            InitializeDependencies(settings);
            Initialize();
        }

        public PhysicsData(GameObject character) : this()
        {
            InitializeDependencies(character);
            Initialize();
        }

        #endregion

        #endregion

        #endregion
    }
}

/*public float SpeedY
        {
            get => Speed.y;
            set => value = Speed.y;
        }

        public float ExternalForceX
        {
            get => ExternalForce.x;
            set => value = ExternalForce.x;
        }

        public float ExternalForceY
        {
            get => ExternalForce.y;
            set => value = ExternalForce.y;
        }
        
        public float DeltaMovementX
        {
            get => DeltaMovement.x;
            set => value = DeltaMovement.x;
        }

        public float DeltaMovementY
        {
            get => DeltaMovement.y;
            set => value = DeltaMovement.y;
        }*/
/*public void InitializeData()
{
    FacingRight = false;
    IgnoreFriction = false;
    GravityScale = 1;
    Speed = zero;
    ExternalForce = zero;
    ExternalForce = zero;
    TotalSpeed = Speed * ExternalForce;
    DeltaMovement = TotalSpeed * fixedDeltaTime;
    HorizontalMovementDirection = (int) Sign(DeltaMovement.x);
}*/
/*public void Initialize(PhysicsSettings settings)
{
    ApplySettings(settings);
}*/
/*public void OnDescendSlope(float groundAngle, float distance)
{
    DeltaMovementX = Cos(groundAngle * Deg2Rad) * distance * Sign(DeltaMovement.x);
    DeltaMovementY = -Sin(groundAngle * Deg2Rad) * distance;
    StopVerticalForce();
}

public void OnClimbSlope(float verticalMovement, float groundAngle, float distance)
{
    DeltaMovementX = Cos(groundAngle * Deg2Rad) * distance * Sign(DeltaMovement.x);
    DeltaMovementY = verticalMovement;
    StopVerticalForce();
}*/
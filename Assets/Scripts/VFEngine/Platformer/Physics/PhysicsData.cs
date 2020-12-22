// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment

using UnityEngine;
using VFEngine.Platformer.Physics.Movement;

namespace VFEngine.Platformer.Physics
{
    using static Vector2;
    using static Mathf;
    using static Time;

    public class PhysicsData
    {
        #region fields
        
        #region private methods
        
        private void ApplySettings(PhysicsSettings settings)
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

        #endregion
        
        public bool FacingRight { get; set; }
        public bool IgnoreFriction { get; set; }
        public int HorizontalMovementDirection { get; set; }
        public float GravityScale { get; set; }
        public float SpeedY
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
        public Vector2 Speed { get; set; }
        public Vector2 ExternalForce { get; set; }
        public Vector2 TotalSpeed { get; set; }
        public Vector2 DeltaMove { get; set; }
        
        #region public methods
        
        public void InitializeData()
        {
            FacingRight = false;
            IgnoreFriction = false;
            GravityScale = 1;
            Speed = zero;
            ExternalForce = zero;
            ExternalForce = zero;
            TotalSpeed = Speed * ExternalForce;
            DeltaMove = TotalSpeed * fixedDeltaTime;
            HorizontalMovementDirection = (int) Sign(DeltaMove.x);
        }

        public void Initialize(PhysicsSettings settings)
        {
            ApplySettings(settings);
        }

        #endregion

        #endregion
        
        
    }
}
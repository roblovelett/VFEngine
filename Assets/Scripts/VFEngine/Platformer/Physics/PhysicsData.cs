// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment

using UnityEngine;

namespace VFEngine.Platformer.Physics
{
    using static Vector2;
    using static Mathf;
    using static Time;

    public class PhysicsData
    {
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
        public float GravityScale { get; set; }
        public float HorizontalMovementDirection { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 ExternalForce { get; set; }
        public Vector2 TotalSpeed { get; set; }
        public Vector2 DeltaMove { get; set; }
        
        #region public methods

        public void ApplySettings(PhysicsSettings settings)
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

        public void Initialize()
        {
            FacingRight = false;
            IgnoreFriction = false;
            GravityScale = 1;
            Speed = zero;
            ExternalForce = zero;
            ExternalForce = zero;
            TotalSpeed = Speed * ExternalForce;
            DeltaMove = TotalSpeed * fixedDeltaTime;
        }

        public void ApplyDeltaMoveToHorizontalMovementDirection()
        {
            HorizontalMovementDirection = Sign(DeltaMove.x);
        }

        #endregion

        #endregion
    }
}
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static Vector2;
    using static Time;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "PhysicsData", menuName = PlatformerPhysicsDataPath, order = 0)]
    public class PhysicsData : ScriptableObject
    {
        #region events

        #endregion

        #region properties

        public int DeltaMoveDirectionAxis { get; private set; }
        public Vector2 DeltaMove { get; private set; }
        public bool IgnoreFriction { get; private set; }
        public float GroundFriction { get; private set; }
        public float AirFriction { get; private set; }
        public Vector2 ExternalForce { get; private set; }
        public float MinimumMoveThreshold { get; private set; }
        public float Gravity { get; private set; }
        public float GravityScale { get; private set; }
        public Vector2 Speed { get; private set; }
        public float MaximumSlopeAngle { get; private set; }
        public float MinimumWallAngle { get; private set; }
        public bool FacingRight { get; private set; }
        public Vector2 TotalSpeed { get; private set; }

        #endregion

        #region fields

        private float SpeedY
        {
            get => Speed.y;
            set => value = Speed.y;
        }

        private float SpeedX
        {
            set => value = Speed.x;
        }

        private float ExternalForceX
        {
            get => ExternalForce.x;
            set => value = ExternalForce.x;
        }

        private float ExternalForceY
        {
            get => ExternalForce.y;
            set => value = ExternalForce.y;
        }

        private float DeltaMoveX
        {
            get => DeltaMove.x;
            set => value = DeltaMove.x;
        }

        private float DeltaMoveY
        {
            set => value = DeltaMove.y;
        }

        private bool displayWarnings;
        private float staggerSpeedFalloff;

        #endregion

        #region initialization

        private void InitializeInternal(PhysicsSettings settings)
        {
            ApplySettings(settings);
            InitializeDefault();
        }

        private void ApplySettings(PhysicsSettings settings)
        {
            displayWarnings = settings.displayWarnings;
            MaximumSlopeAngle = settings.maximumSlopeAngle;
            MinimumWallAngle = settings.minimumWallAngle;
            MinimumMoveThreshold = settings.minimumMovementThreshold;
            Gravity = settings.gravity;
            AirFriction = settings.airFriction;
            GroundFriction = settings.groundFriction;
            staggerSpeedFalloff = settings.staggerSpeedFalloff;
        }

        private void InitializeDefault()
        {
            FacingRight = true;
            IgnoreFriction = false;
            GravityScale = 1;
            Speed = zero;
            ExternalForce = zero;
            TotalSpeed = Speed * ExternalForce;
            DeltaMove = TotalSpeed * fixedDeltaTime;
            DeltaMoveDirectionAxis = 0;
        }

        #endregion

        #region public methods

        public void Initialize(PhysicsSettings settings)
        {
            InitializeInternal(settings);
        }

        public void SetDeltaMoveDirectionAxis(int axis)
        {
            DeltaMoveDirectionAxis = axis;
        }

        public void SetExternalForce(Vector2 force)
        {
            ExternalForce = force;
        }

        public void ApplyToSpeedY(float force)
        {
            SpeedY += force;
        }

        public void ApplyToExternalForceY(float force)
        {
            ExternalForceY += force;
        }

        public void ApplyToExternalForceX(float force)
        {
            ExternalForceX += force;
        }

        public void SetDeltaMove(float x, float y)
        {
            DeltaMove = new Vector2(x, y);
        }

        public void SetSpeedY(float y)
        {
            SpeedY = y;
        }

        public void SetExternalForceY(float y)
        {
            ExternalForceY = y;
        }

        public void ApplyToDeltaMoveX(float force)
        {
            DeltaMoveX += force;
        }

        public void SetDeltaMoveX(float x)
        {
            DeltaMoveX = x;
        }

        public void SetDeltaMoveY(float y)
        {
            DeltaMoveY = y;
        }

        public void SetSpeedX(float x)
        {
            SpeedX = x;
        }

        public void SetExternalForceX(float x)
        {
            ExternalForceX = x;
        }

        public void ApplyToDeltaMove(Vector2 force)
        {
            DeltaMove += force;
        }

        public void SetIgnoreFriction(bool ignore)
        {
            IgnoreFriction = ignore;
        }

        #endregion

        #region private methods

        #endregion

        #region event handlers

        #endregion
    }
}
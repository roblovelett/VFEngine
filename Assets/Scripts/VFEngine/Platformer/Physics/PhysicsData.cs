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

        #endregion

        #region fields

        private bool displayWarnings;
        private bool facingRight;
        private bool ignoreFriction;
        private float maximumSlopeAngle;
        private float minimumWallAngle;
        private float minimumMovementThreshold;
        private float gravity;
        private float airFriction;
        private float groundFriction;
        private float staggerSpeedFalloff;
        private float gravityScale;
        private Vector2 speed;
        private Vector2 externalForce;
        private Vector2 totalSpeed;

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
            maximumSlopeAngle = settings.maximumSlopeAngle;
            minimumWallAngle = settings.minimumWallAngle;
            minimumMovementThreshold = settings.minimumMovementThreshold;
            gravity = settings.gravity;
            airFriction = settings.airFriction;
            groundFriction = settings.groundFriction;
            staggerSpeedFalloff = settings.staggerSpeedFalloff;
        }

        private void InitializeDefault()
        {
            facingRight = true;
            ignoreFriction = false;
            gravityScale = 1;
            speed = zero;
            externalForce = zero;
            totalSpeed = speed * externalForce;
            DeltaMove = totalSpeed * fixedDeltaTime;
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

        #endregion

        #region private methods

        #endregion

        #region event handlers

        #endregion
    }
}
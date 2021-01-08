/*using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics
{
    using static Vector2;
    using static Mathf;
    using static Time;
    using static ScriptableObjectExtensions;
    
    [CreateAssetMenu(fileName = "RaycastData", menuName = PlatformerPhysicsDataPath, order = 0)]
    [InlineEditor]
    public class PhysicsData : ScriptableObject
    {
        #region fields

        private GameObject character;

        #region private methods

        private void InitializeDependencies(ref GameObject characterObject, PhysicsSettings settings)
        {
            InitializeSettings(settings);
            InitializeTransform(ref characterObject);
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

        private void InitializeTransform(ref GameObject characterObject)
        {
            character = characterObject;
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
            DeltaMove = TotalSpeed * fixedDeltaTime;
            MovementDirection = 0;
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
        public int MovementDirection { get; set; }
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
        public Vector2 DeltaMove { get; set; }

        private float HorizontalDeltaMove
        {
            get => DeltaMove.x;
            set => value = DeltaMove.x;
        }

        private float VerticalDeltaMove
        {
            get => DeltaMove.y;
            set => value = DeltaMove.y;
        }

        #region public methods

        #region constructors

        *//*public PhysicsData(ref GameObject character, PhysicsSettings settings) : this()
        {
            InitializeDependencies(ref character, settings);
            Initialize();
        }*/

        //#endregion
        
        
        /*
        public void SetHorizontalMovementDirection()
        {
            HorizontalMovementDirection = (int) Sign(HorizontalMovement);
        }

        private void SetVerticalMovementDirection()
        {
            VerticalMovementDirection = (int) Sign(VerticalMovement);
        }

        public void MoveExternalForceTowards(float maxDistanceDelta)
        {
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

        public void ApplyForcesToHorizontalExternalForce(float force)
        {
            HorizontalExternalForce += force;
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

        public void OnApplyGroundAngle(float horizontalMovement)
        {
            SetHorizontalMovement(horizontalMovement);
            SetHorizontalSpeed(0);
            SetHorizontalExternalForce(0);
        }

        public void AddToMovement(Vector2 movement)
        {
            Movement += movement;
        }

        public void SetTransformTranslate(Vector2 movement)
        {
            Debug.Log("Movement: " + movement);
            character.transform.Translate(movement);
        }

        public void OnCeilingOrGroundCollision()
        {
            SetVerticalSpeed(0);
            SetVerticalExternalForce(0);
        }

        public void SetIgnoreFriction(bool ignore)
        {
            IgnoreFriction = ignore;
        }*//*

        #endregion

        #endregion
    }
}*/
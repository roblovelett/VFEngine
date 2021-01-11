using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Tools.BetterEvent;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObject;
    using static GameObject;
    using static Mathf;
    using static Vector2;
    using static Time;

    public class PhysicsController : SerializedMonoBehaviour
    {
        #region events

        public BetterEvent slopeBehavior;

        #endregion

        #region properties

        [OdinSerialize] public PhysicsData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private GameObject character;
        [OdinSerialize] private PhysicsSettings settings;
        private RaycastData raycastData;
        private new Transform transform;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = Find("Character");
            transform = character.transform;
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            if (!Data) Data = CreateInstance<PhysicsData>();
            Data.Initialize(settings);
        }

        private void SetDependencies()
        {
            raycastData = GetComponent<RaycastController>().Data;
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            SetDependencies();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private Vector2 DeltaMove => Data.DeltaMove;
        private int DeltaMoveXDirectionAxis => (int) Sign(DeltaMove.x);

        private IEnumerator InitializeFrame()
        {
            Data.SetDeltaMoveDirectionAxis(DeltaMoveXDirectionAxis);
            yield return null;
        }

        private bool IgnoreFriction => Data.IgnoreFriction;
        private bool OnGround => raycastData.OnGround;
        private float GroundFriction => Data.GroundFriction;
        private float AirFriction => Data.AirFriction;
        private float Friction => OnGround ? GroundFriction : AirFriction;
        private Vector2 ExternalForce => Data.ExternalForce;

        private Vector2 FrictionApplied =>
            MoveTowards(ExternalForce, zero, ExternalForce.magnitude * Friction * fixedDeltaTime);

        private float MinimumMoveThreshold => Data.MinimumMoveThreshold;
        private bool StopExternalForce => ExternalForce.magnitude <= MinimumMoveThreshold;

        private IEnumerator SetForces()
        {
            SetExternalForce();
            SetGravity();
            SetHorizontalExternalForce();
            yield return null;
        }

        private void SetExternalForce()
        {
            if (IgnoreFriction) return;
            Data.SetExternalForce(FrictionApplied);
            if (StopExternalForce) Data.SetExternalForce(zero);
        }

        private float GravityScale => Data.GravityScale;
        private float Gravity => Data.Gravity * GravityScale * fixedDeltaTime;
        private Vector2 Speed => Data.Speed;
        private bool ApplyGravityToSpeed => Speed.y > 0;

        private void SetGravity()
        {
            if (ApplyGravityToSpeed) Data.ApplyToSpeedY(Gravity);
            else Data.ApplyToExternalForceY(Gravity);
        }

        private bool OnSlope => raycastData.OnSlope;
        private float GroundAngle => raycastData.GroundAngle;
        private float MaximumSlopeAngle => Data.MaximumSlopeAngle;
        private float MinimumWallAngle => Data.MinimumWallAngle;

        private bool ApplyToExternalForce => OnSlope && GroundAngle > MaximumSlopeAngle &&
                                             (GroundAngle < MinimumWallAngle || Speed.x == 0);

        private int GroundDirection => raycastData.GroundDirection;
        private float ForcesApplied => -Data.Gravity * GroundFriction * GroundDirection * fixedDeltaTime / 4;

        private void SetHorizontalExternalForce()
        {
            if (!ApplyToExternalForce) return;
            Data.ApplyToExternalForceX(ForcesApplied);
        }

        private bool HorizontalDeltaMove => DeltaMove.x != 0;
        private bool SlopeBehavior => HorizontalDeltaMove && DeltaMove.y <= 0 && (DescendingSlope || ClimbingSlope);
        private bool DescendingSlope => GroundDirection == DeltaMoveXDirectionAxis;
        private float ClimbDeltaMoveY => Sin(GroundAngle * Deg2Rad) * SlopeDistance;
        private bool ClimbingSlope => GroundAngle < MinimumWallAngle && DeltaMove.y <= ClimbDeltaMoveY;

        private IEnumerator SetSlopeBehavior()
        {
            if (SlopeBehavior)
            {
                if (DescendingSlope) DescendSlope();
                else ClimbSlope();
                StopForcesY();
                slopeBehavior.Invoke();
            }

            yield return null;
        }

        private float SlopeDistance => Abs(DeltaMove.x);
        private float SlopeDeltaMoveX => Cos(GroundAngle * Deg2Rad) * SlopeDistance * DeltaMoveXDirectionAxis;
        private float DescendDeltaMoveY => -Sin(GroundAngle * Deg2Rad) * SlopeDistance;

        private void DescendSlope()
        {
            Data.SetDeltaMove(SlopeDeltaMoveX, DescendDeltaMoveY);
        }

        private void StopForcesY()
        {
            Data.SetSpeedY(0);
            Data.SetExternalForceY(0);
        }

        private void ClimbSlope()
        {
            Data.SetDeltaMove(SlopeDeltaMoveX, ClimbDeltaMoveY);
        }

        private RaycastHit2D Hit => raycastData.Hit;
        private float SkinWidth => raycastData.SkinWidth;
        private float DistanceSansSkinWidth => Hit.distance - SkinWidth;
        private float HorizontalHitSlopeDeltaMoveX => DistanceSansSkinWidth * DeltaMoveXDirectionAxis;

        private IEnumerator ApplySlopeBehaviorToDeltaMoveX()
        {
            Data.ApplyToDeltaMoveX(-HorizontalHitSlopeDeltaMoveX);
            ClimbSlope();
            Data.ApplyToDeltaMoveX(HorizontalHitSlopeDeltaMoveX);
            yield return null;
        }

        private float HorizontalHitMaximumSlopeDeltaMoveX =>
            Min(Abs(DeltaMove.x), DistanceSansSkinWidth) * DeltaMoveXDirectionAxis;

        private bool StopDeltaMoveY => DeltaMove.y < 0;

        private float HorizontalHitMaximumSlopeWithNonWallAngleDeltaMoveY =>
            Tan(GroundAngle * Deg2Rad) * Abs(DeltaMove.x) * Sign(DeltaMove.y);

        private IEnumerator ApplyMaximumSlopeBehaviorToDeltaMoveX()
        {
            Data.SetDeltaMoveX(HorizontalHitMaximumSlopeDeltaMoveX);
            yield return null;
        }

        private IEnumerator ApplyMaximumSlopeWithNonWallAngleToDeltaMoveY()
        {
            if (StopDeltaMoveY) Data.SetDeltaMoveY(0);
            else Data.SetDeltaMoveY(HorizontalHitMaximumSlopeWithNonWallAngleDeltaMoveY);
            yield return null;
        }

        private IEnumerator OnHorizontalHitWall()
        {
            StopForcesX();
            yield return null;
        }

        private void StopForcesX()
        {
            Data.SetSpeedX(0);
            Data.SetExternalForceX(0);
        }

        private IEnumerator OnHorizontalHitWallEdgeCase()
        {
            Data.SetSpeedX(0);
            yield return null;
        }

        private int DeltaMoveYDirectionAxis => (int) Sign(DeltaMove.y);
        private float VerticalHitDeltaMoveY => DistanceSansSkinWidth * DeltaMoveYDirectionAxis;

        private IEnumerator ApplyVerticalHitToDeltaMoveY()
        {
            Data.SetDeltaMoveY(VerticalHitDeltaMoveY);
            yield return null;
        }

        private float DeltaMoveXOnVerticalHitClimbingSlope =>
            DeltaMove.y / Tan(GroundAngle * Deg2Rad) * DeltaMoveYDirectionAxis;

        private IEnumerator ApplyClimbSlopeBehaviorOnVerticalHitToForces()
        {
            Data.SetDeltaMoveX(DeltaMoveXOnVerticalHitClimbingSlope);
            StopForcesX();
            yield return null;
        }

        private IEnumerator ApplyClimbSteepSlopeBehaviorToDeltaMoveX()
        {
            Data.SetDeltaMoveX(HorizontalHitSlopeDeltaMoveX);
            yield return null;
        }

        private float HitAngle => Angle(Hit.normal, up);
        private bool PositiveHitAngle => HitAngle > 0;

        private IEnumerator ApplyClimbMildSlopeBehaviorToDeltaMove()
        {
            float overshoot;
            if (PositiveHitAngle)
            {
                var tanA = Tan(HitAngle * Deg2Rad);
                var tanB = Tan(GroundAngle * Deg2Rad);
                var sin = Sin(GroundAngle * Deg2Rad);
                overshoot = (2 * tanA * Hit.distance - tanB * Hit.distance) / (tanA * sin - tanB * sin);
            }
            else
            {
                overshoot = Hit.distance / Sin(GroundAngle * Deg2Rad);
            }

            var removeX = Cos(GroundAngle * Deg2Rad) * overshoot * Sign(DeltaMove.x);
            var removeY = Sin(GroundAngle * Deg2Rad) * overshoot;
            var addX = Cos(HitAngle * Deg2Rad) * overshoot * Sign(DeltaMove.x);
            var addY = Sin(HitAngle * Deg2Rad) * overshoot;
            Data.ApplyToDeltaMove(new Vector2(addX - removeX, addY - removeY + SkinWidth));
            yield return null;
        }

        private float DeltaMoveYOnDescendMildSlope => -(Hit.distance - SkinWidth);

        private IEnumerator ApplyDescendMildSlopeBehaviorToDeltaMoveY()
        {
            Data.SetDeltaMoveY(DeltaMoveYOnDescendMildSlope);
            yield return null;
        }

        private bool PositiveGroundAngle => GroundAngle > 0;

        private IEnumerator ApplyDescendSteepSlopeBehaviorToDeltaMove()
        {
            float overshoot;
            if (PositiveGroundAngle)
            {
                var sin = Sin(GroundAngle) * Deg2Rad;
                var cos = Cos(GroundAngle) * Deg2Rad;
                var tan = Tan(HitAngle * Deg2Rad);
                overshoot = Hit.distance * cos / (tan / cos - sin);
            }
            else
            {
                overshoot = Hit.distance / Tan(HitAngle * Deg2Rad);
            }

            var removeX = Cos(GroundAngle * Deg2Rad) * overshoot * Sign(DeltaMove.x);
            var removeY = -Sin(GroundAngle * Deg2Rad) * overshoot;
            var addX = Cos(HitAngle * Deg2Rad) * overshoot * Sign(DeltaMove.x);
            var addY = -Sin(HitAngle * Deg2Rad) * overshoot;
            Data.ApplyToDeltaMove(new Vector2(addX - removeX, addY - removeY - SkinWidth));
            yield return null;
        }

        private IEnumerator TranslateDeltaMove()
        {
            transform.Translate(DeltaMove);
            yield return null;
        }

        private IEnumerator ApplyResetJumpCollisionBehaviorToForcesY()
        {
            StopForcesY();
            yield return null;
        }

        private IEnumerator ResetFriction()
        {
            Data.SetIgnoreFriction(false);
            yield return null;
        }

        #endregion

        #region event handlers

        public void OnPlatformerInitializeFrame()
        {
            StartCoroutine(InitializeFrame());
        }

        public void OnPlatformerSetForces()
        {
            StartCoroutine(SetForces());
        }

        public void OnPlatformerSetSlopeBehavior()
        {
            StartCoroutine(SetSlopeBehavior());
        }

        public void OnRaycastHorizontalHitSlope()
        {
            StartCoroutine(ApplySlopeBehaviorToDeltaMoveX());
        }

        public void OnRaycastHorizontalHitMaximumSlope()
        {
            StartCoroutine(ApplyMaximumSlopeBehaviorToDeltaMoveX());
        }

        public void OnRaycastHorizontalHitMaximumSlopeWithNonWallAngle()
        {
            StartCoroutine(ApplyMaximumSlopeWithNonWallAngleToDeltaMoveY());
        }

        public void OnRaycastHorizontalHitWall()
        {
            StartCoroutine(OnHorizontalHitWall());
        }

        public void OnRaycastHorizontalHitWallEdgeCase()
        {
            StartCoroutine(OnHorizontalHitWallEdgeCase());
        }

        public void OnRaycastVerticalHit()
        {
            StartCoroutine(ApplyVerticalHitToDeltaMoveY());
        }

        public void OnRaycastVerticalHitClimbingSlope()
        {
            StartCoroutine(ApplyClimbSlopeBehaviorOnVerticalHitToForces());
        }

        public void OnRaycastHorizontalHitSteepClimbingSlope()
        {
            StartCoroutine(ApplyClimbSteepSlopeBehaviorToDeltaMoveX());
        }

        public void OnRaycastHorizontalHitMildClimbingSlope()
        {
            StartCoroutine(ApplyClimbMildSlopeBehaviorToDeltaMove());
        }

        public void OnRaycastHorizontalHitMildDescendingSlope()
        {
            StartCoroutine(ApplyDescendMildSlopeBehaviorToDeltaMoveY());
        }

        public void OnRaycastHorizontalHitSteepDescendingSlope()
        {
            StartCoroutine(ApplyDescendSteepSlopeBehaviorToDeltaMove());
        }

        public void OnPlatformerTranslateDeltaMove()
        {
            StartCoroutine(TranslateDeltaMove());
        }

        public void OnRaycastResetJumpCollision()
        {
            StartCoroutine(ApplyResetJumpCollisionBehaviorToForcesY());
        }

        public void OnPlatformerResetFriction()
        {
            StartCoroutine(ResetFriction());
        }

        #endregion
    }
}
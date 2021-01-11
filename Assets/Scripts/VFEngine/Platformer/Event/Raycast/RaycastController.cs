using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Physics;
using VFEngine.Tools.BetterEvent;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;
    using static Vector2;
    using static Physics2D;
    using static Color;
    using static Debug;
    using static Mathf;
    using static RaycastData;
    using static RaycastData.RaycastHitType;

    public class RaycastController : SerializedMonoBehaviour
    {
        #region events

        public BetterEvent horizontalHitSlope;
        public BetterEvent horizontalHitMaximumSlope;
        public BetterEvent horizontalHitMaximumSlopeWithNonWallAngle;
        public BetterEvent horizontalHitWall;
        public BetterEvent horizontalHitWallEdgeCase;
        public BetterEvent verticalHit;
        public BetterEvent verticalHitClimbingSlope;
        public BetterEvent horizontalHitSteepClimbingSlope;
        public BetterEvent horizontalHitMildClimbingSlope;
        public BetterEvent horizontalHitMildDescendingSlope;
        public BetterEvent horizontalHitSteepDescendingSlope;
        public BetterEvent resetJumpCollision;

        #endregion

        #region properties

        [OdinSerialize] public RaycastData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private GameObject character;
        [OdinSerialize] private new BoxCollider2D collider;
        [OdinSerialize] private RaycastSettings settings;
        private LayerMaskData layerMaskData;
        private PhysicsData physicsData;
        private PlatformerData platformerData;
        private new Transform transform;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = GameObject.Find("Character");
            transform = character.transform;
            if (!collider) collider = GetComponent<BoxCollider2D>();
            if (!settings) settings = CreateInstance<RaycastSettings>();
            if (!Data) Data = CreateInstance<RaycastData>();
            Data.Initialize(collider, settings);
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            layerMaskData = GetComponent<LayerMaskController>().Data;
            physicsData = GetComponent<PhysicsController>().Data;
            platformerData = GetComponent<PlatformerController>().Data;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private IEnumerator InitializeFrame()
        {
            Data.ResetCollision();
            Data.SetBounds(collider);
            yield return null;
        }

        private RaycastHit2D Hit => Data.Hit;
        private int VerticalRays => Data.VerticalRays;
        private float SkinWidth => Data.SkinWidth;
        private bool PositiveDeltaMove => physicsData.DeltaMoveDirectionAxis == 1;
        private Vector2 BottomLeft => Data.BoundsBottomLeft;
        private Vector2 BottomRight => Data.BoundsBottomRight;
        private Vector2 VerticalBounds => PositiveDeltaMove ? BottomLeft : BottomRight;
        private Vector2 VerticalDirection => PositiveDeltaMove ? right : left;
        private float VerticalSpacing => Data.VerticalSpacing;
        private int Index => Data.Index;
        private float VerticalRayX => VerticalSpacing * Index;
        private Vector2 InitialVerticalOrigin => VerticalBounds + VerticalDirection * VerticalRayX;
        private float VerticalOriginX => InitialVerticalOrigin.x;
        private float VerticalOriginY => InitialVerticalOrigin.y + SkinWidth * 2;
        private Vector2 DownOrigin => new Vector2(VerticalOriginX, VerticalOriginY);
        private LayerMask Collision => layerMaskData.Collision;
        private float DownDistance => SkinWidth * 4f;
        private RaycastHit2D DownHit => Raycast(DownOrigin, down, DownDistance, Collision);
        private float IgnorePlatformsTime => platformerData.IgnorePlatformsTime;
        private bool SetHitForOneWayPlatform => !DownHit && IgnorePlatformsTime <= 0;
        private bool CastNextRayDown => Hit.distance <= 0;
        private LayerMask OneWayPlatform => layerMaskData.OneWayPlatform;
        private RaycastHit2D DownHitOneWayPlatform => Raycast(DownOrigin, down, DownDistance, OneWayPlatform);

        private IEnumerator GroundCollision()
        {
            for (var i = 0; i < VerticalRays; i++)
            {
                Data.SetIndex(i);
                InitializeRay(DownOrigin, DownHit);
                if (SetHitForOneWayPlatform)
                {
                    Data.SetHit(DownHitOneWayPlatform);
                    if (CastNextRayDown) continue;
                }

                if (!Hit) continue;
                Data.SetCollision(Ground, Hit);
                CastRay(DownOrigin, down * (SkinWidth * 2), blue);
                break;
            }

            yield return null;
        }

        private void InitializeRay(Vector2 origin, RaycastHit2D hit)
        {
            Data.SetOrigin(origin);
            Data.SetHit(hit);
        }

        private static void CastRay(Vector2 start, Vector2 direction, Color color)
        {
            DrawRay(start, direction, color);
        }

        private IEnumerator SetCollisionBelow(bool collision)
        {
            Data.SetCollisionBelow(collision);
            yield return null;
        }

        private Vector2 DeltaMove => physicsData.DeltaMove;
        private float HorizontalLength => Abs(DeltaMove.x) + SkinWidth;
        private int HorizontalRays => Data.HorizontalRays;
        private int DeltaMoveXDirectionAxis => (int) Sign(DeltaMove.x);
        private bool NegativeDeltaMoveXDirectionAxis => DeltaMoveXDirectionAxis == -1;
        private float HorizontalSpacing => Data.HorizontalSpacing;

        private Vector2 HorizontalOrigin => (NegativeDeltaMoveXDirectionAxis ? BottomLeft : BottomRight) +
                                            up * (HorizontalSpacing * Index);

        private RaycastHit2D HorizontalHit =>
            Raycast(HorizontalOrigin, right * DeltaMoveXDirectionAxis, HorizontalLength, Collision);

        private float HorizontalAngle => Angle(Hit.normal, up);
        private bool OnSlope => Data.OnSlope;
        private float MinimumWallAngle => physicsData.MinimumWallAngle;
        private bool FirstHit => Index == 0;
        private bool MetMinimumWallAngle => HorizontalAngle < MinimumWallAngle;
        private bool OnSlopeWithNonWallAngle => OnSlope && MetMinimumWallAngle;
        private bool HitSlope => FirstHit && !OnSlopeWithNonWallAngle;
        private float MaximumSlopeAngle => physicsData.MaximumSlopeAngle;
        private bool SecondHitMaximumAngle => !(FirstHit && OnSlope) && HorizontalAngle > MaximumSlopeAngle;
        private float MaximumSlopeHorizontalLength => Min(HorizontalLength, Hit.distance);
        private bool HorizontalDeltaMove => DeltaMove.x != 0;
        private float GroundAngle => Data.GroundAngle;
        private int GroundDirection => Data.GroundDirection;
        private Vector2 Speed => physicsData.Speed;

        private bool StopHorizontalSpeed => OnSlope && GroundAngle >= MinimumWallAngle &&
                                            GroundDirection != DeltaMoveXDirectionAxis && Speed.y < 0;

        private RaycastHit2D WallEdgeCaseHit => Raycast(BottomRight, left * GroundDirection, 1f, Collision);

        private IEnumerator HorizontalCollision()
        {
            if (HorizontalDeltaMove)
            {
                Data.SetLength(HorizontalLength);
                for (var i = 0; i < HorizontalRays; i++)
                {
                    Data.SetIndex(i);
                    InitializeRay(HorizontalOrigin, HorizontalHit);
                    CastRay(HorizontalOrigin, right * (DeltaMoveXDirectionAxis * HorizontalLength), red);
                    if (!Hit) continue;
                    if (HitSlope)
                    {
                        Data.SetCollision(HorizontalSlope, Hit);
                        horizontalHitSlope.Invoke();
                    }
                    else if (SecondHitMaximumAngle)
                    {
                        if (MetMinimumWallAngle) continue;
                        horizontalHitMaximumSlope.Invoke();
                        Data.SetLength(MaximumSlopeHorizontalLength);
                        if (OnSlopeWithNonWallAngle) horizontalHitMaximumSlopeWithNonWallAngle.Invoke();
                        Data.SetCollision(HorizontalWall, Hit, DeltaMoveXDirectionAxis);
                        horizontalHitWall.Invoke();
                    }
                }
            }

            if (StopHorizontalSpeed)
            {
                horizontalHitWallEdgeCase.Invoke();
                Data.SetOrigin(BottomRight);
                Data.SetCollision(HorizontalWallEdgeCase, WallEdgeCaseHit);
            }

            yield return null;
        }

        private bool VerticalDeltaMove => DeltaMove.y > 0 || DeltaMove.y < 0 && (!OnSlope || DeltaMove.x == 0);
        private int DeltaMoveYDirectionAxis => (int) Sign(DeltaMove.y);
        private float VerticalLength => Abs(DeltaMove.y) + SkinWidth;
        private Vector2 TopLeft => Data.BoundsTopLeft;
        private Vector2 VerticalOrigin => DeltaMoveYDirectionAxis == -1 ? BottomLeft : TopLeft;

        private RaycastHit2D VerticalHit =>
            Raycast(VerticalOrigin, up * DeltaMoveYDirectionAxis, VerticalLength, Collision);

        private bool VerticalCastOneWayPlatform => DeltaMoveYDirectionAxis < 0 && !Hit;

        private RaycastHit2D VerticalOneWayPlatformHit => Raycast(VerticalOrigin, up * DeltaMoveYDirectionAxis,
            VerticalLength, OneWayPlatform);

        private float AdjustedLengthForNextVerticalHit => Hit.distance;
        private bool VerticalHitClimbingSlope => OnSlope && DeltaMoveYDirectionAxis == 1;

        private IEnumerator VerticalCollision()
        {
            if (VerticalDeltaMove)
            {
                Data.SetLength(VerticalLength);
                for (var i = 0; i < VerticalRays; i++)
                {
                    Data.SetIndex(i);
                    InitializeRay(VerticalOrigin, VerticalHit);
                    CastRay(VerticalOrigin, up * (DeltaMoveYDirectionAxis * VerticalLength), red);
                    if (VerticalCastOneWayPlatform) Data.SetHit(VerticalOneWayPlatformHit);
                    if (!Hit) continue;
                    verticalHit.Invoke();
                    Data.SetLength(AdjustedLengthForNextVerticalHit);
                    if (VerticalHitClimbingSlope) verticalHitClimbingSlope.Invoke();
                    Data.SetCollision(Vertical, Hit, DeltaMoveYDirectionAxis);
                }
            }

            yield return null;
        }

        private bool OnGround => Data.OnGround;
        private bool DetectSlopeChange => OnGround && DeltaMove.x != 0 && Speed.y <= 0;
        private bool PositiveSlopeChange => DeltaMove.y > 0;

        private IEnumerator SlopeChangeCollision()
        {
            if (DetectSlopeChange)
            {
                if (PositiveSlopeChange) PositiveSlopeChangeCollision();
                else NegativeSlopeChangeCollision();
            }

            yield return null;
        }

        private void PositiveSlopeChangeCollision()
        {
            ClimbSteepSlopeCollision();
            ClimbMildSlopeCollision();
        }

        private float ClimbSteepSlopeLength => HorizontalLength * 2;

        private Vector2 ClimbSteepSlopeOrigin =>
            (DeltaMoveXDirectionAxis == -1 ? BottomLeft : BottomRight) + up * DeltaMove.y;

        private RaycastHit2D ClimbSteepSlopeHit => Raycast(ClimbSteepSlopeOrigin, right * DeltaMoveXDirectionAxis,
            ClimbSteepSlopeLength, Collision);

        private float ClimbSteepSlopeAngle => Angle(Hit.normal, up);
        private bool IsClimbingSteepSlopeAngle => Hit && Abs(ClimbSteepSlopeAngle - GroundAngle) > Tolerance;

        private void ClimbSteepSlopeCollision()
        {
            InitializeRay(ClimbSteepSlopeOrigin, ClimbSteepSlopeHit);
            if (!IsClimbingSteepSlopeAngle) return;
            horizontalHitSteepClimbingSlope.Invoke();
            Data.SetCollision(ClimbSteepSlope, Hit);
        }

        private Vector2 ClimbMildSlopeOrigin =>
            (NegativeDeltaMoveXDirectionAxis ? BottomLeft : BottomRight) + DeltaMove;

        private RaycastHit2D ClimbMildSlopeHit => Raycast(ClimbMildSlopeOrigin, down, 1, Collision);
        private float GroundLayer => Data.GroundLayer;
        private float ClimbMildSlopeAngle => Angle(Hit.normal, up);

        private bool IsClimbingMildSlopeGroundAngle => Hit &&
                                                       Abs(Hit.collider.gameObject.layer - GroundLayer) < Tolerance &&
                                                       ClimbMildSlopeAngle < GroundAngle;

        private void ClimbMildSlopeCollision()
        {
            if (IsClimbingSteepSlopeAngle) return;
            InitializeRay(ClimbMildSlopeOrigin, ClimbMildSlopeHit);
            CastRay(ClimbMildSlopeOrigin, down, yellow);
            if (IsClimbingMildSlopeGroundAngle) horizontalHitMildClimbingSlope.Invoke();
        }

        private void NegativeSlopeChangeCollision()
        {
            DescendMildSlopeCollision();
            DescendSteepSlopeCollision();
        }

        private float DescendMildSlopeLength => Abs(DeltaMove.y) + SkinWidth;

        private Vector2 DescendMildSlopeOrigin =>
            (NegativeDeltaMoveXDirectionAxis ? BottomRight : BottomLeft) + right * DeltaMove.x;

        private RaycastHit2D DescendMildSlopeHit =>
            Raycast(DescendMildSlopeOrigin, down, DescendMildSlopeLength, Collision);

        private float DescendMildSlopeAngle => Angle(Hit.normal, up);
        private bool IsDescendingMildSlopeAngle => Hit && DescendMildSlopeAngle < GroundAngle;

        private void DescendMildSlopeCollision()
        {
            InitializeRay(DescendMildSlopeOrigin, DescendMildSlopeHit);
            if (!IsDescendingMildSlopeAngle) return;
            horizontalHitMildDescendingSlope.Invoke();
            Data.SetCollision(DescendMildSlope, Hit);
        }

        private bool PositiveDeltaMoveXDirectionAxis => DeltaMoveXDirectionAxis == 1;

        private Vector2 DescendSteepSlopeOrigin =>
            (PositiveDeltaMoveXDirectionAxis ? BottomLeft : BottomRight) + DeltaMove;

        private RaycastHit2D DescendSteepSlopeHit => Raycast(DescendSteepSlopeOrigin, down, 1, Collision);

        private bool IsDescendingSteepSlope => Hit && Abs(Sign(Hit.normal.x) - DeltaMoveXDirectionAxis) < Tolerance &&
                                               Abs(Hit.collider.gameObject.layer - GroundLayer) < Tolerance;

        private float DescendSteepSlopeAngle => Angle(Hit.normal, up);
        private bool FacingRight => physicsData.FacingRight;

        private bool IsDescendingSteepSlopeAngle => IsDescendingSteepSlope && DescendSteepSlopeAngle > GroundAngle &&
                                                    Abs(Sign(Hit.normal.x) - (FacingRight ? 1 : -1)) < Tolerance;

        private void DescendSteepSlopeCollision()
        {
            if (IsDescendingMildSlopeAngle) return;
            InitializeRay(DescendSteepSlopeOrigin, DescendSteepSlopeHit);
            CastRay(DescendSteepSlopeOrigin, down, yellow);
            if (IsDescendingSteepSlopeAngle) horizontalHitSteepDescendingSlope.Invoke();
        }

        private IEnumerator CastRayFromInitialPosition()
        {
            CastRay(transform.position, DeltaMove * 3f, green);
            yield return null;
        }

        private bool VerticalHitCollision => Data.VerticalHit;
        private Vector2 TotalSpeed => physicsData.TotalSpeed;
        private bool CollidingBelow => Data.CollidingBelow;
        private bool CollidingAbove => Data.CollidingAbove;

        private bool ResetJump => VerticalHitCollision && CollidingBelow && TotalSpeed.y < 0 ||
                                  CollidingAbove && TotalSpeed.y > 0 && (!OnSlope || GroundAngle < MinimumWallAngle);

        private IEnumerator ResetJumpCollision()
        {
            if (ResetJump) resetJumpCollision.Invoke();
            yield return null;
        }

        #endregion

        #region event handlers

        public void OnPlatformerInitializeFrame()
        {
            StartCoroutine(InitializeFrame());
        }

        public void OnPlatformerGroundCollision()
        {
            StartCoroutine(GroundCollision());
        }

        public void OnPhysicsSlopeBehavior()
        {
            StartCoroutine(SetCollisionBelow(true));
        }

        public void OnPlatformerHorizontalCollision()
        {
            StartCoroutine(HorizontalCollision());
        }

        public void OnPlatformerVerticalCollision()
        {
            StartCoroutine(VerticalCollision());
        }

        public void OnPlatformerSlopeChangeCollision()
        {
            StartCoroutine(SlopeChangeCollision());
        }

        public void OnPlatformerCastRayFromInitialPosition()
        {
            StartCoroutine(CastRayFromInitialPosition());
        }

        public void OnPlatformerResetJumpCollision()
        {
            StartCoroutine(ResetJumpCollision());
        }

        #endregion
    }
}

#region hide

/*public void OnPlatformerCastRaysDown()
        {
            Raycast.OnCastRaysDown();
        }

        public void OnPlatformerSetDownHitAtOneWayPlatform()
        {
            Raycast.SetDownHitAtOneWayPlatform();
        }

        private RaycastData RaycastData => Raycast.Data;
        private Vector2 Origin => RaycastData.Origin;
        private Vector2 DownRayOrigin => Origin;
        private float SkinWidth => RaycastData.SkinWidth;
        private Vector2 DownRayDirection => down * SkinWidth * 2;
        private static Color DownRayColor => blue;

        public void OnPlatformerDownHit()
        {
            Raycast.OnDownHit();
            DrawRay(DownRayOrigin, DownRayDirection, DownRayColor);
        }

        public void OnPlatformerSlopeBehavior()
        {
            Raycast.OnSlopeBehavior();
        }

        public void OnPlatformerInitializeLengthForSideRay()
        {
            Raycast.InitializeLengthForSideRay();
        }

        private Vector2 SideRayOrigin => Origin;
        private int HorizontalMovementDirection => Physics.HorizontalMovementDirection;
        private float Length => RaycastData.Length;
        private float SideRayLength => Length;
        private Vector2 SideRayDirection => right * HorizontalMovementDirection * SideRayLength;
        private static Color SideRayColor => red;

        public void OnPlatformerCastRaysToSides()
        {
            Raycast.OnCastRaysToSides();
            DrawRay(SideRayOrigin, SideRayDirection, SideRayColor);
        }

        public void OnPlatformerOnFirstSideHit()
        {
            Raycast.OnFirstSideHit();
        }

        public void OnPlatformerSetLengthForSideRay()
        {
            Raycast.SetLengthForSideRay();
        }

        public void OnPlatformerHitWall()
        {
            Raycast.OnHitWall();
        }

        public void OnPlatformerOnStopHorizontalSpeedHit()
        {
            Raycast.OnStopHorizontalSpeedHit();
        }

        public void OnPlatformerInitializeLengthForVerticalRay()
        {
            Raycast.InitializeLengthForVerticalRay();
        }

        private Vector2 VerticalRayOrigin => Origin;
        private int VerticalMovementDirection => Physics.VerticalMovementDirection;
        private float VerticalRayLength => Length;
        private Vector2 VerticalRayDirection => up * VerticalMovementDirection * VerticalRayLength;
        private static Color VerticalRayColor => red;

        public void OnPlatformerCastRaysVertically()
        {
            Raycast.OnCastRaysVertically();
            DrawRay(VerticalRayOrigin, VerticalRayDirection, VerticalRayColor);
        }

        public void OnPlatformerSetVerticalHitAtOneWayPlatform()
        {
            Raycast.SetVerticalHitAtOneWayPlatform();
        }

        public void OnPlatformerVerticalHit()
        {
            Raycast.OnVerticalHit();
        }

        public void OnPlatformerInitializeClimbSteepSlope()
        {
            Raycast.OnInitializeClimbSteepSlope();
        }

        public void OnPlatformerClimbSteepSlope()
        {
            Raycast.OnClimbSteepSlope();
        }

        public void OnPlatformerInitializeClimbMildSlope()
        {
            Raycast.OnInitializeClimbMildSlope();
            DrawRay(Origin, down, yellow);
        }

        public void OnPlatformerInitializeDescendMildSlope()
        {
            Raycast.OnInitializeDescendMildSlope();
        }

        public void OnPlatformerDescendMildSlope()
        {
            Raycast.OnDescendMildSlope();
        }

        public void OnPlatformerInitializeDescendSteepSlope()
        {
            Raycast.OnInitializeDescendSteepSlope();
            DrawRay(Origin, down, yellow);
        }

        private Vector2 InitialPositionStart => Physics.Transform.position;
        private Vector2 Movement => Physics.Movement;
        private Vector2 InitialPositionDirection => Movement * 3f;

        public void OnPlatformerCastRayFromInitialPosition()
        {
            DrawRay(InitialPositionStart, InitialPositionDirection, green);
        }*/

#endregion
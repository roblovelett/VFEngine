using Packages.BetterEvent;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.ScriptableObjects;
using VFEngine.Platformer.ScriptableObjects;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObject;

    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastController : SerializedMonoBehaviour
    {
        #region events

        public BetterEvent initializedFrameForPlatformer;
        public BetterEvent castedGroundCollisionRaycastForPlatformer;

        #endregion

        #region properties

        [OdinSerialize] public RaycastData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private GameObject character;
        [OdinSerialize] private BoxCollider2D boxCollider;
        [OdinSerialize] private RaycastSettings settings;
        [OdinSerialize] private PlatformerController platformerController;
        [OdinSerialize] private LayerMaskController layerMaskController;
        [OdinSerialize] private PhysicsController physicsController;
        private LayerMaskData layerMaskData;
        private PhysicsData physicsData;
        private PlatformerData platformerData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = GameObject.Find("Character");
            if (!boxCollider) boxCollider = GetComponent<BoxCollider2D>();
            if (!settings) settings = CreateInstance<RaycastSettings>();
            if (!platformerController) platformerController = GetComponent<PlatformerController>();
            if (!layerMaskController) layerMaskController = GetComponent<LayerMaskController>();
            if (!physicsController) physicsController = GetComponent<PhysicsController>();
            if (!Data) Data = CreateInstance<RaycastData>();
            Data.OnInitialize(boxCollider, settings);
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

        private void SetDependencies()
        {
            layerMaskData = layerMaskController.Data;
            physicsData = physicsController.Data;
            platformerData = platformerController.Data;
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void InitializeFrame()
        {
            Data.OnInitializeFrame(boxCollider);
        }

        private int VerticalRays => Data.VerticalRays;
        private int DeltaMoveXDirectionAxis => physicsData.DeltaMoveXDirectionAxis;
        private LayerMask Collision => layerMaskData.Collision;
        private LayerMask OneWayPlatform => layerMaskData.OneWayPlatform;
        private RaycastHit2D RaycastHit => Data.Hit;
        private float IgnorePlatformsTime => Data.IgnorePlatformsTime;
        private bool CastGroundCollisionRaycastForOneWayPlatform => !RaycastHit && IgnorePlatformsTime <= 0;
        private bool RaycastHitMissed => RaycastHit.distance <= 0;

        private void GroundCollisionRaycast()
        {
            for (var i = 0; i < VerticalRays; i++)
            {
                SetGroundCollisionRaycast(DeltaMoveXDirectionAxis, i, Collision);
                if (CastGroundCollisionRaycastForOneWayPlatform)
                {
                    SetGroundCollisionRaycastHit(OneWayPlatform);
                    if (RaycastHitMissed) continue;
                }

                if (!RaycastHit) continue;
                OnGroundCollisionRaycastHit();
                break;
            }
        }

        private void OnGroundCollisionRaycastHit()
        {
            Data.OnGroundCollisionRaycastHit();
        }

        private void SetGroundCollisionRaycast(int deltaMoveXDirectionAxis, int index, LayerMask layer)
        {
            Data.OnSetGroundCollisionRaycast(deltaMoveXDirectionAxis, index, layer);
        }

        private void SetGroundCollisionRaycastHit(LayerMask layer)
        {
            Data.OnSetGroundCollisionRaycastHit(layer);
        }

        #endregion

        #region event handlers

        public void OnPlatformerInitializeFrame()
        {
            InitializeFrame();
            initializedFrameForPlatformer.Invoke();
        }

        public void OnPlatformerGroundCollisionRaycast()
        {
            GroundCollisionRaycast();
            castedGroundCollisionRaycastForPlatformer.Invoke();
        }

        #endregion
    }
}

#region hide
/*
private Vector2 DeltaMove => physicsData.DeltaMove;
private float HorizontalLength => Abs(DeltaMove.x) + SkinWidth;
private int HorizontalRays => Data.HorizontalRays;
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
private Vector2 Speed => physicsData.Speed;

private bool StopHorizontalSpeed => OnSlope && GroundAngle >= MinimumWallAngle &&
                                    GroundDirectionAxis != DeltaMoveXDirectionAxis && Speed.y < 0;

private RaycastHit2D WallEdgeCaseHit => Raycast(BottomRight, left * GroundDirectionAxis, 1f, Collision);
*/
/*
private bool VerticalDeltaMove => DeltaMove.y > 0 || DeltaMove.y < 0 && (!OnSlope || DeltaMove.x == 0);
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

private void VerticalCollision()
{
    if (!VerticalDeltaMove) return;
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

private bool OnGround => Data.OnGround;
private bool DetectSlopeChange => OnGround && DeltaMove.x != 0 && Speed.y <= 0;
private bool PositiveSlopeChange => DeltaMove.y > 0;

private void SlopeChangeCollision()
{
    if (!DetectSlopeChange) return;
    if (PositiveSlopeChange) PositiveSlopeChangeCollision();
    else NegativeSlopeChangeCollision();
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

private void CastRayFromInitialPosition()
{
    CastRay(transform.position, DeltaMove * 3f, green);
}

private bool VerticalHitCollision => Data.VerticalHit;
private Vector2 TotalSpeed => physicsData.TotalSpeed;
private bool CollidingBelow => Data.CollidingBelow;
private bool CollidingAbove => Data.CollidingAbove;

private bool ResetJump => VerticalHitCollision && CollidingBelow && TotalSpeed.y < 0 ||
                          CollidingAbove && TotalSpeed.y > 0 && (!OnSlope || GroundAngle < MinimumWallAngle);

private void ResetJumpCollision()
{
    if (!ResetJump) return;
    resetJumpCollision.Invoke();
}
*/
/*
public void OnPlatformerVerticalCollision()
{
    VerticalCollision();
}

public void OnPlatformerSlopeChangeCollision()
{
    SlopeChangeCollision();
}

public void OnPlatformerCastRayFromInitialPosition()
{
    CastRayFromInitialPosition();
}

public void OnPlatformerResetJumpCollision()
{
    ResetJumpCollision();
}*/
#endregion
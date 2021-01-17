using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Physics.ScriptableObjects;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObject;
    using static GameObject;
    using static Time;
    using static UniTask;

    public class PhysicsController : SerializedMonoBehaviour
    {
        #region events

        #endregion

        #region properties

        [OdinSerialize] public PhysicsData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private GameObject character;
        [OdinSerialize] private PhysicsSettings settings;
        private RaycastData raycastData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            if (!Data) Data = CreateInstance<PhysicsData>();
            Data.OnInitialize(settings);
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

        private async UniTask InitializeFrame()
        {
            Data.OnInitializeFrame(fixedDeltaTime);
            await Yield();
        }

        private bool OnGround => raycastData.OnGround;
        private bool OnSlope => raycastData.OnSlope;
        private int GroundDirectionAxis => raycastData.GroundDirectionAxis;
        private float GroundAngle => raycastData.GroundAngle;

        private async UniTask UpdateForces()
        {
            Data.OnUpdateForces(OnGround, OnSlope, GroundDirectionAxis, GroundAngle, fixedDeltaTime);
            await Yield();
        }

        private async UniTask DescendSlope()
        {
            Data.OnDescendSlope(GroundAngle);
            await Yield();
        }

        private async UniTask ClimbSlope()
        {
            Data.OnClimbSlope(GroundAngle);
            await Yield();
        }

        private RaycastHit2D Hit => raycastData.Hit;
        private float SkinWidth => raycastData.SkinWidth;

        private async UniTask HitClimbingSlope()
        {
            Data.OnHitClimbingSlope(GroundAngle, Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask HitMaximumSlope(float hitDistance, float skinWidth)
        {
            Data.OnHitMaximumSlope(hitDistance, skinWidth);
            await Yield();
        }

        private async UniTask HitSlopedGroundAngle()
        {
            Data.OnHitSlopedGroundAngle(GroundAngle);
            await Yield();
        }

        private async UniTask HitMaximumSlope()
        {
            Data.OnHitMaximumSlope();
            await Yield();
        }

        private async UniTask StopHorizontalSpeed()
        {
            Data.OnStopHorizontalSpeed();
            await Yield();
        }

        private async UniTask VerticalCollision()
        {
            Data.OnVerticalCollision(Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask VerticalCollisionHitClimbingSlope()
        {
            Data.OnVerticalCollisionHitClimbingSlope(GroundAngle);
            await Yield();
        }

        private async UniTask ClimbSteepSlope()
        {
            Data.OnClimbSteepSlope(Hit.distance, SkinWidth);
            await Yield();
        }

        private float HitAngle => raycastData.HitAngle;
        private async UniTask ClimbMildSlope()
        {
            Data.OnClimbMildSlope(HitAngle, GroundAngle, Hit.distance, SkinWidth);
            await Yield();
        }

        #endregion

        #region event handlers

        public async UniTask OnPlatformerInitializeFrame()
        {
            await InitializeFrame();
        }

        public async UniTask OnPlatformerUpdateForces()
        {
            await UpdateForces();
        }

        public async UniTask OnPlatformerDescendSlope()
        {
            await DescendSlope();
        }

        public async UniTask OnPlatformerClimbSlope()
        {
            await ClimbSlope();
        }

        public async UniTask OnRaycastHorizontalCollisionRaycastHitClimbingSlope()
        {
            await HitClimbingSlope();
        }

        public async UniTask OnRaycastHorizontalCollisionRaycastHitMaximumSlopeSetDeltaMoveX()
        {
            await HitMaximumSlope(Hit.distance, SkinWidth);
        }

        public async UniTask OnHorizontalCollisionRaycastHitSlopedGroundAngle()
        {
            await HitSlopedGroundAngle();
        }

        public async UniTask OnRaycastHorizontalCollisionRaycastHitMaximumSlope()
        {
            await HitMaximumSlope();
        }

        public async UniTask OnPlatformerStopHorizontalSpeed()
        {
            await StopHorizontalSpeed();
        }

        public async UniTask OnRaycastVerticalCollisionRaycastHit()
        {
            await VerticalCollision();
        }

        public async UniTask OnRaycastVerticalCollisionRaycastHitClimbingSlope()
        {
            await VerticalCollisionHitClimbingSlope();
        }

        public async UniTask OnPlatformerClimbSteepSlopeHit()
        {
            await ClimbSteepSlope();
        }

        public async UniTask OnPlatformerClimbMildSlopeHit()
        {
            await ClimbMildSlope();
        }

        #endregion
    }
}

#region hide

/*
        private void SetForces()
        {
            Data.OnSetForces(OnGround, OnSlope, GroundDirectionAxis, GroundAngle, fixedDeltaTime);
        }

        private bool DescendingSlope => GroundDirectionAxis == DeltaMoveXDirectionAxis;

        private bool ClimbingSlope => GroundAngle < MinimumWallAngle &&
                                      DeltaMove.y <= Sin(GroundAngle * Deg2Rad) * Abs(DeltaMove.x);

        private bool SlopeBehavior => DeltaMove.x != 0 && DeltaMove.y <= 0 && (DescendingSlope || ClimbingSlope);

        private void SetSlopeBehavior()
        {
            if (!SlopeBehavior) return;
            Data.OnSetSlopeBehavior(GroundDirectionAxis, GroundAngle);
            //slopeBehavior.Invoke();
        }

        private RaycastHit2D Hit => raycastData.Hit;
        private float SkinWidth => raycastData.SkinWidth;
        private void OnHorizontalCollisionHit()
        {
            Data.OnRaycastHorizontalCollisionHit(Hit, SkinWidth, GroundAngle);
        }
        */
/*
        public void OnPlatformerInitializeFrame()
        {
            InitializeFrame();
        }

        public void OnPlatformerSetForces()
        {
            SetForces();
        }

        public void OnPlatformerSetSlopeBehavior()
        {
            SetSlopeBehavior();
        }

        public void OnRaycastHorizontalCollisionHitClimbingSlope()
        {
            OnHorizontalCollisionHit();//ApplyClimbingSlopeBehavior
            //ApplySlopeBehaviorToDeltaMoveX
        }
        */
/*
private RaycastHit2D Hit => raycastData.Hit;
private float SkinWidth => raycastData.SkinWidth;
private float DistanceSansSkinWidth => Hit.distance - SkinWidth;
private float HorizontalHitSlopeDeltaMoveX => DistanceSansSkinWidth * DeltaMoveXDirectionAxis;

private void ApplySlopeBehaviorToDeltaMoveX()
{
    Data.ApplyToDeltaMoveX(-HorizontalHitSlopeDeltaMoveX);
    ClimbSlope();
    Data.ApplyToDeltaMoveX(HorizontalHitSlopeDeltaMoveX);
}

private float HorizontalHitMaximumSlopeDeltaMoveX =>
    Min(Abs(DeltaMove.x), DistanceSansSkinWidth) * DeltaMoveXDirectionAxis;

private bool StopDeltaMoveY => DeltaMove.y < 0;

private float HorizontalHitMaximumSlopeWithNonWallAngleDeltaMoveY =>
    Tan(GroundAngle * Deg2Rad) * Abs(DeltaMove.x) * Sign(DeltaMove.y);

private void ApplyMaximumSlopeBehaviorToDeltaMoveX()
{
    Data.SetDeltaMoveX(HorizontalHitMaximumSlopeDeltaMoveX);
}

private void ApplyMaximumSlopeWithNonWallAngleToDeltaMoveY()
{
    if (StopDeltaMoveY) Data.SetDeltaMoveY(0);
    else Data.SetDeltaMoveY(HorizontalHitMaximumSlopeWithNonWallAngleDeltaMoveY);
}

private void OnHorizontalHitWall()
{
    StopForcesX();
}

private void StopForcesX()
{
    Data.SetSpeedX(0);
    Data.SetExternalForceX(0);
}

private void OnHorizontalHitWallEdgeCase()
{
    Data.SetSpeedX(0);
}

private float VerticalHitDeltaMoveY => DistanceSansSkinWidth * DeltaMoveYDirectionAxis;

private void ApplyVerticalHitToDeltaMoveY()
{
    Data.SetDeltaMoveY(VerticalHitDeltaMoveY);
}

private float DeltaMoveXOnVerticalHitClimbingSlope =>
    DeltaMove.y / Tan(GroundAngle * Deg2Rad) * DeltaMoveYDirectionAxis;

private void ApplyClimbSlopeBehaviorOnVerticalHitToForces()
{
    Data.SetDeltaMoveX(DeltaMoveXOnVerticalHitClimbingSlope);
    StopForcesX();
}

private void ApplyClimbSteepSlopeBehaviorToDeltaMoveX()
{
    Data.SetDeltaMoveX(HorizontalHitSlopeDeltaMoveX);
}

private float HitAngle => Angle(Hit.normal, up);
private bool PositiveHitAngle => HitAngle > 0;

private void ApplyClimbMildSlopeBehaviorToDeltaMove()
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
}

private float DeltaMoveYOnDescendMildSlope => -(Hit.distance - SkinWidth);

private void ApplyDescendMildSlopeBehaviorToDeltaMoveY()
{
    Data.SetDeltaMoveY(DeltaMoveYOnDescendMildSlope);
}

private bool PositiveGroundAngle => GroundAngle > 0;

private void ApplyDescendSteepSlopeBehaviorToDeltaMove()
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
}

private void TranslateDeltaMove()
{
    transform.Translate(DeltaMove);
}

private void ApplyResetJumpCollisionBehaviorToForcesY()
{
    StopForcesY();
}

private void ResetFriction()
{
    Data.SetIgnoreFriction(false);
}
*/
/*
public void OnRaycastHorizontalHitSlope()
{
    ApplySlopeBehaviorToDeltaMoveX();
}

public void OnRaycastHorizontalHitMaximumSlope()
{
    ApplyMaximumSlopeBehaviorToDeltaMoveX();
}

public void OnRaycastHorizontalHitMaximumSlopeWithNonWallAngle()
{
    ApplyMaximumSlopeWithNonWallAngleToDeltaMoveY();
}

public void OnRaycastHorizontalHitWall()
{
    OnHorizontalHitWall();
}

public void OnRaycastHorizontalHitWallEdgeCase()
{
    OnHorizontalHitWallEdgeCase();
}

public void OnRaycastVerticalHit()
{
    ApplyVerticalHitToDeltaMoveY();
}

public void OnRaycastVerticalHitClimbingSlope()
{
    ApplyClimbSlopeBehaviorOnVerticalHitToForces();
}

public void OnRaycastHorizontalHitSteepClimbingSlope()
{
    ApplyClimbSteepSlopeBehaviorToDeltaMoveX();
}

public void OnRaycastHorizontalHitMildClimbingSlope()
{
    ApplyClimbMildSlopeBehaviorToDeltaMove();
}

public void OnRaycastHorizontalHitMildDescendingSlope()
{
    ApplyDescendMildSlopeBehaviorToDeltaMoveY();
}

public void OnRaycastHorizontalHitSteepDescendingSlope()
{
    ApplyDescendSteepSlopeBehaviorToDeltaMove();
}

public void OnPlatformerTranslateDeltaMove()
{
    TranslateDeltaMove();
}

public void OnRaycastResetJumpCollision()
{
    ApplyResetJumpCollisionBehaviorToForcesY();
}

public void OnPlatformerResetFriction()
{
    ResetFriction();
}*/

#endregion
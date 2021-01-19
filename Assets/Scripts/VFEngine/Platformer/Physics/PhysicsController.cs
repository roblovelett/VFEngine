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
        private bool IgnoreFriction => raycastData.IgnoreFriction;
        private int GroundDirectionAxis => raycastData.GroundDirectionAxis;
        private float GroundAngle => raycastData.GroundAngle;

        private async UniTask UpdateForces()
        {
            Data.OnUpdateForces(OnGround, OnSlope, IgnoreFriction, GroundDirectionAxis, GroundAngle, fixedDeltaTime);
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

        private async UniTask DescendMildSlope()
        {
            Data.OnDescendMildSlope(Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask DescendSteepSlope()
        {
            Data.OnDescendSteepSlope(HitAngle, GroundAngle, Hit.distance, SkinWidth);
            await Yield();
        }

        private async UniTask Move()
        {
            Data.OnMove(ref character);
            await Yield();
        }

        private async UniTask ResetJumpCollision()
        {
            Data.OnResetJumpCollision();
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

        public async UniTask OnPlatformerDescendMildSlopeHit()
        {
            await DescendMildSlope();
        }

        public async UniTask OnPlatformerDescendSteepSlopeHit()
        {
            await DescendSteepSlope();
        }

        public async UniTask OnPlatformerMove()
        {
            await Move();
        }

        public async UniTask OnPlatformerResetJumpCollision()
        {
            await ResetJumpCollision();
        }

        #endregion
    }
}
using Cysharp.Threading.Tasks;
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
    using static UniTask;

    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastController : SerializedMonoBehaviour
    {
        #region events

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
            Data.OnInitialize(ref boxCollider, ref character, settings);
        }

        private void SetDependencies()
        {
            layerMaskData = layerMaskController.Data;
            physicsData = physicsController.Data;
            platformerData = platformerController.Data;
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
            Data.OnInitializeFrame();
            await Yield();
        }

        private async UniTask UpdateBounds()
        {
            Data.OnUpdateBounds(boxCollider, character);
            await Yield();
        }

        private Vector2 NewPosition => physicsData.NewPosition;
        private LayerMask Platform => layerMaskData.Platform;

        private async UniTask PerformSafetyBoxcast()
        {
            Data.OnPerformSafetyBoxcast(character, NewPosition, Platform);
            await Yield();
        }

        private async UniTask SetGroundedEvent()
        {
            Data.OnSetGroundedEvent();
            await Yield();
        }

        private LayerMask BelowPlatforms => layerMaskData.BelowPlatforms;

        private async UniTask SetDistanceToGroundRaycast()
        {
            Data.OnSetDistanceToGroundRaycast(character, BelowPlatforms);
            await Yield();
        }

        private async UniTask SetDistanceToGroundOnRaycastHit()
        {
            Data.OnSetDistanceToGroundOnRaycastHit();
            await Yield();
        }

        private async UniTask SetDistanceToGround()
        {
            Data.OnSetDistanceToGround();
            await Yield();
        }

        private LayerMask SavedBelow => layerMaskData.SavedBelow;

        private async UniTask SetStandingOnLastFrameLayerToSavedBelow()
        {
            Data.OnSetStandingOnLastFrameLayerToSavedBelow(SavedBelow);
            await Yield();
        }

        private async UniTask ApplyMovingPlatformBehavior()
        {
            Data.OnApplyMovingPlatformBehavior();
            await Yield();
        }

        private async UniTask SetCurrentRaycastDirectionToLeft()
        {
            Data.OnSetCurrentRaycastDirectionToLeft();
            await Yield();
        }

        private async UniTask SetCurrentRaycastDirectionToRight()
        {
            Data.OnSetCurrentRaycastDirectionToRight();
            await Yield();
        }

        private Vector2 Speed => physicsData.Speed;

        private async UniTask SetHorizontalRaycast()
        {
            Data.OnSetHorizontalRaycast(character, Speed.x);
            await Yield();
        }

        private async UniTask ResizeHorizontalHitStorage()
        {
            Data.OnResizeHorizontalHitStorage();
            await Yield();
        }

        private int Index => platformerData.Index;

        private async UniTask SetHorizontalRaycastOrigin()
        {
            Data.OnSetHorizontalRaycastOrigin(Index);
            await Yield();
        }

        private async UniTask SetHorizontalRaycastForPlatform()
        {
            Data.OnSetHorizontalRaycastForPlatform(Index, character, Platform);
            await Yield();
        }

        private LayerMask OneWayPlatform => layerMaskData.OneWayPlatform;
        private LayerMask MovingOneWayPlatform => layerMaskData.MovingOneWayPlatform;

        private async UniTask SetHorizontalRaycastForSpecialPlatforms()
        {
            Data.OnSetHorizontalRaycastForSpecialPlatforms(Index, character, Platform, OneWayPlatform,
                MovingOneWayPlatform);
            await Yield();
        }

        private async UniTask SetHorizontalHitAngle()
        {
            Data.OnSetHorizontalHitAngle(Index, character);
            await Yield();
        }

        private async UniTask SetLateralSlopeAngle()
        {
            Data.OnSetLateralSlopeAngle();
            await Yield();
        }

        private async UniTask SetCollisionOnHitWall()
        {
            Data.OnSetCollisionOnHitWall(Index);
            await Yield();
        }

        private async UniTask SetCollisionOnHitWallInMovementDirection()
        {
            Data.OnSetCollisionOnHitWallInMovementDirection(Index);
            await Yield();
        }

        private async UniTask AddHorizontalContact()
        {
            Data.OnAddHorizontalContact(Index);
            await Yield();
        }

        private int SmallestDistanceIndex => platformerData.SmallestDistanceIndex;

        private async UniTask FrictionTest()
        {
            Data.OnFrictionTest(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask SetFriction()
        {
            Data.OnSetFriction();
            await Yield();
        }

        private async UniTask MovingPlatformTest()
        {
            Data.OnMovingPlatformTest(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask SetCurrentRaycastDirectionToDown()
        {
            Data.OnSetCurrentRaycastDirectionToDown();
            await Yield();
        }

        private async UniTask InitializeFriction()
        {
            Data.OnInitializeFriction();
            await Yield();
        }

        private async UniTask SetNotCollidingBelow()
        {
            Data.OnSetNotCollidingBelow();
            await Yield();
        }

        private async UniTask SetVerticalRaycastLength()
        {
            Data.OnSetVerticalRaycastLength();
            await Yield();
        }

        private async UniTask SetVerticalRaycastLengthOnMovingPlatform()
        {
            Data.OnSetVerticalRaycastLengthOnMovingPlatform();
            await Yield();
        }

        private async UniTask ApplyNewPositionYToVerticalRaycastLength()
        {
            Data.OnApplyNewPositionYToVerticalRaycastLength(NewPosition.y);
            await Yield();
        }

        private async UniTask SetVerticalRaycast()
        {
            Data.OnSetVerticalRaycast(character, NewPosition.x);
            await Yield();
        }

        private async UniTask ResizeBelowHitStorage()
        {
            Data.OnResizeBelowHitStorage();
            await Yield();
        }

        private async UniTask SetStandingOnLastFrameLayerToPlatforms()
        {
            Data.OnSetStandingOnLastFrameLayerToPlatforms();
            await Yield();
        }

        private async UniTask SetBelowRaycastOrigin()
        {
            Data.OnSetBelowRaycastOrigin(Index);
            await Yield();
        }

        private LayerMask BelowPlatformsWithoutOneWay => layerMaskData.BelowPlatformsWithoutOneWay;

        private async UniTask SetBelowRaycastToBelowPlatformsWithoutOneWay()
        {
            Data.OnSetBelowRaycastToBelowPlatformsWithoutOneWay(Index, character, BelowPlatformsWithoutOneWay);
            await Yield();
        }

        private async UniTask SetBelowRaycastToBelowPlatforms()
        {
            Data.OnSetBelowRaycastToBelowPlatforms(Index, character, BelowPlatforms);
            await Yield();
        }

        private async UniTask SetBelowRaycastDistance()
        {
            Data.OnSetBelowRaycastDistance(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask SetCollisionOnBelowRaycastHit()
        {
            Data.OnSetCollisionOnBelowRaycastHit(Index, character);
            await Yield();
        }

        private async UniTask SetNegativeBelowSlopeAngle()
        {
            Data.OnSetNegativeBelowSlopeAngle();
            await Yield();
        }

        private async UniTask SetStandingOnOnSmallestHitConnected()
        {
            Data.OnSetStandingOnOnSmallestHitConnected(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask SetCollidingBelow()
        {
            Data.OnSetCollidingBelow();
            await Yield();
        }

        private async UniTask SetBelowRaycastDistanceOnSmallestDistanceHit()
        {
            Data.OnSetBelowRaycastDistanceOnSmallestDistanceHit(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask SetCollisionOnDetachFromMovingPlatform()
        {
            Data.OnSetCollisionOnDetachFromMovingPlatform();
            await Yield();
        }

        private float MaximumSlopeAngle => physicsData.MaximumSlopeAngle;

        private async UniTask SetStickToSlopeRayLength()
        {
            Data.OnSetStickToSlopeRayLength(MaximumSlopeAngle);
            await Yield();
        }

        private async UniTask SetStickToSlopesRaycast()
        {
            Data.OnSetStickToSlopeRaycast(NewPosition.x, character, BelowPlatforms);
            await Yield();
        }

        private async UniTask SetNegativeBelowSlopeAngleLeft()
        {
            Data.OnSetNegativeBelowSlopeAngleLeft();
            await Yield();
        }

        private async UniTask SetNegativeBelowSlopeAngleRight()
        {
            Data.OnSetNegativeBelowSlopeAngleRight();
            await Yield();
        }

        private async UniTask SetCastStickToSlopeRaycastLeft()
        {
            Data.OnSetCastStickToSlopeRaycastLeft();
            await Yield();
        }

        private async UniTask SetStickToSlopeRaycastOnSlope()
        {
            Data.OnSetStickToSlopeRaycastOnSlope();
            await Yield();
        }

        private async UniTask SetStickToSlopeRaycastOnRightSlopeOnLeftGround()
        {
            Data.OnSetStickToSlopeRaycastOnRightSlopeOnLeftGround();
            await Yield();
        }

        private async UniTask SetStickToSlopeRaycastOnLeftSlopeOnRightGround()
        {
            Data.OnSetStickToSlopeRaycastOnLeftSlopeOnRightGround();
            await Yield();
        }

        private async UniTask SetStickToSlopeRaycastOnSlopes()
        {
            Data.OnSetStickToSlopeRaycastOnSlopes();
            await Yield();
        }

        private async UniTask SetStickToSlopeRaycastOnMaximumAngle()
        {
            Data.OnSetStickToSlopeRaycastOnMaximumAngle(character, BelowPlatforms);
            await Yield();
        }

        private async UniTask SetCollisionOnStickToSlopeRaycastHit()
        {
            Data.OnSetCollisionOnStickToSlopeRaycastHit();
            await Yield();
        }

        private async UniTask UpdateStickToSlopeRaycast()
        {
            Data.OnUpdateStickToSlopeRaycast();
            await Yield();
        }

        private async UniTask SetCurrentRaycastDirectionToUp()
        {
            Data.OnSetCurrentRaycastDirectionToUp();
            await Yield();
        }

        private async UniTask SetAboveRaycast()
        {
            Data.OnSetAboveRaycast(NewPosition, character);
            await Yield();
        }

        private async UniTask ResizeAboveHitStorage()
        {
            Data.OnResizeAboveHitStorage();
            await Yield();
        }

        private async UniTask UpdateAboveRaycast()
        {
            Data.OnUpdateAboveRaycast(Index, character, Platform & ~OneWayPlatform & ~MovingOneWayPlatform);
            await Yield();
        }

        private async UniTask SetCollisionOnAboveRaycastSmallestDistanceHit()
        {
            Data.OnSetCollisionOnAboveRaycastSmallestDistanceHit();
            await Yield();
        }

        private async UniTask SetStandingOnColliderContainsBottomCenterPosition()
        {
            Data.OnSetStandingOnColliderContainsBottomCenterPosition();
            await Yield();
        }

        private async UniTask SetStandingOnLastFrameNotNull()
        {
            Data.OnSetStandingOnLastFrameNotNull();
            await Yield();
        }

        #endregion

        #region event handlers

        public async UniTask OnPlatformerInitializeFrame()
        {
            await InitializeFrame();
        }

        public async UniTask OnPlatformerUpdateRaycastBounds()
        {
            await UpdateBounds();
        }

        public async UniTask OnPlatformerPerformSafetyBoxcast()
        {
            await PerformSafetyBoxcast();
        }

        public async UniTask OnPlatformerSetGroundedEvent()
        {
            await SetGroundedEvent();
        }

        public async UniTask OnPlatformerSetDistanceToGroundRaycast()
        {
            await SetDistanceToGroundRaycast();
        }

        public async UniTask OnPlatformerSetDistanceToGroundOnRaycastHit()
        {
            await SetDistanceToGroundOnRaycastHit();
        }

        public async UniTask OnPlatformerSetDistanceToGround()
        {
            await SetDistanceToGround();
        }

        public async UniTask OnPlatformerSetStandingOnLastFrameLayerToSavedBelow()
        {
            await SetStandingOnLastFrameLayerToSavedBelow();
        }

        public async UniTask OnPlatformerApplyMovingPlatformBehavior()
        {
            await ApplyMovingPlatformBehavior();
        }

        public async UniTask OnPlatformerSetCurrentRaycastDirectionToLeft()
        {
            await SetCurrentRaycastDirectionToLeft();
        }

        public async UniTask OnPlatformerSetCurrentRaycastDirectionToRight()
        {
            await SetCurrentRaycastDirectionToRight();
        }

        public async UniTask OnPlatformerSetHorizontalRaycast()
        {
            await SetHorizontalRaycast();
        }

        public async UniTask OnPlatformerResizeHorizontalHitStorage()
        {
            await ResizeHorizontalHitStorage();
        }

        public async UniTask OnPlatformerSetHorizontalRaycastOrigin()
        {
            await SetHorizontalRaycastOrigin();
        }

        public async UniTask OnPlatformerSetHorizontalRaycastForPlatform()
        {
            await SetHorizontalRaycastForPlatform();
        }

        public async UniTask OnPlatformerSetHorizontalRaycastForSpecialPlatforms()
        {
            await SetHorizontalRaycastForSpecialPlatforms();
        }

        public async UniTask OnPlatformerSetHorizontalHitAngle()
        {
            await SetHorizontalHitAngle();
        }

        public async UniTask OnPlatformerSetLateralSlopeAngle()
        {
            await SetLateralSlopeAngle();
        }

        public async UniTask OnPlatformerSetCollisionOnHitWall()
        {
            await SetCollisionOnHitWall();
        }

        public async UniTask OnPlatformerSetCollisionOnHitWallInMovementDirection()
        {
            await SetCollisionOnHitWallInMovementDirection();
        }

        public async UniTask OnPlatformerFrictionTest()
        {
            await FrictionTest();
        }

        public async UniTask OnPlatformerSetFriction()
        {
            await SetFriction();
        }

        public async UniTask OnPlatformerMovingPlatformTest()
        {
            await MovingPlatformTest();
        }

        public async UniTask OnPlatformerAddHorizontalContact()
        {
            await AddHorizontalContact();
        }

        public async UniTask OnPlatformerSetCurrentRaycastDirectionToDown()
        {
            await SetCurrentRaycastDirectionToDown();
        }

        public async UniTask OnPlatformerInitializeFriction()
        {
            await InitializeFriction();
        }

        public async UniTask OnPlatformerSetNotCollidingBelow()
        {
            await SetNotCollidingBelow();
        }

        public async UniTask OnPlatformerSetVerticalRaycastLength()
        {
            await SetVerticalRaycastLength();
        }

        public async UniTask OnPlatformerSetVerticalRaycastLengthOnMovingPlatform()
        {
            await SetVerticalRaycastLengthOnMovingPlatform();
        }

        public async UniTask OnPlatformerApplyNewPositionYToVerticalRaycastLength()
        {
            await ApplyNewPositionYToVerticalRaycastLength();
        }

        public async UniTask OnPlatformerSetVerticalRaycast()
        {
            await SetVerticalRaycast();
        }

        public async UniTask OnPlatformerResizeBelowHitStorage()
        {
            await ResizeBelowHitStorage();
        }

        public async UniTask OnPlatformerSetStandingOnLastFrameLayerToPlatforms()
        {
            await SetStandingOnLastFrameLayerToPlatforms();
        }

        public async UniTask OnPlatformerSetBelowRaycastOrigin()
        {
            await SetBelowRaycastOrigin();
        }

        public async UniTask OnPlatformerSetBelowRaycastToBelowPlatformsWithoutOneWay()
        {
            await SetBelowRaycastToBelowPlatformsWithoutOneWay();
        }

        public async UniTask OnPlatformerSetBelowRaycastToBelowPlatforms()
        {
            await SetBelowRaycastToBelowPlatforms();
        }

        public async UniTask OnPlatformerSetBelowRaycastDistance()
        {
            await SetBelowRaycastDistance();
        }

        public async UniTask OnPlatformerSetCollisionOnBelowRaycastHit()
        {
            await SetCollisionOnBelowRaycastHit();
        }

        public async UniTask OnPlatformerSetNegativeBelowSlopeAngle()
        {
            await SetNegativeBelowSlopeAngle();
        }

        public async UniTask OnPlatformerSetStandingOnOnSmallestHitConnected()
        {
            await SetStandingOnOnSmallestHitConnected();
        }

        public async UniTask OnPlatformerSetCollidingBelow()
        {
            await SetCollidingBelow();
        }

        public async UniTask OnPlatformerSetBelowRaycastDistanceOnSmallestDistanceHit()
        {
            await SetBelowRaycastDistanceOnSmallestDistanceHit();
        }

        public async UniTask OnPlatformerSetCollisionOnDetachFromMovingPlatform()
        {
            await SetCollisionOnDetachFromMovingPlatform();
        }

        public async UniTask OnPlatformerSetStickToSlopeRayLength()
        {
            await SetStickToSlopeRayLength();
        }

        public async UniTask OnPlatformerSetStickToSlopesRaycast()
        {
            await SetStickToSlopesRaycast();
        }

        public async UniTask OnPlatformerSetNegativeBelowSlopeAngleLeft()
        {
            await SetNegativeBelowSlopeAngleLeft();
        }

        public async UniTask OnPlatformerSetNegativeBelowSlopeAngleRight()
        {
            await SetNegativeBelowSlopeAngleRight();
        }

        public async UniTask OnPlatformerSetCastStickToSlopeRaycastLeft()
        {
            await SetCastStickToSlopeRaycastLeft();
        }

        public async UniTask OnPlatformerSetStickToSlopeRaycastOnSlope()
        {
            await SetStickToSlopeRaycastOnSlope();
        }

        public async UniTask OnPlatformerSetStickToSlopeRaycastOnRightSlopeOnLeftGround()
        {
            await SetStickToSlopeRaycastOnRightSlopeOnLeftGround();
        }

        public async UniTask OnPlatformerSetStickToSlopeRaycastOnLeftSlopeOnRightGround()
        {
            await SetStickToSlopeRaycastOnLeftSlopeOnRightGround();
        }

        public async UniTask OnPlatformerSetStickToSlopeRaycastOnSlopes()
        {
            await SetStickToSlopeRaycastOnSlopes();
        }

        public async UniTask OnPlatformerSetStickToSlopeRaycastOnMaximumAngle()
        {
            await SetStickToSlopeRaycastOnMaximumAngle();
        }

        public async UniTask OnPlatformerSetCollisionOnStickToSlopeRaycastHit()
        {
            await SetCollisionOnStickToSlopeRaycastHit();
        }

        public async UniTask OnPlatformerUpdateStickToSlopeRaycast()
        {
            await UpdateStickToSlopeRaycast();
        }

        public async UniTask OnPlatformerSetCurrentRaycastDirectionToUp()
        {
            await SetCurrentRaycastDirectionToUp();
        }

        public async UniTask OnPlatformerSetAboveRaycast()
        {
            await SetAboveRaycast();
        }

        public async UniTask OnPlatformerResizeAboveHitStorage()
        {
            await ResizeAboveHitStorage();
        }

        public async UniTask OnPlatformerUpdateAboveRaycast()
        {
            await UpdateAboveRaycast();
        }

        public async UniTask OnPlatformerSetCollisionOnAboveRaycastSmallestDistanceHit()
        {
            await SetCollisionOnAboveRaycastSmallestDistanceHit();
        }

        public async UniTask OnPlatformerSetStandingOnColliderContainsBottomCenterPosition()
        {
            await SetStandingOnColliderContainsBottomCenterPosition();
        }

        public async UniTask OnPlatformerSetStandingOnLastFrameNotNull()
        {
            await SetStandingOnLastFrameNotNull();
        }

        #endregion
    }
}
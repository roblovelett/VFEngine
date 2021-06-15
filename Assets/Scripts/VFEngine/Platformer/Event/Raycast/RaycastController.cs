using Cysharp.Threading.Tasks;
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
    public class RaycastController : MonoBehaviour
    {
        #region events

        #endregion

        #region properties

        public RaycastData Data { get; private set; }

        #endregion

        #region fields

        [SerializeField] private GameObject character;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private RaycastSettings settings;
        private PlatformerController platformerController;
        private LayerMaskController layerMaskController;
        private PhysicsController physicsController;
        private LayerMaskData layerMaskData;
        private PhysicsData physicsData;
        private Data platformerData;

        #endregion

        #region initialization

        private bool SettingsIsNull => settings == null;
        private bool DataIsNull => Data == null;
        private bool InitializePlatformerController => platformerController == null;
        private bool InitializeLayerMaskController => layerMaskController == null;
        private bool InitializePhysicsController => physicsController == null;
        private void Initialize()
        {
            if (!character) character = GameObject.Find("Character");
            if (!boxCollider) boxCollider = GetComponent<BoxCollider2D>();
            if (SettingsIsNull) settings = CreateInstance<RaycastSettings>();
            if (InitializePlatformerController) platformerController = GetComponent<PlatformerController>();
            if (InitializeLayerMaskController) layerMaskController = GetComponent<LayerMaskController>();
            if (InitializePhysicsController) physicsController = GetComponent<PhysicsController>();
            if (DataIsNull) Data = CreateInstance<RaycastData>();
            Data.OnInitialize(ref boxCollider, ref character, settings);
        }

        private void Dependencies()
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
            Dependencies();
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

        private async UniTask GroundedEvent()
        {
            Data.OnSetGroundedEvent();
            await Yield();
        }

        private LayerMask BelowPlatforms => layerMaskData.BelowPlatforms;

        private async UniTask DistanceToGroundRaycast()
        {
            Data.OnSetDistanceToGroundRaycast(character, BelowPlatforms);
            await Yield();
        }

        private async UniTask SetDistanceToGroundOnRaycastHit()
        {
            Data.OnSetDistanceToGroundOnRaycastHit();
            await Yield();
        }

        private async UniTask DistanceToGround()
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

        private async UniTask CurrentRaycastDirectionToLeft()
        {
            Data.OnSetCurrentRaycastDirectionToLeft();
            await Yield();
        }

        private async UniTask CurrentRaycastDirectionToRight()
        {
            Data.OnSetCurrentRaycastDirectionToRight();
            await Yield();
        }

        private Vector2 Speed => physicsData.Speed;

        private async UniTask HorizontalRaycast()
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

        private async UniTask HorizontalRaycastOrigin()
        {
            Data.OnSetHorizontalRaycastOrigin(Index);
            await Yield();
        }

        private async UniTask HorizontalRaycastForPlatform()
        {
            Data.OnSetHorizontalRaycastForPlatform(Index, character, Platform);
            await Yield();
        }

        private LayerMask OneWayPlatform => layerMaskData.OneWayPlatform;
        private LayerMask MovingOneWayPlatform => layerMaskData.MovingOneWayPlatform;

        private async UniTask HorizontalRaycastForSpecialPlatforms()
        {
            Data.OnSetHorizontalRaycastForSpecialPlatforms(Index, character, Platform, OneWayPlatform,
                MovingOneWayPlatform);
            await Yield();
        }

        private async UniTask HorizontalHitAngle()
        {
            Data.OnSetHorizontalHitAngle(Index, character);
            await Yield();
        }

        private async UniTask LateralSlopeAngle()
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

        private async UniTask Friction()
        {
            Data.OnSetFriction();
            await Yield();
        }

        private async UniTask MovingPlatformTest()
        {
            Data.OnMovingPlatformTest(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask CurrentRaycastDirectionToDown()
        {
            Data.OnSetCurrentRaycastDirectionToDown();
            await Yield();
        }

        private async UniTask InitializeFriction()
        {
            Data.OnInitializeFriction();
            await Yield();
        }

        private async UniTask NotCollidingBelow()
        {
            Data.OnSetNotCollidingBelow();
            await Yield();
        }

        private async UniTask VerticalRaycastLength()
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

        private async UniTask VerticalRaycast()
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

        private async UniTask BelowRaycastOrigin()
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

        private async UniTask BelowRaycastToBelowPlatforms()
        {
            Data.OnSetBelowRaycastToBelowPlatforms(Index, character, BelowPlatforms);
            await Yield();
        }

        private async UniTask BelowRaycastDistance()
        {
            Data.OnSetBelowRaycastDistance(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask SetCollisionOnBelowRaycastHit()
        {
            Data.OnSetCollisionOnBelowRaycastHit(Index, character);
            await Yield();
        }

        private async UniTask NegativeBelowSlopeAngle()
        {
            Data.OnSetNegativeBelowSlopeAngle();
            await Yield();
        }

        private async UniTask SetStandingOnOnSmallestHitConnected()
        {
            Data.OnSetStandingOnOnSmallestHitConnected(SmallestDistanceIndex);
            await Yield();
        }

        private async UniTask CollidingBelow()
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

        private async UniTask StickToSlopeRayLength()
        {
            Data.OnSetStickToSlopeRayLength(MaximumSlopeAngle);
            await Yield();
        }

        private async UniTask StickToSlopesRaycast()
        {
            Data.OnSetStickToSlopeRaycast(NewPosition.x, character, BelowPlatforms);
            await Yield();
        }

        private async UniTask NegativeBelowSlopeAngleLeft()
        {
            Data.OnSetNegativeBelowSlopeAngleLeft();
            await Yield();
        }

        private async UniTask NegativeBelowSlopeAngleRight()
        {
            Data.OnSetNegativeBelowSlopeAngleRight();
            await Yield();
        }

        private async UniTask CastStickToSlopeRaycastLeft()
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

        private async UniTask CurrentRaycastDirectionToUp()
        {
            Data.OnSetCurrentRaycastDirectionToUp();
            await Yield();
        }

        private async UniTask AboveRaycast()
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
            await GroundedEvent();
        }

        public async UniTask OnPlatformerSetDistanceToGroundRaycast()
        {
            await DistanceToGroundRaycast();
        }

        public async UniTask OnPlatformerSetDistanceToGroundOnRaycastHit()
        {
            await SetDistanceToGroundOnRaycastHit();
        }

        public async UniTask OnPlatformerSetDistanceToGround()
        {
            await DistanceToGround();
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
            await CurrentRaycastDirectionToLeft();
        }

        public async UniTask OnPlatformerSetCurrentRaycastDirectionToRight()
        {
            await CurrentRaycastDirectionToRight();
        }

        public async UniTask OnPlatformerSetHorizontalRaycast()
        {
            await HorizontalRaycast();
        }

        public async UniTask OnPlatformerResizeHorizontalHitStorage()
        {
            await ResizeHorizontalHitStorage();
        }

        public async UniTask OnPlatformerSetHorizontalRaycastOrigin()
        {
            await HorizontalRaycastOrigin();
        }

        public async UniTask OnPlatformerSetHorizontalRaycastForPlatform()
        {
            await HorizontalRaycastForPlatform();
        }

        public async UniTask OnPlatformerSetHorizontalRaycastForSpecialPlatforms()
        {
            await HorizontalRaycastForSpecialPlatforms();
        }

        public async UniTask OnPlatformerSetHorizontalHitAngle()
        {
            await HorizontalHitAngle();
        }

        public async UniTask OnPlatformerSetLateralSlopeAngle()
        {
            await LateralSlopeAngle();
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
            await Friction();
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
            await CurrentRaycastDirectionToDown();
        }

        public async UniTask OnPlatformerInitializeFriction()
        {
            await InitializeFriction();
        }

        public async UniTask OnPlatformerSetNotCollidingBelow()
        {
            await NotCollidingBelow();
        }

        public async UniTask OnPlatformerSetVerticalRaycastLength()
        {
            await VerticalRaycastLength();
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
            await VerticalRaycast();
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
            await BelowRaycastOrigin();
        }

        public async UniTask OnPlatformerSetBelowRaycastToBelowPlatformsWithoutOneWay()
        {
            await SetBelowRaycastToBelowPlatformsWithoutOneWay();
        }

        public async UniTask OnPlatformerSetBelowRaycastToBelowPlatforms()
        {
            await BelowRaycastToBelowPlatforms();
        }

        public async UniTask OnPlatformerSetBelowRaycastDistance()
        {
            await BelowRaycastDistance();
        }

        public async UniTask OnPlatformerSetCollisionOnBelowRaycastHit()
        {
            await SetCollisionOnBelowRaycastHit();
        }

        public async UniTask OnPlatformerSetNegativeBelowSlopeAngle()
        {
            await NegativeBelowSlopeAngle();
        }

        public async UniTask OnPlatformerSetStandingOnOnSmallestHitConnected()
        {
            await SetStandingOnOnSmallestHitConnected();
        }

        public async UniTask OnPlatformerSetCollidingBelow()
        {
            await CollidingBelow();
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
            await StickToSlopeRayLength();
        }

        public async UniTask OnPlatformerSetStickToSlopesRaycast()
        {
            await StickToSlopesRaycast();
        }

        public async UniTask OnPlatformerSetNegativeBelowSlopeAngleLeft()
        {
            await NegativeBelowSlopeAngleLeft();
        }

        public async UniTask OnPlatformerSetNegativeBelowSlopeAngleRight()
        {
            await NegativeBelowSlopeAngleRight();
        }

        public async UniTask OnPlatformerSetCastStickToSlopeRaycastLeft()
        {
            await CastStickToSlopeRaycastLeft();
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
            await CurrentRaycastDirectionToUp();
        }

        public async UniTask OnPlatformerSetAboveRaycast()
        {
            await AboveRaycast();
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
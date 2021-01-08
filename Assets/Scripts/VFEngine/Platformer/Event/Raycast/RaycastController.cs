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
    using static RaycastData.RaycastHitType;

    public class RaycastController : SerializedMonoBehaviour
    {
        #region events

        public BetterEvent groundCollision;

        #endregion

        #region properties

        [OdinSerialize] public RaycastData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private new BoxCollider2D collider;
        [OdinSerialize] private RaycastSettings settings;
        private LayerMaskData layerMaskData;
        private PhysicsData physicsData;
        private PlatformerData platformerData;

        #endregion

        #region initialization

        private void Initialize()
        {
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
                OnGroundCollision(i, DownOrigin, DownHit);
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

        private void OnGroundCollision(int index, Vector2 origin, RaycastHit2D hit)
        {
            Data.SetIndex(index);
            Data.SetOrigin(origin);
            Data.SetHit(hit);
        }

        private static void CastRay(Vector2 start, Vector2 direction, Color color)
        {
            DrawRay(start, direction, color);
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
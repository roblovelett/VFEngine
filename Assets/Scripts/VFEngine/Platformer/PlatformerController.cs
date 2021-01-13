using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.ScriptableObjects;
using VFEngine.Tools.BetterEvent;

namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : SerializedMonoBehaviour
    {
        #region events

        /*
        public BetterEvent initializeFrame;
        public BetterEvent groundCollision;
        public BetterEvent setForces;
        public BetterEvent setSlopeBehavior;
        public BetterEvent horizontalCollision;
        public BetterEvent verticalCollision;
        public BetterEvent slopeChangeCollision;
        public BetterEvent castRayFromInitialPosition;
        public BetterEvent translateDeltaMove;
        public BetterEvent resetJumpCollision;
        public BetterEvent setLayerMaskToSaved;
        public BetterEvent resetFriction;
        */

        #endregion

        #region properties

        [OdinSerialize] public PlatformerData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private PlatformerSettings settings;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            if (!Data) Data = CreateInstance<PlatformerData>();
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            Run();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private void Run()
        {
            InitializeFrame();
            GroundCollision();
            UpdateForces();
            SlopeCollision();
            CastRayTowardsMovement();
            Move();
            ResetJumpCollision();
            OnFrameExit();
        }

        private void InitializeFrame()
        {
            InitializeRaycast();
            InitializeLayerMask();
        }

        private void InitializeRaycast()
        {
            /*
            initializeRaycastForPlatformer.Invoke();
            -resetRaycastCollision;
            -updateRaycastBounds;
            */
        }

        private void InitializeLayerMask()
        {
            /*
            initializeLayerMaskForPlatformer.Invoke();
            -setSavedLayer;
            -setLayerToIgnoreRaycast;
            */
        }

        private void GroundCollision()
        {
            /*
            setRaycastForGroundCollision.Invoke();
            // if (setRaycastForGroundCollisionOneWayPlatform)
                setRaycastForGroundCollisionOneWayPlatform.Invoke();
            // if (hit)
                setRaycastForGroundCollisionHit.Invoke();
            */
        }

        private void UpdateForces()
        {
            /*
            updateForces.Invoke();
            //if (applyForcesToExternalForce)
                applyForcesToExternalForce.Invoke();
            */
        }

        private void SlopeCollision()
        {
            /*
            // if moving horizontally
                // if descendingSlope
                    DescendSlope();
                // else
                    ClimbSlope();
                HorizontalCollision();
            StopSpeedControl();
            VerticalCollision();
            SlopeChangeCollision();
            */
        }

        private void DescendSlope()
        {
            /*
            setPhysicsOnDescendSlope.Invoke();
            setRaycastCollisionOnDescendSlope.Invoke();
            */
        }

        private void ClimbSlope()
        {
            //
        }

        private void HorizontalCollision()
        {
            //
        }

        private void StopSpeedControl()
        {
            //
        }

        private void VerticalCollision()
        {
            //
        }

        private void SlopeChangeCollision()
        {
           // 
        }

        private void CastRayTowardsMovement()
        {
            //
        }

        private void Move()
        {
            //
        }

        private void ResetJumpCollision()
        {
            //
        }

        private void OnFrameExit()
        {
            /*
            setLayerMaskToSaved.Invoke();
            setRaycastFrictionCollisionDetection.Invoke();
            */
        }
        
        
        #endregion

        #region event handlers

        #endregion
    }
}
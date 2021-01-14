using Packages.BetterEvent;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Layer.Mask;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;
using VFEngine.Platformer.Physics;
using VFEngine.Platformer.Physics.ScriptableObjects;
using VFEngine.Platformer.ScriptableObjects;
using static VFEngine.Platformer.Event.Raycast.ScriptableObjects.RaycastData;
using static VFEngine.Platformer.Layer.Mask.ScriptableObjects.LayerMaskData;
using static VFEngine.Platformer.Physics.ScriptableObjects.PhysicsData;

namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : SerializedMonoBehaviour
    {
        #region events

        public BetterEvent initializeFrame;
        public BetterEvent groundCollisionRaycast;
        public BetterEvent updateForces;
        /*
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
        [OdinSerialize] private RaycastController raycastController;
        [OdinSerialize] private LayerMaskController layerMaskController;
        [OdinSerialize] private PhysicsController physicsController;
        private RaycastData raycastData;
        private PhysicsData physicsData;
        private LayerMaskData layerMaskData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!settings) settings = CreateInstance<PlatformerSettings>();
            if (!raycastController) raycastController = GetComponent<RaycastController>();
            if (!layerMaskController) layerMaskController = GetComponent<LayerMaskController>();
            if (!physicsController) physicsController = GetComponent<PhysicsController>();
            if (!Data) Data = CreateInstance<PlatformerData>();
            Data.OnInitialize();
        }

        private void SetDependencies()
        {
            raycastData = raycastController.Data;
            physicsData = physicsController.Data;
            layerMaskData = layerMaskController.Data;
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
        }

        private void InitializeFrame()
        {
            initializeFrame.Invoke();
        }

        private bool RaycastInitializedFrame => raycastData.State == RaycastState.PlatformerInitializedFrame;
        private bool LayerMaskInitializedFrame => layerMaskData.State == LayerMaskState.PlatformerInitializedFrame;
        private bool PhysicsInitializedFrame => physicsData.State == PhysicsState.PlatformerInitializedFrame;

        private bool StartGroundCollision =>
            RaycastInitializedFrame && LayerMaskInitializedFrame && PhysicsInitializedFrame;

        private void OnStartGroundCollision()
        {
            if (StartGroundCollision) GroundCollision();
        }

        private void GroundCollision()
        {
            groundCollisionRaycast.Invoke();
        }

        private void UpdateForces()
        {
            updateForces.Invoke();
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

        public void OnRaycastInitializedFrameForPlatformer()
        {
            OnStartGroundCollision();
        }

        public void OnLayerMaskInitializedFrameForPlatformer()
        {
            OnStartGroundCollision();
        }

        public void OnPhysicsInitializedFrameForPlatformer()
        {
            OnStartGroundCollision();
        }

        public void OnCastedGroundCollisionRaycastForPlatformer()
        {
            UpdateForces();
        }

        #endregion
    }
}
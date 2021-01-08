using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace VFEngine.Platformer.Physics
{
    using static ScriptableObject;
    using static GameObject;
    using static Mathf;

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

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<PhysicsSettings>();
            if (!Data) Data = CreateInstance<PhysicsData>();
            Data.Initialize(settings);
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            // set dependencies
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

        #endregion

        #region event handlers

        public void OnPlatformerInitializeFrame()
        {
            StartCoroutine(InitializeFrame());
        }

        #endregion
    }
}

#region hide

/*public void OnPlatformerMoveExternalForce()
        {
            Physics.MoveExternalForceTowards();
        }

        public void OnPlatformerApplyGravity()
        {
            Physics.ApplyGravity();
        }

        public void OnPlatformerApplyForcesToExternal()
        {
            Physics.ApplyForcesToExternal();
        }

        public void OnPlatformerDescendSlope()
        {
            Physics.DescendSlope();
        }

        public void OnPlatformerClimbSlope()
        {
            Physics.OnClimbSlope();
        }

        public void OnPlatformerOnFirstSideHit()
        {
            Physics.OnFirstSideHit();
        }

        public void OnPlatformerOnSideHit()
        {
            Physics.OnSideHit();
        }

        public void OnPlatformerStopVerticalMovement()
        {
            Physics.StopVerticalMovement();
        }

        public void OnPlatformerAdjustVerticalMovementToSlope()
        {
            Physics.OnAdjustVerticalMovementToSlope();
        }

        public void OnPlatformerHitWall()
        {
            Physics.OnHitWall();
        }

        public void OnPlatformerStopHorizontalSpeed()
        {
            Physics.StopHorizontalSpeed();
        }

        public void OnPlatformerVerticalHit()
        {
            Physics.OnVerticalHit();
        }

        public void OnPlatformerApplyGroundAngle()
        {
            Physics.OnPlatformerApplyGroundAngle();
        }

        public void OnPlatformerClimbSteepSlope()
        {
            Physics.OnClimbSteepSlope();
        }

        public void OnPlatformerClimbMildSlope()
        {
            Physics.OnClimbMildSlope();
        }

        public void OnPlatformerDescendMildSlope()
        {
            Physics.OnDescendMildSlope();
        }

        public void OnPlatformerDescendSteepSlope()
        {
            Physics.OnDescendSteepSlope();
        }

        public void OnPlatformerTranslateMovement()
        {
            Physics.OnTranslateMovement();
        }

        public void OnPlatformerCeilingOrGroundCollision()
        {
            Physics.OnCeilingOrGroundCollision();
        }

        public void OnPlatformerResetFriction()
        {
            Physics.ResetFriction();
        }*/

#endregion
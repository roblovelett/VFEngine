using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Tools.BetterEvent;

namespace VFEngine.Platformer
{
    using static ScriptableObject;

    public class PlatformerController : SerializedMonoBehaviour
    {
        #region events

        public BetterEvent initializeFrame;
        public BetterEvent groundCollision;
        public BetterEvent setForces;
        public BetterEvent setSlopeBehavior;
        public BetterEvent horizontalCollision;
        public BetterEvent verticalCollision;
        public BetterEvent slopeChangeCollision;

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
            initializeFrame.Invoke();
            groundCollision.Invoke();
            setForces.Invoke();
            setSlopeBehavior.Invoke();
            horizontalCollision.Invoke();
            verticalCollision.Invoke();
            slopeChangeCollision.Invoke();
            /*
            CastRayFromInitialPosition();
            TranslateMovement();
            OnCeilingOrGroundCollision();
            SetLayerMaskToSaved();
            ResetFriction();
            */
        }

        #endregion

        #region event handlers

        #endregion
    }
}
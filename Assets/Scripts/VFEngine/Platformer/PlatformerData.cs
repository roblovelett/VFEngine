// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace VFEngine.Platformer
{
    public struct PlatformerData
    {
        #region fields

        #region private methods

        #region initialization

        private void InitializeDependencies(PlatformerSettings settings)
        {
            InitializeSettings(settings);
        }

        private void InitializeSettings(PlatformerSettings settings)
        {
            DisplayWarnings = settings.displayWarnings;
            OneWayPlatformDelay = settings.oneWayPlatformDelay;
            LadderClimbThreshold = settings.ladderClimbThreshold;
            LadderDelay = settings.ladderDelay;
        }

        private void Initialize()
        {
            Index = 0;
            Tolerance = 0;
            IgnorePlatformsTime = 0;
            ClimbedSteepSlope = false;
        }

        #endregion

        #region private methods

        private void SetClimbedSteepSlope(bool climbed)
        {
            ClimbedSteepSlope = climbed;
        }

        #endregion

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarnings { get; private set; }
        public float OneWayPlatformDelay { get; private set; }
        public float LadderClimbThreshold { get; private set; }
        public float LadderDelay { get; private set; }

        #endregion

        public int Index { get; private set; }
        public float Tolerance { get; private set; }
        public float IgnorePlatformsTime { get; private set; }
        public bool ClimbedSteepSlope { get; private set; }

        #region public methods

        #region constructors

        public PlatformerData(PlatformerSettings settings) : this()
        {
            InitializeDependencies(settings);
            Initialize();
        }

        #endregion

        public void OnInitializeFrame()
        {
            Initialize();
        }

        public void SetIndex(int index)
        {
            Index = index;
        }

        public void OnApplySteepSlopeBehavior()
        {
            SetClimbedSteepSlope(true);
        }

        #endregion

        #endregion
    }
}
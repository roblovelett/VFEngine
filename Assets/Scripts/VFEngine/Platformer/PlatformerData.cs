// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer
{
    public struct PlatformerData
    {
        #region fields

        #region private methods

        private void InitializeDependencies(PlatformerSettings settings)
        {
            DisplayWarnings = settings.displayWarnings;
            OneWayPlatformDelay = settings.oneWayPlatformDelay;
            LadderClimbThreshold = settings.ladderClimbThreshold;
            LadderDelay = settings.ladderDelay;
        }

        private void Initialize()
        {
            InitializeInternal();
        }

        private void InitializeInternal()
        {
            Tolerance = 0;
            IgnorePlatformsTime = 0;
        }

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarnings { get; private set; }
        public float OneWayPlatformDelay { get; private set; }
        public float LadderClimbThreshold { get; private set; }
        public float LadderDelay { get; private set; }

        #endregion

        public float Tolerance { get; private set; }
        public float IgnorePlatformsTime { get; private set; }

        #region public methods

        #region constructors

        public PlatformerData(PlatformerSettings settings) : this()
        {
            InitializeDependencies(settings);
            Initialize();
        }

        #endregion

        #endregion

        #endregion
    }
}
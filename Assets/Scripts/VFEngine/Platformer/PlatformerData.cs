namespace VFEngine.Platformer
{
    public struct PlatformerData
    {
        #region fields

        #region private methods

        private void Initialize()
        {
            Tolerance = 0;
            IgnorePlatformsTime = 0;
        }

        private void InitializeDependencies(PlatformerSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            OneWayPlatformDelay = settings.oneWayPlatformDelay;
            LadderClimbThreshold = settings.ladderClimbThreshold;
            LadderDelay = settings.ladderDelay;
        }

        private void InitializeDependencies()
        {
            DisplayWarningsControl = false;
            OneWayPlatformDelay = 0;
            LadderClimbThreshold = 0;
            LadderDelay = 0;
        }

        #endregion

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; private set; }
        public float OneWayPlatformDelay { get; private set; }
        public float LadderClimbThreshold { get; private set; }
        public float LadderDelay { get; private set; }

        #endregion

        public float Tolerance { get; private set; }
        public float IgnorePlatformsTime { get; private set; }

        public PlatformerData(PlatformerSettings settings) : this()
        {
            if (settings) InitializeDependencies(settings);
            else InitializeDependencies();
            Initialize();
        }

        #endregion
    }
}
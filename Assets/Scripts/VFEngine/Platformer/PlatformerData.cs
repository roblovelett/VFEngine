namespace VFEngine.Platformer
{
    public class PlatformerData
    {
        #region fields

        #region private methods

        private void ApplySettings(PlatformerSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            OneWayPlatformDelay = settings.oneWayPlatformDelay;
            LadderClimbThreshold = settings.ladderClimbThreshold;
            LadderDelay = settings.ladderDelay;
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

        public float Tolerance { get; set; }
        public float IgnorePlatformsTime { get; set; }

        #region public methods

        public void InitializeData()
        {
            Tolerance = 0;
            IgnorePlatformsTime = 0;
        }

        public void Initialize(PlatformerSettings settings)
        {
            ApplySettings(settings);
        }

        #endregion

        #endregion
    }
}
namespace VFEngine.Platformer
{
    public class PlatformerData
    {
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

        public void ApplySettings(PlatformerSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            OneWayPlatformDelay = settings.oneWayPlatformDelay;
            LadderClimbThreshold = settings.ladderClimbThreshold;
            LadderDelay = settings.ladderDelay;
        }

        public void Initialize()
        {
            Tolerance = 0;
            IgnorePlatformsTime = 0;
        }

        #endregion

        #endregion
    }
}
namespace VFEngine.Platformer
{
    public class PlatformerData
    {
        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; private set; }

        #endregion

        public float Tolerance { get; } = 0f;

        #region public methods

        public void ApplySettings(PlatformerSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
        }

        #endregion

        #endregion
    }
}
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    public class StickyRaycastData
    {
        #region properties

        #region dependencies

        public bool DisplayWarningsControl { get; private set; }
        public float StickToSlopesOffsetY { get; private set; }

        #endregion

        public bool IsCastingLeft { get; set; }
        public float StickyRaycastLength { get; set; }

        #region public methods

        public void ApplySettings(StickyRaycastSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
            StickToSlopesOffsetY = settings.stickToSlopesOffsetY;
        }

        #endregion

        #endregion
    }
}
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    public class StickyRaycastData
    {
        #region properties

        #region dependencies

        public bool StickToSlopesControl { get; set; }
        public bool DisplayWarningsControl { get; set; }
        public float StickyRaycastLength { get; set; }
        public float StickToSlopesOffsetY { get; set; }
        

        #endregion

        public bool IsCastingLeft { get; set; }

        #region public methods

        public void ApplySettings(StickyRaycastSettings settings)
        {
            StickToSlopesControl = settings.stickToSlopeControl;
            DisplayWarningsControl = settings.displayWarningsControl;
            StickyRaycastLength = settings.stickyRaycastLength;
            StickToSlopesOffsetY = settings.stickToSlopesOffsetY;
        }

        #endregion

        #endregion
    }
}
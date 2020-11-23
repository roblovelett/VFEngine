namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    public class StickyRaycastRuntimeData
    {
        #region properties

        public bool IsCastingLeft { get; private set; }
        public float StickToSlopesOffsetY { get; private set; }
        public float StickyRaycastLength { get; private set; }

        #region public methods

        public static StickyRaycastRuntimeData CreateInstance(bool isCastingLeft, float stickToSlopesOffsetY,
            float stickyRaycastLength)
        {
            return new StickyRaycastRuntimeData
            {
                IsCastingLeft = isCastingLeft,
                StickToSlopesOffsetY = stickToSlopesOffsetY,
                StickyRaycastLength = stickyRaycastLength
            };
        }

        #endregion

        #endregion
    }
}
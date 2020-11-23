namespace VFEngine.Platformer.Event.Boxcast
{
    public class BoxcastRuntimeData
    {
        #region properties

        public BoxcastController Controller { get; set; }

        #region public methods

        public static BoxcastRuntimeData CreateInstance(BoxcastController controller)
        {
            return new BoxcastRuntimeData {Controller = controller};
        }

        #endregion

        #endregion
    }
}
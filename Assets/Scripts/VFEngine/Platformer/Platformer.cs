namespace VFEngine.Platformer
{
    public class Platformer
    {
        #region properties

        public PlatformerData data;

        #region public methods

        public Platformer(PlatformerSettings settings)
        {
            data = new PlatformerData(settings);
        }

        public Platformer()
        {
            data = new PlatformerData();
        }
        
        #endregion

        #endregion
    }
}
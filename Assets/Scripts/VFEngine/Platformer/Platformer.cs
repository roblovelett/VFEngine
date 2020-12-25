namespace VFEngine.Platformer
{
    public class Platformer
    {
        #region properties

        public PlatformerData Data { get; set; }

        #region public methods

        public Platformer(PlatformerSettings settings)
        {
            Data = new PlatformerData(settings);
        }

        public Platformer()
        {
            Data = new PlatformerData();
        }
        
        #endregion

        #endregion
    }
}
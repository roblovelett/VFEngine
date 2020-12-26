namespace VFEngine.Platformer
{
    public class PlatformerModel
    {
        #region properties

        public PlatformerData Data { get; }

        #region public methods

        public PlatformerModel(PlatformerSettings settings)
        {
            Data = new PlatformerData(settings);
        }

        public PlatformerModel()
        {
            Data = new PlatformerData();
        }
        
        #endregion

        #endregion
    }
}
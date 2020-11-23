
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer
{
    public class PlatformerRuntimeData
    {
        #region properties

        public PlatformerController Controller { get; private set; }

        #region public methods

        public static PlatformerRuntimeData CreateInstance(PlatformerController controller)
        {
            return new PlatformerRuntimeData {Controller = controller};
        }

        #endregion

        #endregion
    }
}
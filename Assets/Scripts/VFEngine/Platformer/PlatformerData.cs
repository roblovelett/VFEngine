using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    public class PlatformerData
    {
        #region fields

        #region dependencies

        #endregion

        private const string ModelAssetPath = "PlatformerModel.asset";

        #endregion

        #region properties

        #region dependencies

        public PlatformerSettings Settings { get; private set; }
        public bool DisplayWarningsControl { get; private set; }

        #endregion

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #region public methods

        public void ApplySettings(PlatformerSettings settings)
        {
            Settings = settings;
            DisplayWarningsControl = settings.displayWarningsControl;
        }

        #endregion

        #endregion
    }
}
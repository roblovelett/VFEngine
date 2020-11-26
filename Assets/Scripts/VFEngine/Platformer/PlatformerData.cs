using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer
{
    using static ScriptableObjectExtensions;

    public class PlatformerData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerSettings settings;

        #endregion

        private const string ModelAssetPath = "PlatformerModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DisplayWarningsControl => settings.displayWarningsControl;
        public PlatformerSettings Settings => settings;

        #endregion

        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
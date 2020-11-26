using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast
{
    using static ScriptableObjectExtensions;

    public class BoxcastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private const string BcPath = "Event/Boxcast/";
        private static readonly string ModelAssetPath = $"{BcPath}BoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public static readonly string BoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Boxcast
{
    using static ScriptableObjectExtensions;

    public class BoxcastData
    {
        #region fields

        #region dependencies

        #endregion

        private const string BcPath = "Event/Boxcast/";
        private static readonly string ModelAssetPath = $"{BcPath}BoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character { get; set; }

        #endregion

        public BoxcastRuntimeData RuntimeData { get; set; }
        public BoxcastController Controller { get; set; }
        public static readonly string BoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
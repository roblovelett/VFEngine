using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Boxcast
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "BoxcastData", menuName = PlatformerBoxcastDataPath, order = 0)]
    [InlineEditor]
    public class BoxcastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;

        #endregion

        private const string BcPath = "Event/Boxcast/";
        private static readonly string ModelAssetPath = $"{BcPath}BoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;

        #endregion

        public BoxcastRuntimeData RuntimeData { get; set; }
        public static readonly string BoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;
    [CreateAssetMenu(fileName = "SafetyBoxcastData", menuName = PlatformerSafetyBoxcastDataPath, order = 0)]
    [InlineEditor]
    public class SafetyBoxcastData : ScriptableObject
    {
        #region fields

        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}SafetyBoxcastModel.asset";

        #endregion

        #region properties

        public RaycastHit2D SafetyBoxcastHit { get; set; }
        public static readonly string SafetyBoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
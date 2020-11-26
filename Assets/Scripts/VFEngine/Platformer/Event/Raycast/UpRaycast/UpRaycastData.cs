using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "UpRaycastData", menuName = PlatformerUpRaycastDataPath, order = 0)]
    [InlineEditor]
    public class UpRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string UpRaycastPath = $"{RaycastPath}UpRaycast/";
        private static readonly string ModelAssetPath = $"{UpRaycastPath}UpRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public float UpRayLength { get; set; }
        public Vector2 UpRaycastStart { get; set; } = Vector2.zero;
        public Vector2 UpRaycastEnd { get; set; } = Vector2.zero;
        public float UpRaycastSmallestDistance { get; set; }
        public Vector2 CurrentUpRaycastOrigin { get; set; }
        public RaycastHit2D CurrentUpRaycastHit { get; set; }
        public static readonly string UpRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
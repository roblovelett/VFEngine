using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static ScriptableObjectExtensions;
    using static StickyRaycastData;

    [CreateAssetMenu(fileName = "LeftStickyRaycastData", menuName = PlatformerLeftStickyRaycastDataPath, order = 0)]
    [InlineEditor]
    public class LeftStickyRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        #endregion

        private static readonly string LeftStickyRaycastPath = $"{StickyRaycastPath}LeftStickyRaycast/";
        private static readonly string ModelAssetPath = $"{LeftStickyRaycastPath}LeftRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public RaycastHit2D LeftStickyRaycastHit { get; set; }
        public float LeftStickyRaycastLength { get; set; }
        public Vector2 LeftStickyRaycastOrigin { get; } = Vector2.zero;

        public float LeftStickyRaycastOriginX
        {
            set => value = LeftStickyRaycastOrigin.x;
        }

        public float LeftStickyRaycastOriginY
        {
            set => value = LeftStickyRaycastOrigin.y;
        }

        public static readonly string LeftStickyRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
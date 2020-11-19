using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

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

        [SerializeField] private GameObject character;

        #endregion

        [SerializeField] private RaycastReference leftStickyRaycastHit = new RaycastReference();
        [SerializeField] private FloatReference leftStickyRaycastLength = new FloatReference();
        private static readonly string LeftStickyRaycastPath = $"{StickyRaycastPath}RightStickyRaycast/";
        private static readonly string ModelAssetPath = $"{LeftStickyRaycastPath}RightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public Transform Transform { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public float StickyRaycastLength { get; set; }
        public float BoundsWidth { get; set; }
        public float MaximumSlopeAngle { get; set; }
        public float BoundsHeight { get; set; }
        public float RayOffset { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public float LeftStickyRaycastLength
        {
            get => leftStickyRaycastLength.Value;
            set => value = leftStickyRaycastLength.Value;
        }

        public Vector2 LeftStickyRaycastOrigin { get; } = Vector2.zero;

        public float LeftStickyRaycastOriginX
        {
            set => value = LeftStickyRaycastOrigin.x;
        }

        public float LeftStickyRaycastOriginY
        {
            set => value = LeftStickyRaycastOrigin.y;
        }

        public RaycastHit2D LeftStickyRaycastHit
        {
            get => leftStickyRaycastHit.Value.hit2D;
            set => leftStickyRaycastHit.Value = new ScriptableObjects.Variables.Raycast(value);
        }

        public static readonly string LeftStickyRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
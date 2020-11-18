using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static RaycastData;

    [InlineEditor]
    [CreateAssetMenu(fileName = "StickyRaycastData", menuName = PlatformerStickyRaycastDataPath, order = 0)]
    public class StickyRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private StickyRaycastSettings settings = null;

        #endregion

        [SerializeField] private FloatReference stickToSlopesOffsetY = new FloatReference();
        [SerializeField] private BoolReference isCastingLeft = new BoolReference();
        [SerializeField] private FloatReference stickyRaycastLength = new FloatReference();
        private const string ModelAssetPath = "StickyRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool HasSettings => settings;
        public bool StickToSlopesControl { get; set; }
        public bool DisplayWarningsControl { get; set; }
        public float BelowSlopeAngle { get; set; }
        public float BoundsWidth { get; set; }
        public float MaximumSlopeAngle { get; set; }
        public float BoundsHeight { get; set; }
        public float RayOffset { get; set; }
        public float BelowSlopeAngleLeft { get; set; }
        public float BelowSlopeAngleRight { get; set; }
        public RaycastHit2D LeftStickyRaycastHit { get; set; }
        public RaycastHit2D RightStickyRaycastHit { get; set; }

        #endregion

        public float StickToSlopesOffsetY
        {
            get => stickToSlopesOffsetY.Value;
            set => value = stickToSlopesOffsetY.Value;
        }

        public float StickToSlopesOffsetYSetting => settings.stickToSlopesOffsetY;
        public bool DisplayWarningsControlSetting => settings.displayWarningsControl;

        public bool IsCastingLeft
        {
            get => isCastingLeft.Value;
            set => value = isCastingLeft.Value;
        }

        public float StickyRaycastLength
        {
            get => stickyRaycastLength.Value;
            set => value = stickyRaycastLength.Value;
        }

        public static string StickyRaycastPath { get; } = $"{RaycastPath}StickyRaycast/";

        public static readonly string StickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{StickyRaycastPath}{ModelAssetPath}";

        #endregion
    }
}
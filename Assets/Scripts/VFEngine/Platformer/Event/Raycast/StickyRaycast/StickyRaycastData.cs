using ScriptableObjects.Atoms.RaycastHit2D.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static RaycastData;

    public class StickyRaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private BoolReference displayWarningsControl;
        [SerializeField] private StickyRaycastSettings settings;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private RaycastHit2DReference leftStickyRaycast;
        [SerializeField] private RaycastHit2DReference rightStickyRaycast;
        [SerializeField] private FloatReference belowSlopeAngleLeft;
        [SerializeField] private FloatReference belowSlopeAngleRight;

        /* fields */
        [SerializeField] private FloatReference stickToSlopesOffsetY;
        [SerializeField] private BoolReference isCastingLeft;
        [SerializeField] private FloatReference stickyRaycastLength;
        private const string ModelAssetPath = "DefaultStickyRaycastModel.asset";

        /* properties: dependencies */
        public bool DisplayWarningsControl => displayWarningsControl.Value;
        public RaycastHit2D LeftStickyRaycast => leftStickyRaycast.Value;
        public RaycastHit2D RightStickyRaycast => rightStickyRaycast.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public float BelowSlopeAngleLeft => belowSlopeAngleLeft.Value;
        public float BelowSlopeAngleRight => belowSlopeAngleRight.Value;

        /* properties */
        public float StickToSlopesOffsetY
        {
            set => value = stickToSlopesOffsetY.Value;
        }

        public float StickToSlopesOffsetYSetting => settings.stickToSlopesOffsetY;

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

        public float BelowSlopeAngle { get; set; }
        public static string StickyRaycastPath { get; } = $"{RaycastPath}StickyRaycast/";

        public static readonly string StickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{StickyRaycastPath}{ModelAssetPath}";
    }
}
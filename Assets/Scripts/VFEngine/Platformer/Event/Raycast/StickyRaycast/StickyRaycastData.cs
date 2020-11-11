using ScriptableObjects.Atoms.Raycast.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static RaycastData;

    public class StickyRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference displayWarningsControl;
        [SerializeField] private BoolReference stickToSlopesControl;
        [SerializeField] private StickyRaycastSettings settings;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference maximumSlopeAngle;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private RaycastReference leftStickyRaycast;
        [SerializeField] private RaycastReference rightStickyRaycast;
        [SerializeField] private FloatReference belowSlopeAngleLeft;
        [SerializeField] private FloatReference belowSlopeAngleRight;
        [SerializeField] private FloatReference belowSlopeAngle;

        #endregion

        [SerializeField] private FloatReference stickToSlopesOffsetY;
        [SerializeField] private BoolReference isCastingLeft;
        [SerializeField] private FloatReference stickyRaycastLength;
        private const string ModelAssetPath = "DefaultStickyRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool HasSettings => settings;
        public bool StickToSlopesControl => stickToSlopesControl.Value;
        public bool DisplayWarningsControl => displayWarningsControl.Value;

        public RaycastHit2D LeftStickyRaycast
        {
            get
            {
                var r = leftStickyRaycast.Value;
                return r.hit2D;
            }
        }

        public RaycastHit2D RightStickyRaycast
        {
            get
            {
                var r = rightStickyRaycast.Value;
                return r.hit2D;
            }
        }

        public float BelowSlopeAngle => belowSlopeAngle.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public float BelowSlopeAngleLeft => belowSlopeAngleLeft.Value;
        public float BelowSlopeAngleRight => belowSlopeAngleRight.Value;

        #endregion

        public float StickToSlopesOffsetY
        {
            set => value = stickToSlopesOffsetY.Value;
        }

        public float StickToSlopesOffsetYSetting => settings.stickToSlopesOffsetY;

        public bool IsCastingLeft
        {
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
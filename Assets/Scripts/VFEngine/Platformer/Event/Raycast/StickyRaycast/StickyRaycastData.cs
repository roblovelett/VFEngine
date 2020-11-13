using ScriptableObjects.Atoms.Raycast.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    using static ScriptableObjectExtensions;
    using static RaycastData;

    public class StickyRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private StickyRaycastSettings settings = null;
        [SerializeField] private BoolReference displayWarningsControl = new BoolReference();
        [SerializeField] private BoolReference stickToSlopesControl = new BoolReference();
        [SerializeField] private FloatReference boundsWidth = new FloatReference();
        [SerializeField] private FloatReference maximumSlopeAngle = new FloatReference();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private RaycastReference leftStickyRaycast = new RaycastReference();
        [SerializeField] private RaycastReference rightStickyRaycast = new RaycastReference();
        [SerializeField] private FloatReference belowSlopeAngleLeft = new FloatReference();
        [SerializeField] private FloatReference belowSlopeAngleRight = new FloatReference();
        [SerializeField] private FloatReference belowSlopeAngle = new FloatReference();

        #endregion

        [SerializeField] private FloatReference stickToSlopesOffsetY = new FloatReference();
        [SerializeField] private BoolReference isCastingLeft = new BoolReference();
        [SerializeField] private FloatReference stickyRaycastLength = new FloatReference();
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
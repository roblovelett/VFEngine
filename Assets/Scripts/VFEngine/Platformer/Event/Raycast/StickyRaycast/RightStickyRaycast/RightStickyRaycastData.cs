using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.RightStickyRaycast
{
    using static ScriptableObjectExtensions;
    using static StickyRaycastData;

    [InlineEditor]
    public class RightStickyRaycastData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
/*
        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private FloatReference stickyRaycastLength = new FloatReference();
        [SerializeField] private FloatReference boundsWidth = new FloatReference();
        [SerializeField] private FloatReference maximumSlopeAngle = new FloatReference();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private Vector2Reference boundsBottomRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsCenter = new Vector2Reference();
        [SerializeField] private MaskReference raysBelowLayerMaskPlatforms = new MaskReference();
        [SerializeField] private new Transform transform = null;
*/

        #endregion

        [SerializeField] private RaycastReference rightStickyRaycastHit = new RaycastReference();
        [SerializeField] private FloatReference rightStickyRaycastLength = new FloatReference();
        private static readonly string RightStickyRaycastPath = $"{StickyRaycastPath}RightStickyRaycast/";
        private static readonly string ModelAssetPath = $"{RightStickyRaycastPath}RightRaycastModel.asset";

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
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public Vector2 RightStickyRaycastOrigin { get; } = Vector2.zero;

        public float RightStickyRaycastOriginX
        {
            set => value = RightStickyRaycastOrigin.x;
        }

        public float RightStickyRaycastOriginY
        {
            set => value = RightStickyRaycastOrigin.y;
        }

        public float RightStickyRaycastLength
        {
            get => rightStickyRaycastLength.Value;
            set => value = rightStickyRaycastLength.Value;
        }

        public RaycastHit2D RightStickyRaycastHit
        {
            get => rightStickyRaycastHit.Value.hit2D;
            set => rightStickyRaycastHit.Value = new ScriptableObjects.Variables.Raycast(value);
        }

        public static readonly string RightStickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
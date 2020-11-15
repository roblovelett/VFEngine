using ScriptableObjects.Atoms.Mask.References;
using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
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

        #endregion

        [SerializeField] private RaycastReference rightStickyRaycast = new RaycastReference();
        [SerializeField] private FloatReference rightStickyRaycastLength = new FloatReference();
        [SerializeField] private FloatReference rightStickyRaycastOriginY = new FloatReference();
        private static readonly string RightStickyRaycastPath = $"{StickyRaycastPath}RightStickyRaycast/";
        private static readonly string ModelAssetPath = $"{RightStickyRaycastPath}RightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value.layer;
        public Transform Transform => transform;

        #endregion

        public float RightStickyRaycastLength
        {
            get => rightStickyRaycastLength.Value;
            set => value = rightStickyRaycastLength.Value;
        }

        [HideInInspector] public Vector2 rightStickyRaycastOrigin;

        public float RightStickyRaycastOriginY
        {
            set => value = rightStickyRaycastOriginY.Value;
        }

        public ScriptableObjects.Atoms.Raycast.Raycast RightStickyRaycast
        {
            get => rightStickyRaycast.Value;
            set => value = rightStickyRaycast.Value;
        }

        public static readonly string RightStickyRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
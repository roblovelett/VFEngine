using ScriptableObjects.Atoms.Raycast.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.StickyRaycast.LeftStickyRaycast
{
    using static ScriptableObjectExtensions;
    using static StickyRaycastData;

    public class LeftStickyRaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private FloatReference stickyRaycastLength = new FloatReference();
        [SerializeField] private FloatReference boundsWidth = new FloatReference();
        [SerializeField] private FloatReference maximumSlopeAngle = new FloatReference();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private Vector2Reference boundsBottomLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsCenter = new Vector2Reference();
        [SerializeField] private LayerMask raysBelowLayerMaskPlatforms = new LayerMask();
        [SerializeField] private new Transform transform = null;

        #endregion

        [SerializeField] private RaycastReference leftStickyRaycast = new RaycastReference();
        [SerializeField] private FloatReference leftStickyRaycastLength = new FloatReference();
        [SerializeField] private FloatReference leftStickyRaycastOriginY = new FloatReference();
        private static readonly string LeftStickyRaycastPath = $"{StickyRaycastPath}RightStickyRaycast/";
        private static readonly string ModelAssetPath = $"{LeftStickyRaycastPath}RightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public float BoundsWidth => boundsWidth.Value;
        public float MaximumSlopeAngle => maximumSlopeAngle.Value;
        public float BoundsHeight => boundsHeight.Value;
        public float RayOffset => rayOffset.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms;
        public Transform Transform => transform;

        #endregion

        public float LeftStickyRaycastLength
        {
            get => leftStickyRaycastLength.Value;
            set => value = leftStickyRaycastLength.Value;
        }

        [HideInInspector] public Vector2 leftStickyRaycastOrigin;

        public ScriptableObjects.Atoms.Raycast.Raycast LeftStickyRaycast
        {
            get => leftStickyRaycast.Value;
            set => value = leftStickyRaycast.Value;
        }

        public float LeftStickyRaycastOriginY
        {
            set => value = leftStickyRaycastOriginY.Value;
        }

        public static readonly string LeftStickyRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
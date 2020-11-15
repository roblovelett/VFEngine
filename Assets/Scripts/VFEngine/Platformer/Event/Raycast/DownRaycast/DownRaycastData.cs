using ScriptableObjects.Atoms.Mask.References;
using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;
    [InlineEditor]
    public class DownRaycastData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private IntReference currentDownHitsStorageIndex = new IntReference();
        [SerializeField] private IntReference numberOfVerticalRaysPerSide = new IntReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopRightCorner = new Vector2Reference();
        [SerializeField] private new Transform transform = null;
        [SerializeField] private MaskReference raysBelowLayerMaskPlatformsWithoutOneWay = new MaskReference();
        [SerializeField] private MaskReference raysBelowLayerMaskPlatforms = new MaskReference();

        #endregion

        [SerializeField] private FloatReference downRayLength = new FloatReference();
        [SerializeField] private Vector2Reference currentDownRaycastOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference downRaycastFromLeft = new Vector2Reference();
        [SerializeField] private Vector2Reference downRaycastToRight = new Vector2Reference();
        [SerializeField] private RaycastReference currentDownRaycast = new RaycastReference();
        private static readonly string DownRaycastPath = $"{RaycastPath}DownRaycast/";
        private static readonly string ModelAssetPath = $"{DownRaycastPath}DownRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public int CurrentDownHitsStorageIndex => currentDownHitsStorageIndex.Value;
        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public float RayOffset => rayOffset.Value;
        public float BoundsHeight => boundsHeight.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 BoundsTopLeftCorner => boundsTopLeftCorner.Value;
        public Vector2 BoundsTopRightCorner => boundsTopRightCorner.Value;
        public Transform Transform => transform;
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay => raysBelowLayerMaskPlatformsWithoutOneWay.Value.layer;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value.layer;

        #endregion

        public float DownRayLength
        {
            get => downRayLength.Value;
            set => value = downRayLength.Value;
        }

        public Vector2 CurrentDownRaycastOrigin
        {
            get => currentDownRaycastOrigin.Value;
            set => value = currentDownRaycastOrigin.Value;
        }

        public Vector2 DownRaycastFromLeft
        {
            get => downRaycastFromLeft.Value;
            set => value = downRaycastFromLeft.Value;
        }

        public Vector2 DownRaycastToRight
        {
            get => downRaycastToRight.Value;
            set => value = downRaycastToRight.Value;
        }

        public ScriptableObjects.Atoms.Raycast.Raycast CurrentDownRaycast
        {
            set => value = currentDownRaycast.Value;
        }

        public static readonly string DownRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
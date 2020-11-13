using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.Raycast.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class DownRaycastData : MonoBehaviour
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
        [SerializeField] private new TransformReference transform = new TransformReference();
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatformsWithoutOneWay = new LayerMaskReference();
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms = new LayerMaskReference();

        #endregion

        [SerializeField] private FloatReference downRayLength = new FloatReference();
        [SerializeField] private Vector2Reference currentDownRaycastOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference downRaycastFromLeft = new Vector2Reference();
        [SerializeField] private Vector2Reference downRaycastToRight = new Vector2Reference();
        [SerializeField] private RaycastReference currentDownRaycast = new RaycastReference();
        private static readonly string DownRaycastPath = $"{RaycastPath}DownRaycast/";
        private static readonly string ModelAssetPath = $"{DownRaycastPath}DefaultDownRaycastModel.asset";

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
        public Transform Transform => transform.Value;
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay => raysBelowLayerMaskPlatformsWithoutOneWay.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;

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
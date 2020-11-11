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

        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private IntReference currentDownHitsStorageIndex;
        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private Vector2Reference boundsTopLeftCorner;
        [SerializeField] private Vector2Reference boundsTopRightCorner;
        [SerializeField] private Vector2Reference standingOnWithSmallestDistancePoint;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatformsWithoutOneWay;
        [SerializeField] private LayerMaskReference raysBelowLayerMaskPlatforms;
        [SerializeField] private FloatReference raycastDownHitAtDistance;

        #endregion

        [SerializeField] private FloatReference downRayLength;
        [SerializeField] private FloatReference smallestDistanceToDownHit;
        [SerializeField] private FloatReference currentDistanceOfDownRaycastAndSmallestDistancePoint;
        [SerializeField] private Vector2Reference currentDownRaycastOrigin;
        [SerializeField] private Vector2Reference downRaycastFromLeft;
        [SerializeField] private Vector2Reference downRaycastToRight;
        [SerializeField] private RaycastReference currentDownRaycast;
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
        public Vector2 StandingOnWithSmallestDistancePoint => standingOnWithSmallestDistancePoint.Value;
        public Transform Transform => transform.Value;
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay => raysBelowLayerMaskPlatformsWithoutOneWay.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value;
        public float RaycastDownHitAtDistance => raycastDownHitAtDistance.Value;

        #endregion

        public float DownRayLength
        {
            get => downRayLength.Value;
            set => value = downRayLength.Value;
        }

        public float SmallestDistanceToDownHit
        {
            set => value = smallestDistanceToDownHit.Value;
        }

        public float CurrentDistanceOfDownRaycastAndSmallestDistancePoint
        {
            set => value = currentDistanceOfDownRaycastAndSmallestDistancePoint.Value;
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
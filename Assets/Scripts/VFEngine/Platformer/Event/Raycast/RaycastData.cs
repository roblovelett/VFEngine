using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;
// ReSharper disable RedundantAssignment

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;
    using static Vector2;

    public class RaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private new TransformReference transform;
        private int NumberOfHorizontalRaysSetting => settings.numberOfHorizontalRays;
        private int NumberOfVerticalRaysSetting => settings.numberOfVerticalRays;

        #endregion

        [SerializeField] private BoolReference displayWarningsControl;
        [SerializeField] private BoolReference drawRaycastGizmosControl;
        [SerializeField] private BoolReference castRaysOnBothSides;
        [SerializeField] private BoolReference performSafetyBoxcast;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength;
        [SerializeField] private FloatReference boundsWidth;
        [SerializeField] private FloatReference boundsHeight;
        [SerializeField] private Vector2Reference boundsCenter;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private Vector2Reference boundsBottomCenterPosition;
        [SerializeField] private Vector2Reference boundsTopLeftCorner;
        [SerializeField] private Vector2Reference boundsTopRightCorner;
        [SerializeField] private IntReference numberOfHorizontalRaysPerSide;
        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private FloatReference obstacleHeightTolerance;
        private static readonly string ModelAssetPath = $"{RaycastPath}DefaultRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool HasSettings => settings;
        public bool HasBoxCollider => boxCollider;
        public BoxCollider2D BoxCollider => boxCollider;
        public Transform Transform => transform.Value;
        public bool DisplayWarningsControlSetting => settings.displayWarningsControl;
        public bool DrawRaycastGizmosControlSetting => settings.drawRaycastGizmosControl;
        public bool CastRaysOnBothSidesSetting => settings.castRaysOnBothSides;
        public bool PerformSafetyBoxcastSetting => settings.performSafetyBoxcast;
        public float RayOffsetSetting => settings.rayOffset;
        public float DistanceToGroundRayMaximumLengthSetting => settings.distanceToGroundRayMaximumLength;

        #endregion

        public bool DisplayWarningsControl
        {
            get => displayWarningsControl.Value;
            set => value = displayWarningsControl.Value;
        }

        public bool DrawRaycastGizmosControl
        {
            set => value = drawRaycastGizmosControl.Value;
        }

        public bool CastRaysOnBothSides
        {
            get => castRaysOnBothSides.Value;
            set => value = castRaysOnBothSides.Value;
        }

        public bool PerformSafetyBoxcast
        {
            set => value = performSafetyBoxcast.Value;
        }

        public float DistanceToGroundRayMaximumLength
        {
            get => distanceToGroundRayMaximumLength.Value;
            set => value = distanceToGroundRayMaximumLength.Value;
        }

        public int NumberOfHorizontalRays => NumberOfHorizontalRaysSetting;
        public int NumberOfVerticalRays => NumberOfVerticalRaysSetting;

        public float ObstacleHeightTolerance
        {
            set => value = obstacleHeightTolerance.Value;
        }

        public float RayOffset
        {
            get => rayOffset.Value;
            set => value = rayOffset.Value;
        }

        public float BoundsWidth
        {
            get => boundsWidth.Value;
            set => value = boundsWidth.Value;
        }

        public float BoundsHeight
        {
            get => boundsHeight.Value;
            set => value = boundsHeight.Value;
        }

        public Vector2 bounds = zero;

        public Vector2 BoundsCenter
        {
            set => value = boundsCenter.Value;
        }

        public Vector2 BoundsBottomLeftCorner
        {
            get => boundsBottomLeftCorner.Value;
            set => value = boundsBottomLeftCorner.Value;
        }

        public Vector2 BoundsBottomRightCorner
        {
            get => boundsBottomRightCorner.Value;
            set => value = boundsBottomRightCorner.Value;
        }

        public Vector2 BoundsTopLeftCorner
        {
            get => boundsTopLeftCorner.Value;
            set => value = boundsTopLeftCorner.Value;
        }

        public Vector2 BoundsTopRightCorner
        {
            set => value = boundsTopRightCorner.Value;
        }

        public Vector2 BoundsBottomCenterPosition
        {
            get => boundsBottomCenterPosition.Value;
            set => value = boundsBottomCenterPosition.Value;
        }

        public int NumberOfHorizontalRaysPerSide
        {
            set => value = numberOfHorizontalRaysPerSide.Value;
        }

        public int NumberOfVerticalRaysPerSide
        {
            set => value = numberOfVerticalRaysPerSide.Value;
        }

        public const string RaycastPath = "Event/Raycast/";
        public static readonly string RaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
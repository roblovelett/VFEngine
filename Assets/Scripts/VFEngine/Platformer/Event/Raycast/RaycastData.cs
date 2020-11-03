using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Serialization;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast
{
    using static ScriptableObjectExtensions;
    using static Vector2;

    public class RaycastData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastSettings settings;
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
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
        public Vector2 BoxColliderSize => boxColliderSize.Value;
        public Vector2 BoxColliderOffset => boxColliderOffset.Value;
        public Vector2 BoxColliderBoundsCenter => boxColliderBoundsCenter.Value;
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
            get => drawRaycastGizmosControl.Value;
            set => value = drawRaycastGizmosControl.Value;
        }

        public bool CastRaysOnBothSides
        {
            get => castRaysOnBothSides.Value;
            set => value = castRaysOnBothSides.Value;
        }

        public bool PerformSafetyBoxcast
        {
            get => performSafetyBoxcast.Value;
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
            get => obstacleHeightTolerance.Value;
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
            get => boundsCenter.Value;
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
            get => boundsTopRightCorner.Value;
            set => value = boundsTopRightCorner.Value;
        }

        public int NumberOfHorizontalRaysPerSide
        {
            get => numberOfHorizontalRaysPerSide;
            set => value = numberOfHorizontalRaysPerSide.Value;
        }

        public int NumberOfVerticalRaysPerSide
        {
            get => numberOfVerticalRaysPerSide.Value;
            set => value = numberOfVerticalRaysPerSide.Value;
        }

        public const string RaycastPath = "Event/Raycast/";
        public static readonly string RaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
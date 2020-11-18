using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.LeftRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    [InlineEditor]
    [CreateAssetMenu(fileName = "LeftRaycastData", menuName = PlatformerLeftRaycastDataPath, order = 0)]
    public class LeftRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;

        #endregion

        [SerializeField] private FloatReference leftRayLength = new FloatReference();
        [SerializeField] private Vector2Reference leftRaycastFromBottomOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference leftRaycastToTopOrigin = new Vector2Reference();
        [SerializeField] private RaycastReference currentLeftRaycastHit = new RaycastReference();
        private static readonly string LeftRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{LeftRaycastPath}LeftRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public int CurrentLeftHitsStorageIndex { get; set; }
        public float RayOffset { get; set; }
        public float ObstacleHeightTolerance { get; set; }
        public float BoundsWidth { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public Vector2 Speed { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask OneWayPlatformMask { get; set; }
        public LayerMask MovingOneWayPlatformMask { get; set; }

        #endregion

        public float LeftRayLength
        {
            get => leftRayLength.Value;
            set => value = leftRayLength.Value;
        }

        public Vector2 LeftRaycastFromBottomOrigin
        {
            get => leftRaycastFromBottomOrigin.Value;
            set => value = leftRaycastFromBottomOrigin.Value;
        }

        public Vector2 LeftRaycastToTopOrigin
        {
            get => leftRaycastToTopOrigin.Value;
            set => value = leftRaycastToTopOrigin.Value;
        }

        public Vector2 CurrentLeftRaycastOrigin { get; set; }

        public RaycastHit2D CurrentLeftRaycastHit
        {
            get => currentLeftRaycastHit.Value.hit2D;
            set => currentLeftRaycastHit.Value = new ScriptableObjects.Variables.Raycast(value);
        }

        public static readonly string LeftRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.RightRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RightRaycastData", menuName = PlatformerRightRaycastDataPath, order = 0)]
    [InlineEditor]
    public class RightRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObjectReference character = null;

        #endregion

        [SerializeField] private FloatReference rightRayLength = new FloatReference();
        [SerializeField] private Vector2Reference rightRaycastFromBottomOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference rightRaycastToTopOrigin = new Vector2Reference();
        [SerializeField] private RaycastReference currentRightRaycastHit = new RaycastReference();
        private static readonly string RightRaycastPath = $"{RaycastPath}RightRaycast/";
        private static readonly string ModelAssetPath = $"{RightRaycastPath}RightRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character.Value;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public int NumberOfHorizontalRaysPerSide { get; set; }
        public int CurrentRightHitsStorageIndex { get; set; }
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

        public float RightRayLength
        {
            get => rightRayLength.Value;
            set => value = rightRayLength.Value;
        }

        public Vector2 RightRaycastFromBottomOrigin
        {
            get => rightRaycastFromBottomOrigin.Value;
            set => value = rightRaycastFromBottomOrigin.Value;
        }

        public Vector2 RightRaycastToTopOrigin
        {
            get => rightRaycastToTopOrigin.Value;
            set => value = rightRaycastToTopOrigin.Value;
        }

        public Vector2 CurrentRightRaycastOrigin { get; set; }

        public RaycastHit2D CurrentRightRaycastHit
        {
            get => currentRightRaycastHit.Value.hit2D;
            set => currentRightRaycastHit.Value = new ScriptableObjects.Variables.Raycast(value);
        }

        public static readonly string RightRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "UpRaycastData", menuName = PlatformerUpRaycastDataPath, order = 0)]
    [InlineEditor]
    public class UpRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character = null;

        #endregion

        [SerializeField] private FloatReference upRaycastSmallestDistance = new FloatReference();
        [SerializeField] private Vector2Reference currentUpRaycastOrigin = new Vector2Reference();
        [SerializeField] private RaycastReference currentUpRaycastHit = new RaycastReference();
        private static readonly string UpRaycastPath = $"{RaycastPath}UpRaycast/";
        private static readonly string ModelAssetPath = $"{UpRaycastPath}UpRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform => character.transform;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool GroundedEvent { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public int CurrentUpHitsStorageIndex { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public float RayOffset { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public LayerMask PlatformMask { get; set; }
        public LayerMask OneWayPlatformMask { get; set; }
        public LayerMask MovingOneWayPlatformMask { get; set; }
        public RaycastHit2D RaycastUpHitAt { get; set; }

        #endregion

        public float UpRayLength { get; set; }
        public Vector2 UpRaycastStart { get; set; } = Vector2.zero;
        public Vector2 UpRaycastEnd { get; set; } = Vector2.zero;

        public float UpRaycastSmallestDistance
        {
            get => upRaycastSmallestDistance.Value;
            set => value = upRaycastSmallestDistance.Value;
        }

        public Vector2 CurrentUpRaycastOrigin
        {
            get => currentUpRaycastOrigin.Value;
            set => value = currentUpRaycastOrigin.Value;
        }

        public RaycastHit2D CurrentUpRaycastHit
        {
            get => currentUpRaycastHit.Value.hit2D;
            set => value = currentUpRaycastHit.Value.hit2D;
        }

        public static readonly string UpRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
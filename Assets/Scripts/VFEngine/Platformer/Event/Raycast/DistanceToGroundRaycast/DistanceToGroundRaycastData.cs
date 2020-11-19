using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastData", menuName = PlatformerDistanceToGroundRaycastDataPath,
        order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObjectReference character = null;

        #endregion

        [SerializeField] private RaycastReference distanceToGroundRaycast = new RaycastReference();
        private static readonly string DistanceToGroundRaycastPath = $"{RaycastPath}DistanceToGroundRaycast/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastPath}DistanceToGroundRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character.Value;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public float BelowSlopeAngle { get; set; }
        public float DistanceToGroundRayMaximumLength { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsCenter { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public RaycastHit2D DistanceToGroundRaycast
        {
            get => distanceToGroundRaycast.Value.hit2D;
            set => distanceToGroundRaycast.Value = new ScriptableObjects.Variables.Raycast(value);
        }

        public Vector2 DistanceToGroundRaycastOrigin { get; set; } = Vector2.zero;

        public static readonly string DistanceToGroundRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
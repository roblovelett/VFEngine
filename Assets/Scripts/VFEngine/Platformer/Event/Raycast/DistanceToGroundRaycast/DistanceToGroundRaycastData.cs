using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.DistanceToGroundRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;
    
    [CreateAssetMenu(fileName = "DistanceToGroundRaycastData", menuName = PlatformerDistanceToGroundRaycastDataPath, order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastData : ScriptableObject
    {
        #region fields

        #region dependencies

        /*
        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private FloatReference belowSlopeAngle = new FloatReference();
        [SerializeField] private Vector2Reference boundsBottomLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsCenter = new Vector2Reference();
        [SerializeField] private MaskReference raysBelowLayerMaskPlatforms = new MaskReference();
        [SerializeField] private new Transform transform = null;
        [SerializeField] private FloatReference distanceToGroundRayMaximumLength = new FloatReference();
        */
        
        #endregion

        [SerializeField] private RaycastReference distanceToGroundRaycast = new RaycastReference();
        private static readonly string DistanceToGroundRaycastPath = $"{RaycastPath}DistanceToGroundRaycast/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastPath}DistanceToGroundRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        /*
        public float BelowSlopeAngle => belowSlopeAngle.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value.layer;
        public Transform Transform => transform;
        public bool DrawRaycastGizmosControl => drawRaycastGizmosControl.Value;
        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;
        */
        
        #endregion

        public RaycastHit2D DistanceToGroundRaycast
        {
            set => distanceToGroundRaycast.Value = new ScriptableObjects.Variables.Raycast(value);
        }
        
        public Vector2 DistanceToGroundRaycastOrigin { get; set; } = Vector2.zero;

        public static readonly string DistanceToGroundRaycastModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
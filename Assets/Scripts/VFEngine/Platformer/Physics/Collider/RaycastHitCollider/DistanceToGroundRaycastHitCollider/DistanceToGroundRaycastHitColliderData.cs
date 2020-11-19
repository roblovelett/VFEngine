using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "DistanceToGroundRaycastHitColliderData",
        menuName = PlatformerDistanceToGroundRaycastHitColliderDataPath, order = 0)]
    [InlineEditor]
    public class DistanceToGroundRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region depenedencies

        [SerializeField] private GameObjectReference character = null;

        #endregion

        [SerializeField] private BoolReference distanceToGroundRaycastHitConnected = new BoolReference();

        private static readonly string DistanceToGroundRaycastHitColliderPath =
            $"{RaycastHitColliderPath}DistanceToGroundRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastHitColliderPath}DistanceToGroundRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region depenedencies

        public GameObject Character => character.Value;
        public PlatformerRuntimeData RuntimeData { get; set; }
        public float DistanceToGroundRayMaximumLength { get; set; }
        public RaycastHit2D DistanceToGroundRaycastHit { get; set; }
        public float BoundsHeight { get; set; }

        #endregion

        public float DistanceToGround { get; set; }

        public bool DistanceToGroundRaycastHitConnected
        {
            get => distanceToGroundRaycastHitConnected.Value;
            set => value = distanceToGroundRaycastHitConnected.Value;
        }

        public static readonly string DistanceToGroundRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
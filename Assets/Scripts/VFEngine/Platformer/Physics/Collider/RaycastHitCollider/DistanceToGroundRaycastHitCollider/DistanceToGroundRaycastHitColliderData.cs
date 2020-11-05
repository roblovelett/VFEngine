using ScriptableObjects.Atoms.RaycastHit2D.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.DistanceToGroundRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class DistanceToGroundRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region depenedencies

        [SerializeField] private FloatReference distanceToGroundRayMaximumLength;
        [SerializeField] private RaycastHit2DReference distanceToGroundRaycast;
        [SerializeField] private FloatReference boundsHeight;

        #endregion

        [SerializeField] private BoolReference hasDistanceToGroundRaycast;
        private RaycastHit2D DistanceToGroundRaycast => distanceToGroundRaycast.Value;

        private static readonly string DistanceToGroundRaycastHitColliderPath =
            $"{RaycastHitColliderPath}DistanceToGroundRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastHitColliderPath}DefaultDownRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region depenedencies

        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;
        public float DistanceToGroundRaycastDistance => DistanceToGroundRaycast.distance;
        public float BoundsHeight => boundsHeight.Value;

        #endregion

        public bool HasDistanceToGroundRaycast
        {
            set => value = hasDistanceToGroundRaycast.Value;
        }

        public float DistanceToGround { get; set; }

        public static readonly string DistanceToGroundRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
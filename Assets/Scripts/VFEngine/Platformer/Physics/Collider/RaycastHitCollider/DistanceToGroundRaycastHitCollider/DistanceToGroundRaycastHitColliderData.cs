using ScriptableObjects.Atoms.Raycast.References;
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

        [SerializeField] private FloatReference distanceToGroundRayMaximumLength = new FloatReference();
        [SerializeField] private RaycastReference distanceToGroundRaycast = new RaycastReference();
        [SerializeField] private FloatReference boundsHeight = new FloatReference();

        #endregion

        [SerializeField] private BoolReference distanceToGroundRaycastHit = new BoolReference();

        private static readonly string DistanceToGroundRaycastHitColliderPath =
            $"{RaycastHitColliderPath}DistanceToGroundRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{DistanceToGroundRaycastHitColliderPath}DownRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region depenedencies

        public float DistanceToGroundRayMaximumLength => distanceToGroundRayMaximumLength.Value;

        public RaycastHit2D DistanceToGroundRaycast
        {
            get
            {
                var r = distanceToGroundRaycast.Value;
                return r.hit2D;
            }
        }

        public float BoundsHeight => boundsHeight.Value;

        #endregion

        public bool DistanceToGroundRaycastHit
        {
            set => value = distanceToGroundRaycastHit.Value;
        }

        public float DistanceToGround { get; set; }

        public static readonly string DistanceToGroundRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
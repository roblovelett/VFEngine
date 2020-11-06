using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;
    public class LeftStickyRaycastHitColliderData : MonoBehaviour
    {
        #region fields
        
        #region dependencies

        [SerializeField] private RaycastHit2DReference leftStickyRaycast;
        [SerializeField] private new TransformReference transform;

        #endregion

        [SerializeField] private FloatReference belowSlopeAngleLeft;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleLeft;
        
        private static readonly string LeftStickyRaycastHitColliderPath = $"{StickyRaycastHitColliderPath}LeftStickyRaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{LeftStickyRaycastHitColliderPath}DefaultLeftStickyRaycastHitColliderModel.asset";
        
        #endregion

        #region properties

        #region dependencies

        public RaycastHit2D LeftStickyRaycast => leftStickyRaycast.Value;
        public Transform Transform => transform.Value;

        #endregion

        public float BelowSlopeAngleLeft
        {
            get => belowSlopeAngleLeft.Value;
            set => value = belowSlopeAngleLeft.Value;
        }

        public Vector3 CrossBelowSlopeAngleLeft
        {
            set => value = crossBelowSlopeAngleLeft.Value;
        }
        
        public static readonly string LeftStickyRaycastHitColliderModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
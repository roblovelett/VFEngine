using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;
    [InlineEditor]
    public class RightStickyRaycastHitColliderData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastReference rightStickyRaycast = new RaycastReference();
        [SerializeField] private new Transform transform = null;

        #endregion

        [SerializeField] private FloatReference belowSlopeAngleRight = new FloatReference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngleRight = new Vector3Reference();

        private static readonly string RightStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}RightStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightStickyRaycastHitColliderPath}RightStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public RaycastHit2D RightStickyRaycast
        {
            get
            {
                var r = rightStickyRaycast.Value;
                return r.hit2D;
            }
        }

        public Transform Transform => transform;

        #endregion

        public float BelowSlopeAngleRight
        {
            get => belowSlopeAngleRight.Value;
            set => value = belowSlopeAngleRight.Value;
        }

        public Vector3 CrossBelowSlopeAngleRight
        {
            set => value = crossBelowSlopeAngleRight.Value;
        }

        public static readonly string RightStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
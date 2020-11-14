using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.LeftStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;
    [InlineEditor]
    public class LeftStickyRaycastHitColliderData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private RaycastReference leftStickyRaycast = new RaycastReference();
        [SerializeField] private new Transform transform = null;

        #endregion

        [SerializeField] private FloatReference belowSlopeAngleLeft = new FloatReference();
        [SerializeField] private Vector3Reference crossBelowSlopeAngleLeft = new Vector3Reference();

        private static readonly string LeftStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}LeftStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{LeftStickyRaycastHitColliderPath}LeftStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public RaycastHit2D LeftStickyRaycast
        {
            get
            {
                var r = leftStickyRaycast.Value;
                return r.hit2D;
            }
        }

        public Transform Transform => transform;

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

        public static readonly string LeftStickyRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
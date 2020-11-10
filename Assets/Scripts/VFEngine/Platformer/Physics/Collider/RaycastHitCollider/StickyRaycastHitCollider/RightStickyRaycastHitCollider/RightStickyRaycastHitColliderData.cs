﻿//using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;
// ReSharper disable RedundantAssignment

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.StickyRaycastHitCollider.RightStickyRaycastHitCollider
{
    using static StickyRaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class RightStickyRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        //[SerializeField] private RaycastHit2DReference rightStickyRaycast;
        [SerializeField] private new TransformReference transform;

        #endregion

        [SerializeField] private FloatReference belowSlopeAngleRight;
        [SerializeField] private Vector3Reference crossBelowSlopeAngleRight;

        private static readonly string RightStickyRaycastHitColliderPath =
            $"{StickyRaycastHitColliderPath}RightStickyRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{RightStickyRaycastHitColliderPath}DefaultRightStickyRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        //public RaycastHit2D RightStickyRaycast => rightStickyRaycast.Value;
        public Transform Transform => transform.Value;

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
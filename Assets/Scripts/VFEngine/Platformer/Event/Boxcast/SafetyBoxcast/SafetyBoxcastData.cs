﻿using ScriptableObjects.Atoms.Mask.References;
using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    using static ScriptableObjectExtensions;

    [InlineEditor]
    public class SafetyBoxcastData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference drawRaycastGizmosControl = new BoolReference();
        [SerializeField] private Vector2Reference bounds = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsCenter = new Vector2Reference();
        [SerializeField] private new Transform transform = null;
        [SerializeField] private FloatReference stickyRaycastLength = new FloatReference();
        [SerializeField] private MaskReference raysBelowLayerMaskPlatforms = new MaskReference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private MaskReference platformMask = new MaskReference();

        #endregion

        [SerializeField] private RaycastReference safetyBoxcast = new RaycastReference();
        private const string SbPath = "Event/Boxcast/SafetyBoxcast/";
        private static readonly string ModelAssetPath = $"{SbPath}SafetyBoxcastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public LayerMask PlatformMask => platformMask.Value.layer;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 Bounds => bounds.Value;
        public Vector2 BoundsCenter => boundsCenter.Value;
        public Transform Transform => transform;
        public float StickyRaycastLength => stickyRaycastLength.Value;
        public LayerMask RaysBelowLayerMaskPlatforms => raysBelowLayerMaskPlatforms.Value.layer;
        public bool DrawBoxcastGizmosControl => drawRaycastGizmosControl.Value;

        #endregion

        public static readonly string SafetyBoxcastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        public ScriptableObjects.Atoms.Raycast.Raycast SafetyBoxcast
        {
            set => value = safetyBoxcast.Value;
        }

        #endregion
    }
}
﻿using ScriptableObjects.Atoms.Mask.References;
using ScriptableObjects.Atoms.Raycast.References;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;
    [InlineEditor]
    public class UpRaycastData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private BoolReference groundedEvent = new BoolReference();
        [SerializeField] private BoolReference drawRaycastGizmos = new BoolReference();
        [SerializeField] private FloatReference rayOffset = new FloatReference();
        [SerializeField] private IntReference currentUpHitsStorageIndex = new IntReference();
        [SerializeField] private IntReference numberOfVerticalRaysPerSide = new IntReference();
        [SerializeField] private Vector2Reference newPosition = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsBottomRightCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopLeftCorner = new Vector2Reference();
        [SerializeField] private Vector2Reference boundsTopRightCorner = new Vector2Reference();
        [SerializeField] private new Transform transform = null;
        [SerializeField] private MaskReference platformMask = new MaskReference();
        [SerializeField] private MaskReference oneWayPlatformMask = new MaskReference();
        [SerializeField] private MaskReference movingOneWayPlatformMask = new MaskReference();
        [SerializeField] private RaycastReference raycastUpHitAt = new RaycastReference();

        #endregion

        [SerializeField] private FloatReference upRaycastSmallestDistance = new FloatReference();
        [SerializeField] private Vector2Reference currentUpRaycastOrigin = new Vector2Reference();
        [SerializeField] private RaycastReference currentUpRaycast = new RaycastReference();
        private static readonly string UpRaycastPath = $"{RaycastPath}UpRaycast/";
        private static readonly string ModelAssetPath = $"{UpRaycastPath}UpRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public bool GroundedEvent => groundedEvent.Value;
        public bool DrawRaycastGizmos => drawRaycastGizmos.Value;
        public int CurrentUpHitsStorageIndex => currentUpHitsStorageIndex.Value;
        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public float RayOffset => rayOffset.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 BoundsTopLeftCorner => boundsTopLeftCorner.Value;
        public Vector2 BoundsTopRightCorner => boundsTopRightCorner.Value;
        public LayerMask PlatformMask => platformMask.Value.layer;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask.Value.layer;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask.Value.layer;
        public Transform Transform => transform;

        public RaycastHit2D RaycastUpHitAt
        {
            get
            {
                var r = raycastUpHitAt.Value;
                return r.hit2D;
            }
        }

        #endregion

        [HideInInspector] public float upRayLength;
        public Vector2 UpRaycastStart { get; set; } = Vector2.zero;
        public Vector2 UpRaycastEnd { get; set; } = Vector2.zero;

        public float UpRaycastSmallestDistance
        {
            set => value = upRaycastSmallestDistance.Value;
        }

        public Vector2 CurrentUpRaycastOrigin
        {
            get => currentUpRaycastOrigin.Value;
            set => value = currentUpRaycastOrigin.Value;
        }

        public ScriptableObjects.Variables.Raycast CurrentUpRaycast
        {
            set => value = currentUpRaycast.Value;
        }

        public static readonly string UpRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Event.Raycast.UpRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    public class UpRaycastData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private BoolReference isGrounded;
        [SerializeField] private BoolReference drawRaycastGizmos;
        [SerializeField] private FloatReference rayOffset;
        [SerializeField] private IntReference currentUpHitsStorageIndex;
        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private Vector2Reference newPosition;
        [SerializeField] private Vector2Reference boundsBottomLeftCorner;
        [SerializeField] private Vector2Reference boundsBottomRightCorner;
        [SerializeField] private Vector2Reference boundsTopLeftCorner;
        [SerializeField] private Vector2Reference boundsTopRightCorner;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private LayerMaskReference platformMask;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;
        [SerializeField] private RaycastHit2DReference raycastUpHitAt;

        /* fields */
        [SerializeField] private FloatReference upRaycastSmallestDistance;
        [SerializeField] private Vector2Reference currentUpRaycastOrigin;
        [SerializeField] private RaycastHit2DReference currentUpRaycast;
        private static readonly string UpRaycastPath = $"{RaycastPath}UpRaycast/";
        private static readonly string ModelAssetPath = $"{UpRaycastPath}DefaultUpRaycastModel.asset";

        /* properties: dependencies */
        public bool IsGrounded => isGrounded.Value;
        public bool DrawRaycastGizmos => drawRaycastGizmos.Value;
        public int CurrentUpHitsStorageIndex => currentUpHitsStorageIndex.Value;
        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public float RayOffset => rayOffset.Value;
        public Vector2 NewPosition => newPosition.Value;
        public Vector2 BoundsBottomLeftCorner => boundsBottomLeftCorner.Value;
        public Vector2 BoundsBottomRightCorner => boundsBottomRightCorner.Value;
        public Vector2 BoundsTopLeftCorner => boundsTopLeftCorner.Value;
        public Vector2 BoundsTopRightCorner => boundsTopRightCorner.Value;
        public LayerMask PlatformMask => platformMask.Value;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask.Value;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask.Value;
        public Transform Transform => transform.Value;
        public RaycastHit2D RaycastUpHitAt => raycastUpHitAt.Value;

        /* properties */
        public float UpRayLength { get; set; }
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

        public RaycastHit2D CurrentUpRaycast
        {
            set => value = currentUpRaycast.Value;
        }

        public static readonly string UpRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
    }
}
using System.Collections.Generic;
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;

    public class RaycastHitColliderData : MonoBehaviour
    {
        /* fields: dependencies */
        [SerializeField] private RaycastHitColliderSettings settings;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private IntReference numberOfHorizontalRays;
        [SerializeField] private IntReference numberOfVerticalRays;
        [SerializeField] private BoolReference castRaysBothSides;
        [SerializeField] private Vector2Reference rightRaycastOriginPoint;
        [SerializeField] private Vector2Reference leftRaycastOriginPoint;
        [SerializeField] private new TransformReference transform;
        [SerializeField] private FloatReference horizontalRayLength;
        [SerializeField] private LayerMaskReference platformMask;
        [SerializeField] private LayerMaskReference oneWayPlatformMask;
        [SerializeField] private LayerMaskReference movingOneWayPlatformMask;
        [SerializeField] private BoolReference drawRaycastGizmos;
        [SerializeField] private IntReference rightHitsStorageIndex;
        [SerializeField] private IntReference leftHitsStorageIndex;

        /* fields */
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
        [SerializeField] private IntReference horizontalHitsStorageIndexesAmount;
        private const string RhcPath = "Physics/Collider/RaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{RhcPath}DefaultRaycastHitColliderModel.asset";

        /* properties: dependencies */
        public bool HasSettings => settings;
        public bool DisplayWarnings => settings.displayWarningsControl;
        public bool HasBoxCollider => boxCollider;
        public Vector2 OriginalColliderSize => boxCollider.size;
        public Vector2 OriginalColliderOffset => boxCollider.offset;
        public Vector2 OriginalColliderBoundsCenter => boxCollider.bounds.center;
        public int NumberOfHorizontalRays => numberOfHorizontalRays.Value;
        public int NumberOfVerticalRays => numberOfVerticalRays.Value;
        public bool CastRaysBothSides => castRaysBothSides.Value;
        public Vector2 RightRaycastOriginPoint => rightRaycastOriginPoint.Value;
        public Vector2 LeftRaycastOriginPoint => leftRaycastOriginPoint.Value;
        public Transform Transform => transform.Value;
        public float HorizontalRayLength => horizontalRayLength.Value;
        public LayerMask PlatformMask => platformMask.Value;
        public LayerMask OneWayPlatformMask => oneWayPlatformMask.Value;
        public LayerMask MovingOneWayPlatformMask => movingOneWayPlatformMask.Value;
        public bool DrawRaycastGizmos => drawRaycastGizmos.Value;
        public int RightHitsStorageIndex => rightHitsStorageIndex.Value;
        public int LeftHitsStorageIndex => leftHitsStorageIndex.Value;
        
        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public List<RaycastHit2D> contactList = new List<RaycastHit2D>();
        public readonly RaycastHitColliderState state = new RaycastHitColliderState();
        public float MovingPlatformCurrentGravity { get; set; }
        public float MovingPlatformGravity { get; } = -500f;

        public Vector2 BoxColliderSizeRef
        {
            set => value = boxColliderSize.Value;
        }

        public Vector2 BoxColliderOffsetRef
        {
            set => value = boxColliderOffset.Value;
        }

        public Vector2 BoxColliderBoundsCenterRef
        {
            set => value = boxColliderBoundsCenter.Value;
        }

        public int HorizontalHitsStorageIndexesAmountRef
        {
            set => value = horizontalHitsStorageIndexesAmount.Value;
        }

        public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] DownHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D UpHit { get; set; }
        public RaycastHit2D RightHit { get; set; }
        public RaycastHit2D DownHit { get; set; }
        public RaycastHit2D LeftHit { get; set; }
    }
}
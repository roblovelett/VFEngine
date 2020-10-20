using System.Collections.Generic;
using ScriptableObjects.Atoms.LayerMask.References;
using ScriptableObjects.Atoms.RaycastHit2D.References;
using ScriptableObjects.Atoms.Transform.References;
using UnityAtoms.BaseAtoms;
using UnityAtoms.Editor;
using UnityEngine;
using UnityEngine.Serialization;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObjectExtensions;
    using static Mathf;
    using static Vector2;

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
        [SerializeField] private RaycastHit2DReference currentRightRaycast;
        [SerializeField] private RaycastHit2DReference currentLeftRaycast;
        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        private Transform Transform => transform.Value;
        
        /* fields */
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
        [SerializeField] private IntReference verticalHitsStorageLength;
        [SerializeField] private IntReference horizontalHitsStorageLength;
        [SerializeField] private IntReference rightHitsStorageLength;
        [SerializeField] private IntReference leftHitsStorageLength;
        [SerializeField] private IntReference downHitsStorageLength;
        [SerializeField] private IntReference upHitsStorageLength;
        [SerializeField] private IntReference currentRightHitsStorageIndex;
        [SerializeField] private IntReference currentLeftHitsStorageIndex;
        [SerializeField] private IntReference currentDownHitsStorageIndex;
        [SerializeField] private IntReference currentUpHitsStorageIndex;
        [SerializeField] private FloatReference currentRightHitDistance;
        [SerializeField] private FloatReference currentLeftHitDistance;
        [SerializeField] private Collider2DReference currentRightHitCollider;
        [SerializeField] private Collider2DReference currentLeftHitCollider;
        [SerializeField] private Collider2DReference ignoredCollider;
        [SerializeField] private FloatReference currentRightHitAngle;
        [SerializeField] private FloatReference currentLeftHitAngle;
        [SerializeField] private Vector2Reference currentRightHitPoint;
        [SerializeField] private Vector2Reference currentLeftHitPoint;
        [SerializeField] private BoolReference isGrounded;
        [SerializeField] private FloatReference friction;
        [SerializeField] private BoolReference onMovingPlatform;
        [SerializeField] private GameObjectReference standingOnLastFrame;
        [SerializeField] private BoolReference isStandingOnLastFrameNotNull;
        [SerializeField] private Collider2DReference standingOnCollider;
        [SerializeField] private Vector2Reference colliderBottomCenterPosition;
        [SerializeField] private IntReference downHitsStorageSmallestDistanceIndex;
        [SerializeField] private BoolReference downHitConnected;
        private const string RhcPath = "Physics/Collider/RaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{RhcPath}DefaultRaycastHitColliderModel.asset";
        
        /* fields: methods */
        private Vector2 SetCurrentRightHitPoint()
        {
            return RightHitsStorage[CurrentRightHitsStorageIndex].point;
        }

        private Vector2 SetCurrentLeftHitPoint()
        {
            return LeftHitsStorage[CurrentLeftHitsStorageIndex].point;
        }
        
        private float SetCurrentRightHitDistance()
        {
            return RightHitsStorage[CurrentRightHitsStorageIndex].distance;
        }

        private float SetCurrentLeftHitDistance()
        {
            return LeftHitsStorage[CurrentLeftHitsStorageIndex].distance;
        }

        private Collider2D SetCurrentRightHitCollider()
        {
            return RightHitsStorage[CurrentRightHitsStorageIndex].collider;
        }
        
        private Collider2D SetCurrentLeftHitCollider()
        {
            return LeftHitsStorage[CurrentLeftHitsStorageIndex].collider;
        }

        private float SetCurrentRightHitAngle()
        {
            return Abs(Angle(RightHitsStorage[CurrentRightHitsStorageIndex].normal, Transform.up));
        }
        private float SetCurrentLeftHitAngle()
        {
            return Abs(Angle(LeftHitsStorage[CurrentLeftHitsStorageIndex].normal, Transform.up));
        }

        private Vector2 SetColliderBottomCenterPosition()
        {
            var bounds = new Vector2();
            var colliderBounds = boxCollider.bounds;
            bounds.x = colliderBounds.center.x;
            bounds.y = colliderBounds.min.y;
            return bounds;
        }

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

        public RaycastHit2D CurrentRightRaycast => currentRightRaycast.Value;
        public RaycastHit2D CurrentLeftRaycast => currentLeftRaycast.Value;
        
        /* properties */
        public static readonly string ModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";
        public readonly List<RaycastHit2D> contactList = new List<RaycastHit2D>();
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

        public int HorizontalHitsStorageLength { get; set; }
        public int HorizontalHitsStorageLengthRef
        {
            set => value = horizontalHitsStorageLength.Value;
        }
        
        public int VerticalHitsStorageLength { get; set; }

        public int VerticalHitsStorageLengthRef
        {
            set => value = verticalHitsStorageLength.Value;
        }

        public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] RightHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] DownHitsStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] LeftHitsStorage { get; set; } = new RaycastHit2D[0];

        public int UpHitsStorageLength => UpHitsStorage.Length;
        public int RightHitsStorageLength => RightHitsStorage.Length;
        public int DownHitsStorageLength => DownHitsStorage.Length;
        public int LeftHitsStorageLength => LeftHitsStorage.Length;

        public int UpHitsStorageLengthRef
        {
            set => value = upHitsStorageLength.Value;
        }
        public int RightHitsStorageLengthRef
        {
            set => value = rightHitsStorageLength.Value;
        }
        public int DownHitsStorageLengthRef
        {
            set => value = downHitsStorageLength.Value;
        }
        public int LeftHitsStorageLengthRef
        {
            set => value = leftHitsStorageLength.Value;
        }

        public int CurrentRightHitsStorageIndex { get; set; } = 0;
        public int CurrentRightHitsStorageIndexRef
        {
            set => value = currentRightHitsStorageIndex.Value;
        }

        public int CurrentLeftHitsStorageIndex { get; set; } = 0;
        public int CurrentLeftHitsStorageIndexRef
        {
            set => value = currentLeftHitsStorageIndex.Value;
        }
        
        public int CurrentDownHitsStorageIndex { get; set; } = 0;

        public int CurrentDownHitsStorageIndexRef
        {
            set => value = currentDownHitsStorageIndex.Value;
        }

        public int CurrentUpHitsStorageIndex { get; set; } = 0;

        public int CurrentUpHitsStorageIndexRef
        {
            set => value = currentUpHitsStorageIndex.Value;
        }

        public float CurrentRightHitDistance => SetCurrentRightHitDistance();

        public float CurrentLeftHitDistance => SetCurrentLeftHitDistance();

        public float CurrentRightHitDistanceRef
        {
            set => value = currentRightHitDistance.Value;
        }

        public float CurrentLeftHitDistanceRef
        {
            set => value = currentLeftHitDistance.Value;
        }

        public Collider2D CurrentRightHitCollider => SetCurrentRightHitCollider();
        public Collider2D CurrentLeftHitCollider => SetCurrentLeftHitCollider();

        public Collider2D CurrentRightHitColliderRef
        {
            set => value = currentRightHitCollider.Value;
        }

        public Collider2D CurrentLeftHitColliderRef
        {
            set => value = currentLeftHitCollider.Value;
        }

        public Collider2D IgnoredCollider { get; set; }

        public Collider2D IgnoredColliderRef
        {
            set => value = ignoredCollider.Value;
        }

        public float CurrentRightHitAngle => SetCurrentRightHitAngle();
        public float CurrentLeftHitAngle => SetCurrentLeftHitAngle();

        public float CurrentRightHitAngleRef
        {
            set => value = currentRightHitAngle.Value;
        }

        public float CurrentLeftHitAngleRef
        {
            set => value = currentLeftHitAngle.Value;
        }

        public Vector2 CurrentRightHitPoint => SetCurrentRightHitPoint();
        public Vector2 CurrentLeftHitPoint => SetCurrentLeftHitPoint();

        public Vector2 CurrentRightHitPointRef
        {
            set => value = currentRightHitPoint.Value;
        }

        public Vector2 CurrentLeftHitPointRef
        {
            set => value = currentLeftHitPoint.Value;
        }

        public bool IsGroundedRef
        {
            set => value = isGrounded.Value;
        }
        
        public float Friction { get; set; }

        public float FrictionRef
        {
            set => value = friction.Value;
        }

        public bool OnMovingPlatformRef
        {
            set => value = onMovingPlatform.Value;
        }

        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;

        public GameObject StandingOnLastFrameRef
        {
            set => value = standingOnLastFrame.Value;
        }

        public bool IsStandingOnLastFrameNotNull => state.StandingOnLastFrame != null;

        public bool IsStandingOnLastFrameNotNullRef
        {
            set => value = isStandingOnLastFrameNotNull.Value;
        }

        public Collider2D StandingOnColliderRef
        {
            set => value = standingOnCollider.Value;
        }

        public Vector2 ColliderBottomCenterPosition => SetColliderBottomCenterPosition();

        public Vector2 ColliderBottomCenterPositionRef
        {
            set => value = colliderBottomCenterPosition.Value;
        }

        public int DownHitsStorageSmallestDistanceIndex { get; set; }

        public int DownHitsStorageSmallestDistanceIndexRef
        {
            set => value = downHitsStorageSmallestDistanceIndex.Value;
        }
        
        public bool DownHitConnected { get; set; }

        public bool DownHitConnectedRef
        {
            set => value = downHitConnected.Value;
        }
    }
}
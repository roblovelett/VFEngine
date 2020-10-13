using System.Collections.Generic;
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

        /* fields */
        [SerializeField] private Vector2Reference boxColliderSize;
        [SerializeField] private Vector2Reference boxColliderOffset;
        [SerializeField] private Vector2Reference boxColliderBoundsCenter;
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

        public RaycastHit2D[] UpHitStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] RightHitStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] DownHitStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D[] LeftHitStorage { get; set; } = new RaycastHit2D[0];
        public RaycastHit2D UpHit { get; set; }
        public RaycastHit2D RightHit { get; set; }
        public RaycastHit2D DownHit { get; set; }
        public RaycastHit2D LeftHit { get; set; }
    }
}
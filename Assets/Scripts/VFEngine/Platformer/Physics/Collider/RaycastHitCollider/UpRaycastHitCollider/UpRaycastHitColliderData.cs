using ScriptableObjectArchitecture;
using ScriptableObjects.Variables;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "UpRaycastHitColliderData", menuName = PlatformerUpRaycastHitColliderDataPath,
        order = 0)]
    [InlineEditor]
    public class UpRaycastHitColliderData : ScriptableObject
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObjectReference character = null;

        #endregion

        [SerializeField] private BoolReference upHitConnected = new BoolReference();
        [SerializeField] private IntReference upHitsStorageLength = new IntReference();
        [SerializeField] private IntReference upHitsStorageCollidingIndex = new IntReference();
        [SerializeField] private IntReference currentUpHitsStorageIndex = new IntReference();
        [SerializeField] private RaycastReference raycastUpHitAt = new RaycastReference();
        [SerializeField] private BoolReference isCollidingAbove = new BoolReference();
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame = new BoolReference();
        private static readonly string UpRaycastHitColliderPath = $"{RaycastHitColliderPath}UpRaycastHitCollider/";
        private static readonly string ModelAssetPath = $"{UpRaycastHitColliderPath}UpRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character.Value;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public RaycastHit2D CurrentUpRaycastHit { get; set; }

        #endregion

        public bool UpHitConnected
        {
            get => upHitConnected.Value;
            set => value = upHitConnected.Value;
        }

        public bool IsCollidingAbove
        {
            get => isCollidingAbove.Value;
            set => value = isCollidingAbove.Value;
        }

        public bool WasTouchingCeilingLastFrame
        {
            get => wasTouchingCeilingLastFrame.Value;
            set => value = wasTouchingCeilingLastFrame.Value;
        }

        public int UpHitsStorageLength
        {
            get => upHitsStorageLength.Value;
            set => value = upHitsStorageLength.Value;
        }

        public int UpHitsStorageCollidingIndex
        {
            get => upHitsStorageCollidingIndex.Value;
            set => value = upHitsStorageCollidingIndex.Value;
        }

        public int CurrentUpHitsStorageIndex
        {
            get => currentUpHitsStorageIndex.Value;
            set => value = currentUpHitsStorageIndex.Value;
        }

        public RaycastHit2D RaycastUpHitAt
        {
            get => raycastUpHitAt.Value.hit2D;
            set => raycastUpHitAt.Value = new Raycast(value);
        }

        public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];

        public static readonly string UpRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
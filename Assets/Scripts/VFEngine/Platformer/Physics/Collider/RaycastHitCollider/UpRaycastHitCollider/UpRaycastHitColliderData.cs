using ScriptableObjects.Atoms.Raycast.References;
using ScriptableObjects.Variables;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    [InlineEditor]
    public class UpRaycastHitColliderData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private IntReference numberOfVerticalRaysPerSide = new IntReference();
        [SerializeField] private RaycastReference currentUpRaycast = new RaycastReference();

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

        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public RaycastHit2D CurrentUpRaycast => currentUpRaycast.Value.hit2D;

        #endregion

        public bool UpHitConnected
        {
            set => value = upHitConnected.Value;
        }

        public bool IsCollidingAbove
        {
            get => isCollidingAbove.Value;
            set => value = isCollidingAbove.Value;
        }

        public bool WasTouchingCeilingLastFrame
        {
            set => value = wasTouchingCeilingLastFrame.Value;
        }

        public int UpHitsStorageLength
        {
            set => value = upHitsStorageLength.Value;
        }

        public int UpHitsStorageCollidingIndex
        {
            set => value = upHitsStorageCollidingIndex.Value;
        }

        public int CurrentUpHitsStorageIndex
        {
            get => currentUpHitsStorageIndex.Value;
            set => value = currentUpHitsStorageIndex.Value;
        }

        public Raycast RaycastUpHitAt
        {
            set => value = raycastUpHitAt.Value;
        }

        public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];

        public static readonly string UpRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
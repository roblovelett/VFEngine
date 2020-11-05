using ScriptableObjects.Atoms.RaycastHit2D.References;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider.UpRaycastHitCollider
{
    using static RaycastHitColliderData;
    using static ScriptableObjectExtensions;

    public class UpRaycastHitColliderData : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private IntReference numberOfVerticalRaysPerSide;
        [SerializeField] private RaycastHit2DReference currentUpRaycast;

        #endregion

        [SerializeField] private BoolReference upHitConnected;
        [SerializeField] private IntReference upHitsStorageCollidingIndex;
        [SerializeField] private IntReference currentUpHitsStorageIndex;
        [SerializeField] private RaycastHit2DReference raycastUpHitAt;
        [SerializeField] private BoolReference isCollidingAbove;
        [SerializeField] private BoolReference wasTouchingCeilingLastFrame;
        private static readonly string UpRaycastHitColliderPath = $"{RaycastHitColliderPath}UpRaycastHitCollider/";

        private static readonly string ModelAssetPath =
            $"{UpRaycastHitColliderPath}DefaultUpRaycastHitColliderModel.asset";

        #endregion

        #region properties

        #region dependencies

        public int NumberOfVerticalRaysPerSide => numberOfVerticalRaysPerSide.Value;
        public RaycastHit2D CurrentUpRaycast => currentUpRaycast.Value;

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

        public int UpHitsStorageCollidingIndex
        {
            set => value = upHitsStorageCollidingIndex.Value;
        }

        public int CurrentUpHitsStorageIndex
        {
            get => currentUpHitsStorageIndex.Value;
            set => value = currentUpHitsStorageIndex.Value;
        }

        public RaycastHit2D RaycastUpHitAt
        {
            set => value = raycastUpHitAt.Value;
        }

        public RaycastHit2D[] UpHitsStorage { get; set; } = new RaycastHit2D[0];

        public static readonly string UpRaycastHitColliderModelPath =
            $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
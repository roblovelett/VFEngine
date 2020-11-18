using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.References;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer

// ReSharper disable RedundantAssignment
namespace VFEngine.Platformer.Event.Raycast.DownRaycast
{
    using static RaycastData;
    using static ScriptableObjectExtensions;

    [InlineEditor]
    public class DownRaycastData : SerializedMonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;

        #endregion

        [SerializeField] private FloatReference downRayLength = new FloatReference();
        [SerializeField] private Vector2Reference currentDownRaycastOrigin = new Vector2Reference();
        [SerializeField] private Vector2Reference downRaycastFromLeft = new Vector2Reference();
        [SerializeField] private Vector2Reference downRaycastToRight = new Vector2Reference();
        [SerializeField] private RaycastReference currentDownRaycastHit = new RaycastReference();
        private static readonly string DownRaycastPath = $"{RaycastPath}DownRaycast/";
        private static readonly string ModelAssetPath = $"{DownRaycastPath}DownRaycastModel.asset";

        #endregion

        #region properties

        #region dependencies

        public GameObject Character => character;
        public Transform Transform { get; set; }
        public PlatformerRuntimeData RuntimeData { get; set; }
        public bool DrawRaycastGizmosControl { get; set; }
        public int CurrentDownHitsStorageIndex { get; set; }
        public int NumberOfVerticalRaysPerSide { get; set; }
        public float RayOffset { get; set; }
        public float BoundsHeight { get; set; }
        public Vector2 NewPosition { get; set; }
        public Vector2 BoundsBottomLeftCorner { get; set; }
        public Vector2 BoundsBottomRightCorner { get; set; }
        public Vector2 BoundsTopLeftCorner { get; set; }
        public Vector2 BoundsTopRightCorner { get; set; }
        public LayerMask RaysBelowLayerMaskPlatformsWithoutOneWay { get; set; }
        public LayerMask RaysBelowLayerMaskPlatforms { get; set; }

        #endregion

        public float DownRayLength
        {
            get => downRayLength.Value;
            set => value = downRayLength.Value;
        }

        public Vector2 CurrentDownRaycastOrigin
        {
            get => currentDownRaycastOrigin.Value;
            set => value = currentDownRaycastOrigin.Value;
        }

        public Vector2 DownRaycastFromLeft
        {
            get => downRaycastFromLeft.Value;
            set => value = downRaycastFromLeft.Value;
        }

        public Vector2 DownRaycastToRight
        {
            get => downRaycastToRight.Value;
            set => value = downRaycastToRight.Value;
        }

        public RaycastHit2D CurrentDownRaycastHit
        {
            get => currentDownRaycastHit.Value.hit2D;
            set => currentDownRaycastHit.Value = new ScriptableObjects.Variables.Raycast(value);
        }

        public static readonly string DownRaycastModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
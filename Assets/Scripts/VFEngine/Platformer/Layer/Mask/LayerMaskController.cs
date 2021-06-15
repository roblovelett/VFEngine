using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;

namespace VFEngine.Platformer.Layer.Mask
{
    using static GameObject;
    using static ScriptableObject;
    using static UniTask;

    public class LayerMaskController : MonoBehaviour
    {
        #region events

        #endregion

        #region properties

        public LayerMaskData Data { get; private set; }

        #endregion

        #region fields

        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskSettings settings;
        private RaycastData raycastData;

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            if (!Data) Data = CreateInstance<LayerMaskData>();
            Data.OnInitialize(settings);
        }

        private void Dependencies()
        {
            raycastData = GetComponent<RaycastController>().Data;
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            Dependencies();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private async UniTask RaysBelowPlatforms()
        {
            Data.OnSetRaysBelowPlatforms();
            await Yield();
        }

        private GameObject StandingOnLastFrame => raycastData.StandingOnLastFrame;

        private async UniTask SetSavedBelowLayerToStandingOnLastFrame()
        {
            Data.OnSetSavedBelowLayerToStandingOnLastFrame(StandingOnLastFrame.layer);
            await Yield();
        }

        private async UniTask RaysBelowToPlatformsWithoutMidHeight()
        {
            Data.OnSetRaysBelowToPlatformsWithoutMidHeight();
            await Yield();
        }

        private async UniTask SetMidHeightOneWayPlatformContainsStandingOnLastFrame()
        {
            Data.OnSetMidHeightOneWayPlatformContainsStandingOnLastFrame(StandingOnLastFrame);
            await Yield();
        }

        private async UniTask SetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            Data.OnSetRaysBelowToPlatformsAndOneWayOrStairs();
            await Yield();
        }

        private async UniTask SetRaysBelowToOneWayPlatform()
        {
            Data.OnSetRaysBelowToOneWayPlatform();
            await Yield();
        }

        private async UniTask SetStairsContainsStandingOnLastFrame()
        {
            Data.OnSetStairsContainsStandingOnLastFrame(StandingOnLastFrame);
            await Yield();
        }

        private GameObject StandingOn => raycastData.StandingOn;

        private async UniTask SetPlatformsContainStandingOn()
        {
            Data.OnSetPlatformsContainStandingOn(StandingOn);
            await Yield();
        }

        #endregion

        #region event handlers

        public async UniTask OnPlatformerSetRaysBelowPlatforms()
        {
            await RaysBelowPlatforms();
        }

        public async UniTask OnPlatformerSetSavedBelowLayerToStandingOnLastFrame()
        {
            await SetSavedBelowLayerToStandingOnLastFrame();
        }

        public async UniTask OnPlatformerSetMidHeightOneWayPlatformContainsStandingOnLastFrame()
        {
            await SetMidHeightOneWayPlatformContainsStandingOnLastFrame();
        }

        public async UniTask OnPlatformerSetRaysBelowToPlatformsWithoutMidHeight()
        {
            await RaysBelowToPlatformsWithoutMidHeight();
        }

        public async UniTask OnPlatformerSetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            await SetRaysBelowToPlatformsAndOneWayOrStairs();
        }

        public async UniTask OnPlatformerSetRaysBelowToOneWayPlatform()
        {
            await SetRaysBelowToOneWayPlatform();
        }

        public async UniTask OnPlatformerSetStairsContainsStandingOnLastFrame()
        {
            await SetStairsContainsStandingOnLastFrame();
        }

        public async UniTask OnPlatformerSetPlatformsContainStandingOn()
        {
            await SetPlatformsContainStandingOn();
        }

        #endregion
    }
}
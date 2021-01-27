using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.ScriptableObjects;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;

namespace VFEngine.Platformer.Layer.Mask
{
    using static GameObject;
    using static ScriptableObject;
    using static UniTask;

    public class LayerMaskController : SerializedMonoBehaviour
    {
        #region events

        #endregion

        #region properties

        [OdinSerialize] public LayerMaskData Data { get; private set; }

        #endregion

        #region fields

        [OdinSerialize] private GameObject character;
        [OdinSerialize] private LayerMaskSettings settings;
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

        private void SetDependencies()
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
            SetDependencies();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        private async UniTask SetRaysBelowPlatforms()
        {
            Data.OnSetRaysBelowPlatforms();
            await Yield();
        }

        private LayerMask StandingOnLastFrame => raycastData.StandingOnLastFrame.layer;
        private async UniTask SetSavedBelowLayerToStandingOnLastFrame()
        {
            Data.OnSetSavedBelowLayerToStandingOnLastFrame(StandingOnLastFrame);
            await Yield();
        }

        private async UniTask SetRaysBelowToPlatformsWithoutMidHeight()
        {
            Data.OnSetRaysBelowToPlatformsWithoutMidHeight();
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
        
        #endregion

        #region event handlers

        public async UniTask OnPlatformerSetRaysBelowPlatforms()
        {
            await SetRaysBelowPlatforms();
        }

        public async UniTask OnPlatformerSetSavedBelowLayerToStandingOnLastFrame()
        {
            await SetSavedBelowLayerToStandingOnLastFrame();
        }

        public async UniTask OnPlatformerSetRaysBelowToPlatformsWithoutMidHeight()
        {
            await SetRaysBelowToPlatformsWithoutMidHeight();
        }

        public async UniTask OnPlatformerSetRaysBelowToPlatformsAndOneWayOrStairs()
        {
            await SetRaysBelowToPlatformsAndOneWayOrStairs();
        }

        public async UniTask OnPlatformerSetRaysBelowToOneWayPlatform()
        {
            await SetRaysBelowToOneWayPlatform();
        }

        #endregion
    }
}

#region hide

/*private void InitializeFrame()
        {
            Data.OnInitializeFrame(ref character);
        }*/ /*

private void SetSavedLayer()
{
    Data.OnSetSavedLayer(ref character);
}

private void ResetLayerMask()
{
    Data.OnResetLayerMask(ref character);
}/*public async UniTask OnPlatformerInitializeFrame()
        {
            InitializeFrame();
            await Yield();
        }*/ /*

public async UniTask OnPlatformerSetSavedLayer()
{
    SetSavedLayer();
    await Yield();
}

public async UniTask OnPlatformerResetLayerMask()
{
    ResetLayerMask();
    await Yield();
}*/

#endregion
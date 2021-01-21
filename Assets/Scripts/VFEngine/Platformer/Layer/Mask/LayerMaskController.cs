using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;

namespace VFEngine.Platformer.Layer.Mask
{
    using static GameObject;
    using static ScriptableObject;

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

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            if (!Data) Data = CreateInstance<LayerMaskData>();
            Data.OnInitialize(settings);
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        #endregion

        #region event handlers

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
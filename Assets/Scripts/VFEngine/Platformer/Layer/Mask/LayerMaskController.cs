using Packages.BetterEvent;
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

        public BetterEventEntry initializedFrameForPlatformer;

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

        private void InitializeFrame()
        {
            Data.OnInitializeFrame(ref character);
        }

        #endregion

        #region event handlers

        public void OnPlatformerInitializeFrame()
        {
            InitializeFrame();
            initializedFrameForPlatformer.Invoke();
        }

        #endregion
    }
}
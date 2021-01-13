using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Layer.Mask.ScriptableObjects;

namespace VFEngine.Platformer.Layer.Mask
{
    using static GameObject;
    using static ScriptableObject;
    using static Physics2D;

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
            Data.Initialize(character, settings);
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
            Data.OnInitializeFrame(character.layer);
            character.layer = IgnoreRaycastLayer;
        }

        private LayerMask Saved => Data.Saved;

        private void SetLayerToSaved()
        {
            character.layer = Saved;
        }

        #endregion

        #region event handlers

        public void OnPlatformerInitializeFrame()
        {
            InitializeFrame();
        }

        public void OnPlatformerSetLayerMaskToSaved()
        {
            SetLayerToSaved();
        }

        #endregion
    }
}
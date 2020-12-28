using UnityEngine;
using VFEngine.Platformer.Physics;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Layer.Mask
{
    using static GameObject;
    using static ScriptableObject;

    public class LayerMaskController : MonoBehaviour
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private LayerMaskSettings settings;
        private LayerMaskModel layerMask;
        private PhysicsController physicsController;
        private PlatformerController platformerController;

        #endregion

        #region internal

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            physicsController = GetComponent<PhysicsController>();
            platformerController = GetComponent<PlatformerController>();
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            layerMask = new LayerMaskModel(character, settings, physicsController, platformerController);
        }
        #endregion

        #endregion

        #endregion

        #region properties

        public LayerMaskData Data => layerMask.Data;

        #region public methods

        #endregion

        #endregion
    }
}
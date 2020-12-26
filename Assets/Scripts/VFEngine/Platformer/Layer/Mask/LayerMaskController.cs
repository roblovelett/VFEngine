using UnityEngine;

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
            if (!character) character = Find("Character");
            if (!settings) settings = CreateInstance<LayerMaskSettings>();
            layerMask = new LayerMaskModel(settings, character);
        }

        /*private void Start()
        {
            //SetControllers();
            //SetDependencies();
        }*/

        /*private void SetControllers(){}*/
        
        /*private void SetDependencies()
        {
            //layerMask = GetComponent<LayerMaskController>().Data;
            //physics = GetComponent<PhysicsController>().Data;
            //platformer = GetComponent<PlatformerController>().Data;
        }*/

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
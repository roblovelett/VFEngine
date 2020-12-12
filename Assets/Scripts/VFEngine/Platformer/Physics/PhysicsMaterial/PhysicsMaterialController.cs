using UnityEngine;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.PhysicsMaterial
{
    using static ScriptableObject;

    public class PhysicsMaterialController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PhysicsMaterialSettings settings;
        private PhysicsMaterialData p;

        #endregion

        #region private methods

        #region initialization

        private void Awake()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            if (!settings) settings = CreateInstance<PhysicsMaterialSettings>();
            p = new PhysicsMaterialData();
            p.ApplySettings(settings);
        }

        #endregion

        #endregion

        #endregion

        #region properties

        public PhysicsMaterialData Data => p;

        #endregion
    }
}
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local
// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Event.Boxcast
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "BoxcastModel", menuName = PlatformerBoxcastModelPath, order = 0)]
    [InlineEditor]
    public class BoxcastModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Boxcast Data")] [SerializeField]
        private BoxcastData b = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            b.RuntimeData = b.Character.GetComponentNoAllocation<BoxcastController>().RuntimeData;
        }

        private void InitializeModel()
        {
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnInitialize()
        {
            Initialize();
        }
        
        #endregion

        #endregion
    }
}
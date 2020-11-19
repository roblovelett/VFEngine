using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static UniTaskExtensions;
    using static Vector3;
    using static Mathf;
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "RaycastHitColliderModel", menuName = PlatformerRaycastHitColliderModelPath, order = 0)]
    [InlineEditor]
    public class RaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Raycast Hit Collider Data")] [SerializeField]
        private RaycastHitColliderData r = null;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeData();
            InitializeModel();
        }

        private void InitializeData()
        {
            r.RuntimeData = r.Character.GetComponentNoAllocation<PlatformerController>().RuntimeData;
            r.RuntimeData.SetRaycastHitCollider(r.IgnoredCollider, r.ContactList);
        }

        private void InitializeModel()
        {
            r.CurrentRightRaycastHit = r.RuntimeData.rightRaycast.CurrentRightRaycastHit;
            r.CurrentLeftRaycastHit = r.RuntimeData.leftRaycast.CurrentLeftRaycastHit;
            ClearContactList();
        }

        private void AddRightHitToContactList()
        {
            r.ContactList.Add(r.CurrentRightRaycastHit);
        }

        private void AddLeftHitToContactList()
        {
            r.ContactList.Add(r.CurrentLeftRaycastHit);
        }

        private void ClearContactList()
        {
            r.ContactList.Clear();
        }

        private void ResetState()
        {
            ClearContactList();
        }

        private static float SetRaycastHitAngle(Vector2 normal, Transform t)
        {
            return Abs(Angle(normal, t.up));
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public void OnClearContactList()
        {
            ClearContactList();
        }

        public void OnAddRightHitToContactList()
        {
            AddRightHitToContactList();
        }

        public void OnAddLeftHitToContactList()
        {
            AddLeftHitToContactList();
        }

        public static float OnSetRaycastHitAngle(Vector2 normal, Transform t)
        {
            return SetRaycastHitAngle(normal, t);
        }

        public async UniTaskVoid OnInitialize()
        {
            Initialize();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

        public void OnResetState()
        {
            ResetState();
        }

        #endregion

        #endregion
    }
}
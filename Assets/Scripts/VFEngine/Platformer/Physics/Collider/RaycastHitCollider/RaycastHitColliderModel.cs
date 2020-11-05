using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static UniTaskExtensions;
    using static Vector3;
    using static Mathf;

    [CreateAssetMenu(fileName = "RaycastHitColliderModel",
        menuName = "VFEngine/Platformer/Physics/Raycast Hit Collider/Raycast Hit Collider Model", order = 0)]
    public class RaycastHitColliderModel : ScriptableObject, IModel
    {
        #region fields

        #region dependencies

        [LabelText("Raycast Hit Collider Data")] [SerializeField]
        private RaycastHitColliderData r;

        #endregion

        #region private methods

        private void Initialize()
        {
            InitializeModel();
        }

        private void InitializeModel()
        {
            ClearContactList();
        }

        private void AddRightHitToContactList()
        {
            r.ContactList.Add(r.CurrentRightRaycast);
        }

        private void AddLeftHitToContactList()
        {
            r.ContactList.Add(r.CurrentLeftRaycast);
        }

        private void ClearContactList()
        {
            r.ContactList.Clear();
        }

        private static float SetRaycastHitAngle(Vector2 normal, Transform t)
        {
            return Abs(Angle(normal, t.up));
        }

        #endregion

        #endregion

        #region properties

        #region public methods

        public async UniTaskVoid OnClearContactList()
        {
            ClearContactList();
            await SetYieldOrSwitchToThreadPoolAsync();
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

        #endregion

        #endregion
    }
}
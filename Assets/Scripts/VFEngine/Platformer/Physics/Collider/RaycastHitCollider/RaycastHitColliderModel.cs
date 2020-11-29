using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static UniTaskExtensions;
    using static Vector3;
    using static Mathf;
    using static ScriptableObject;

    [Serializable]
    public class RaycastHitColliderModel
    {
        #region fields

        #region dependencies

        [SerializeField] private GameObject character;
        [SerializeField] private RaycastHitColliderController raycastHitColliderController;
        [SerializeField] private RaycastController raycastController;
        private RaycastHitColliderData r;
        private RightRaycastData rightRaycast;
        private LeftRaycastData leftRaycast;

        #endregion

        #region private methods

        private void InitializeData()
        {
            r = new RaycastHitColliderData();
            if (!raycastHitColliderController && character)
                raycastHitColliderController = character.GetComponent<RaycastHitColliderController>();
            else if (raycastHitColliderController && !character) character = raycastHitColliderController.Character;
            if (!raycastController) character.GetComponent<RaycastController>();
            r.ContactList = CreateInstance<RaycastHitColliderContactList>();
        }

        private void InitializeModel()
        {
            rightRaycast = raycastController.RightRaycastModel.Data;
            leftRaycast = raycastController.LeftRaycastModel.Data;
            ClearContactList();
        }

        private void AddRightHitToContactList()
        {
            r.ContactList.Add(rightRaycast.CurrentRightRaycastHit);
        }

        private void AddLeftHitToContactList()
        {
            r.ContactList.Add(leftRaycast.CurrentLeftRaycastHit);
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

        public RaycastHitColliderData Data => r;

        #region public methods

        public void OnInitializeData()
        {
            InitializeData();
        }

        public async UniTaskVoid OnInitializeModel()
        {
            InitializeModel();
            await SetYieldOrSwitchToThreadPoolAsync();
        }

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

        public void OnResetState()
        {
            ResetState();
        }

        #endregion

        #endregion
    }
}
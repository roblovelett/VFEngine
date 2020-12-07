using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;
using UniTaskExtensions = VFEngine.Tools.UniTaskExtensions;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
    using static ScriptableObject;
    using static UniTaskExtensions;

    public class RaycastHitColliderController : MonoBehaviour
    {
        #region fields

        #region dependencies

        private RightRaycastController rightRaycastController;
        private LeftRaycastController leftRaycastController;
        private RaycastHitColliderData r;
        private RightRaycastData rightRaycast;
        private LeftRaycastData leftRaycast;

        #endregion

        #region private methods

        private void Awake()
        {
            SetControllers();
            InitializeData();
        }

        private void SetControllers()
        {
            rightRaycastController = GetComponent<RightRaycastController>();
            leftRaycastController = GetComponent<LeftRaycastController>();
        }
        
        private void InitializeData()
        {
            r = new RaycastHitColliderData
            {
                ContactList = new List<RaycastHit2D>()
            };
        }

        private void Start()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            rightRaycast = rightRaycastController.Data;
            leftRaycast = leftRaycastController.Data;
        }

        private void PlatformerInitializeFrame()
        {
            ClearContactList();
        }

        private void AddRightHitToContactList()
        {
            r.ContactList.Add(rightRaycast.CurrentRaycastHit);
        }

        private void AddLeftHitToContactList()
        {
            r.ContactList.Add(leftRaycast.CurrentRaycastHit);
        }

        private void ClearContactList()
        {
            r.ContactList.Clear();
        }

        #endregion

        #endregion

        #region properties

        public RaycastHitColliderData Data => r;

        #region public methods

        #region platformer

        public void OnPlatformerInitializeFrame()
        {
            PlatformerInitializeFrame();
        }
        
        #endregion
        public void OnAddRightHitToContactList()
        {
            AddRightHitToContactList();
        }

        public void OnAddLeftHitToContactList()
        {
            AddLeftHitToContactList();
        }

        /*public async UniTaskVoid OnResetState()
        {
            ResetState();
            await SetYieldOrSwitchToThreadPoolAsync();
        }*/

        #endregion

        #endregion
    }
}
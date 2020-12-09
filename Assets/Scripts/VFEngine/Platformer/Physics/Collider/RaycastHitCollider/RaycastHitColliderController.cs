using System.Collections.Generic;
using UnityEngine;
using VFEngine.Platformer.Event.Raycast.LeftRaycast;
using VFEngine.Platformer.Event.Raycast.RightRaycast;

// ReSharper disable ConvertToAutoPropertyWithPrivateSetter
namespace VFEngine.Platformer.Physics.Collider.RaycastHitCollider
{
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

        #region initialization

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
            r = new RaycastHitColliderData {ContactList = new List<RaycastHit2D>()};
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

        #endregion

        #region platformer

        private void PlatformerInitializeFrame()
        {
            ClearContactList();
        }

        private void PlatformerLeftRaycastHitWall()
        {
            AddLeftRaycastHitToContactList();
        }

        private void PlatformerRightRaycastHitWall()
        {
            AddRightRaycastHitToContactList();
        }

        #endregion

        private void ClearContactList()
        {
            r.ContactList.Clear();
        }

        private void AddLeftRaycastHitToContactList()
        {
            r.ContactList.Add(leftRaycast.CurrentRaycastHit);
        }

        private void AddRightRaycastHitToContactList()
        {
            r.ContactList.Add(rightRaycast.CurrentRaycastHit);
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

        public void OnPlatformerLeftRaycastHitWall()
        {
            PlatformerLeftRaycastHitWall();
        }

        public void OnPlatformerRightRaycastHitWall()
        {
            PlatformerRightRaycastHitWall();
        }

        #endregion

        #endregion

        #endregion
    }
}
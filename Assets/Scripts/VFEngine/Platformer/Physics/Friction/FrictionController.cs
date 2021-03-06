﻿using UnityEngine;
using VFEngine.Platformer.Physics.Friction.ScriptableObjects;

namespace VFEngine.Platformer.Physics.Friction
{
    using static ScriptableObject;

    public class FrictionController : MonoBehaviour
    {
        #region events

        #endregion

        #region properties

        public FrictionData Data { get; private set; }

        #endregion

        #region fields

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!Data) Data = CreateInstance<FrictionData>();
            Data.OnInitialize();
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

        #endregion

        #region event handlers

        #endregion
    }
}
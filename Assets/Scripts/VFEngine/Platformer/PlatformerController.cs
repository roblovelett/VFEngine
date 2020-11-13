﻿using UnityEngine;
using VFEngine.Tools;

// ReSharper disable RedundantDefaultMemberInitializer
namespace VFEngine.Platformer
{
    using static PlatformerData;
    using static ScriptableObjectExtensions;
    using static Debug;

    public class PlatformerController : MonoBehaviour, IController
    {
        #region fields

        #region dependencies

        [SerializeField] private PlatformerModel model = null;

        #endregion

        #region private methods

        private void Awake()
        {
            if (!model) model = LoadData(ModelPath) as PlatformerModel;
            Assert(model != null, nameof(model) + " != null");
            model.OnInitialize();
        }

        private void FixedUpdate()
        {
            model.OnRunPlatformer();
        }

        #endregion

        #endregion
    }
}
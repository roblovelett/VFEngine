﻿using Sirenix.OdinInspector;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Layer.Mask.ScriptableObjects
{
    using static ScriptableObjectExtensions;

    [CreateAssetMenu(fileName = "LayerMaskSettings", menuName = PlatformerLayerMaskSettingsPath, order = 0)]
    [InlineEditor]
    public class LayerMaskSettings : ScriptableObject
    {
        #region properties

        [SerializeField] public LayerMask ground;
        [SerializeField] public LayerMask oneWayPlatform;
        [SerializeField] public LayerMask characterCollision;
        [SerializeField] public LayerMask ladder;
        [SerializeField] public LayerMask character;
        [SerializeField] public LayerMask standOnCollision;
        [SerializeField] public LayerMask interactive;

        #endregion
    }
}
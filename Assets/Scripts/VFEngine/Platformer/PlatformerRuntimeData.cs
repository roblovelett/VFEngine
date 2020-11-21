using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer
{
    public class PlatformerRuntimeData : ScriptableObject
    {
        #region properties

        public Platformer platformer;

        public struct Platformer
        {
            public PlatformerController Controller { get; set; }
            public Transform Transform { get; set; }
        }

        #endregion

        #region public methods

        public void SetPlatformerController(PlatformerController controller)
        {
            platformer.Controller = controller;
        }

        public void SetPlatformer(Transform transform)
        {
            platformer.Transform = transform;
        }

        #endregion
    }
}
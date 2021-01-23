using UnityEngine;

namespace VFEngine.Platformer.Physics.MovingPlatform.ScriptableObjects
{
    using static Vector2;

    public class MovingPlatformData : ScriptableObject
    {
        public Vector2 CurrentSpeed { get; private set; }

        public void OnInitialize()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeDefault();
        }

        private void InitializeDefault()
        {
            CurrentSpeed = zero;
        }
    }
}
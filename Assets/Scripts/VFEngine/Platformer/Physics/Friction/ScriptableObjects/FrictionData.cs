using UnityEngine;

namespace VFEngine.Platformer.Physics.Friction.ScriptableObjects
{
    public class FrictionData : ScriptableObject
    {
        public float Friction { get; private set; }
        
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
            Friction = 0;
        }
    }
}
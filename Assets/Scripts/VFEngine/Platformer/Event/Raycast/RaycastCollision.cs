using UnityEngine;

namespace VFEngine.Platformer.Event.Raycast
{
    public struct RaycastCollision
    {
        #region fields

        #region private methods

        private void Initialize()
        {
            Reset();
            GroundLayer = 0;
        }

        private void Reset()
        {
            Above = false;
            Right = false;
            Below = false;
            Left = false;
            OnGround = false;
            GroundDirection = 0;
            GroundAngle = 0;
            HorizontalHit = new RaycastHit2D();
            VerticalHit = new RaycastHit2D();
        }

        #endregion

        #endregion

        #region properties

        public bool Above { get; set; }
        public bool Right { get; set; }
        public bool Below { get; set; }
        public bool Left { get; set; }
        public bool OnGround { get; set; }
        public bool OnSlope => OnGround && GroundAngle != 0;
        public int GroundDirection { get; set; }
        public int GroundLayer { get; set; }
        public float GroundAngle { get; set; }
        public RaycastHit2D HorizontalHit { get; set; }
        public RaycastHit2D VerticalHit { get; set; }

        #region public methods

        public void OnInitialize()
        {
            Initialize();
        }

        #endregion

        #endregion
    }
}
using UnityEngine;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace VFEngine.Platformer.Event.Raycast
{
    public struct RaycastCollision
    {
        #region fields

        #region private methods

        /*private void DownHit(float groundAngle, int groundDirection, int groundLayer, RaycastHit2D hit)
        {
            OnGround = true;
            GroundAngle = groundAngle;
            GroundDirection = groundDirection;
            GroundLayer = groundLayer;
            VerticalHit = hit;
            Below = true;
        }*/

        #endregion

        #endregion

        #region properties

        public bool Above { get; set; }
        public bool Right { get; set; }
        public bool Below { get; set; }
        public bool Left { get; set; }
        public bool OnGround { get; set; }
        public bool OnSlope { get; set; }
        public int GroundDirection { get; set; }
        public int GroundLayer { get; set; }
        public float GroundAngle { get; set; }
        public RaycastHit2D HorizontalHit { get; set; }
        public RaycastHit2D VerticalHit { get; set; }

        #region public methods
        
        public void Initialize()
        {
            Reset();
            GroundLayer = 0;
            OnSlope = OnGround && GroundAngle != 0;
        }

        public void Reset()
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

        /*public void OnDownHit(float groundAngle, int groundDirection, int groundLayer, RaycastHit2D hit)
        {
            DownHit(groundAngle, groundDirection, groundLayer, hit);
        }*/

        #endregion

        #endregion
    }
}
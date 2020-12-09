namespace VFEngine.Platformer
{
    public class PlatformerData
    {
        #region properties
        
        //public bool MovingPlatform //=> downRaycastHitCollider.HasMovingPlatform;
        //private bool MovingPlatformHasSpeed //=> !SpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed);
        //private static bool TimeIsActive //=> !TimeLteZero();
        //private bool MovingPlatformHasSpeedOnAxis //=> !AxisSpeedNan(downRaycastHitCollider.MovingPlatformCurrentSpeed);
        //private bool NotTouchingCeilingLastFrame //=> !upRaycastHitCollider.WasTouchingCeilingLastFrame;
        public bool TestPlatform { get; set; }//=> TimeIsActive && MovingPlatformHasSpeedOnAxis && NotTouchingCeilingLastFrame;

        #region dependencies
        public bool DisplayWarningsControl { get; private set; }

        #endregion

        public float Tolerance { get; } = 0f;

        #region public methods

        public void ApplySettings(PlatformerSettings settings)
        {
            DisplayWarningsControl = settings.displayWarningsControl;
        }

        #endregion

        #endregion
    }
}
namespace VFEngine.Platformer.Physics
{
    public class PhysicsState
    {
        public bool IsFalling { get; private set; }
        public bool IsJumping { get; private set; }
        public bool GravityActive { get; private set; }

        public void SetIsFalling(bool isFalling)
        {
            IsFalling = isFalling;
        }

        public void SetIsJumping(bool isJumping)
        {
            IsJumping = isJumping;
        }

        public void SetGravityActive(bool gravity)
        {
            GravityActive = gravity;
        }

        public void Reset()
        {
            IsFalling = true;
        }
    }
}
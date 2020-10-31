namespace VFEngine.Platformer.Event.Raycast.StickyRaycast
{
    public class StickyRaycastState
    {
        public bool IsCastingToLeft { get; private set; }

        public void SetCastToLeft(bool cast)
        {
            IsCastingToLeft = cast;
        }

        public void Reset()
        {
            SetCastToLeft(false);
        }
    }
}
namespace VFEngine.Platformer.Event.Boxcast.SafetyBoxcast
{
    public class SafetyBoxcastState
    {
        public bool HasSafetyBoxcast { get; private set; }

        public void SetHasSafetyBoxcast(bool has)
        {
            HasSafetyBoxcast = has;
        }

        public void Reset()
        {
            SetHasSafetyBoxcast(false);
        }
    }
}
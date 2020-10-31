namespace VFEngine.Platformer.Event.Raycast
{
    public class RaycastState
    {
        public bool HasDistanceToGroundRaycast { get; private set; }

        public void SetHasDistanceToGroundRaycast(bool has)
        {
            HasDistanceToGroundRaycast = has;
        }

        public void Reset()
        {
            SetHasDistanceToGroundRaycast(false);
        }
    }
}
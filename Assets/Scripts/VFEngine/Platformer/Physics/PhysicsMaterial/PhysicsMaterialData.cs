namespace VFEngine.Platformer.Physics.PhysicsMaterial
{
    public class PhysicsMaterialData
    {
        #region properties

        public float Friction { get; set; }

        #region public methods

        public void ApplySettings(PhysicsMaterialSettings settings)
        {
            Friction = settings.friction;
        }

        #endregion

        #endregion
    }
}
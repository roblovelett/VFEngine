using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
namespace VFEngine.Platformer.Physics
{
    public class PhysicsModel
    {
        #region fields

        #endregion

        #region properties

        public PhysicsData Data { get; }

        #region public methods

        #region constructors

        public PhysicsModel(PhysicsSettings settings, GameObject character)
        {
            Data = new PhysicsData(settings, character);
        }

        public PhysicsModel(PhysicsSettings settings)
        {
            Data = new PhysicsData(settings);
        }

        public PhysicsModel(GameObject character)
        {
            Data = new PhysicsData(character);
        }

        public PhysicsModel()
        {
            Data = new PhysicsData();
        }

        #endregion

        #endregion

        #endregion
    }
}
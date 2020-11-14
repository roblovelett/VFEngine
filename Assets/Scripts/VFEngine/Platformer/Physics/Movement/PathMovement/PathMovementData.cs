using UnityAtoms.BaseAtoms;
using UnityEngine;
using VFEngine.Tools;

namespace VFEngine.Platformer.Physics.Movement.PathMovement
{
    using static ScriptableObjectExtensions;

    public class PathMovementData : MonoBehaviour
    {
        #region fields

        #region dependencies

        #endregion

        [SerializeField] private Vector2Reference currentSpeed = new Vector2Reference();
        private static readonly string ModelAssetPath = $"{PathMovementPath}PathMovementModel.asset";

        #endregion

        #region properties

        #region dependencies

        #endregion

        public Vector2 CurrentSpeed => currentSpeed.Value;
        private const string PathMovementPath = "Physics/Movement/PathMovement/";
        public static readonly string PathMovementModelPath = $"{PlatformerScriptableObjectsPath}{ModelAssetPath}";

        #endregion
    }
}
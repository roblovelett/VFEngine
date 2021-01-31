using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VFEngine.Platformer.Physics.MovingPlatform.ScriptableObjects;

namespace VFEngine.Platformer.Physics.MovingPlatform
{
    using static ScriptableObject;

    public class MovingPlatformController : SerializedMonoBehaviour
    {
        #region events

        #endregion

        #region properties

        [OdinSerialize] public MovingPlatformData Data { get; private set; }

        #endregion

        #region fields

        #endregion

        #region initialization

        private void Initialize()
        {
            if (!Data) Data = CreateInstance<MovingPlatformData>();
            Data.OnInitialize();
        }

        #endregion

        #region unity events

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region public methods

        #endregion

        #region private methods

        #endregion

        #region event handlers

        #endregion
    }
}
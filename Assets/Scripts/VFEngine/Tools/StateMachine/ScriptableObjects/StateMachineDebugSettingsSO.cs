using UnityEngine;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public class StateMachineDebugSettingsSO : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private bool enableDebug;

        #endregion
        
        #region Properties
        
        internal bool Enabled => enableDebug;
        
        #endregion
    }
}
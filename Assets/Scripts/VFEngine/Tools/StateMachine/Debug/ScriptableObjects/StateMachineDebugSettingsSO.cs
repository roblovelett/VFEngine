using UnityEngine;
using VFEngine.Tools.StateMachine.Debug.ScriptableObjects.Data;

namespace VFEngine.Tools.StateMachine.Debug.ScriptableObjects
{
    using static Text;
    public class StateMachineDebugSettingsSO : ScriptableObject
    {
        [SerializeField, Tooltip(DebugToolTip)] 
        internal bool debugStateMachineControl;
        
        [SerializeField, Tooltip(DebugLogToolTip)]
        internal bool debugStateTransitionsControl;

        [SerializeField, Tooltip(ConditionsToolTip)]
        internal bool appendStateTransitionsConditionsInformation = true;
    }
}
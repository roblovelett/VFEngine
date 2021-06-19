using System;
using VFEngine.Tools.StateMachine.Debug.ScriptableObjects;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;

namespace VFEngine.Tools.StateMachine.Debug
{
    [Serializable]
    internal class Controller
    {
        private Model debug;
        internal bool HasDebugStateMachineControl => debug.HasDebugStateMachineControl;

        internal void TransitionEvaluation(string state)
        {
            debug.TransitionEvaluation(state);
        }

        /*internal void TransitionEvaluation(bool passed)
        {
            debug.TransitionEvaluation(passed);
        }*/

        internal void TransitionConditionResult(string conditionName, bool result, bool isMet)
        {
            debug.TransitionConditionResult(conditionName, result, isMet);
        }

        private bool CanInitializeDebugModel => debug == null;

        internal Controller(bool isUnityEditor, StateMachineController stateMachineController,
            StateMachineDebugSettingsSO stateMachineDebugSettingsSO)
        {
            if (CanInitializeDebugModel)
                debug = new Model(isUnityEditor, stateMachineController, stateMachineDebugSettingsSO);
        }

        internal void Awake()
        {
            debug.Awake();
        }
    }
}
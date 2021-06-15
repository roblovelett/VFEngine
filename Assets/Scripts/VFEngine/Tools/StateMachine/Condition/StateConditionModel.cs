using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateConditionData = VFEngine.Tools.StateMachine.Condition.Data.Data;
using StateConditionSO = VFEngine.Tools.StateMachine.Condition.ScriptableObjects.ConditionSO;

namespace VFEngine.Tools.StateMachine.Condition
{
    internal class Model
    {
        private readonly StateConditionData data;
        private bool CanCacheStatement => IsCached == false;

        internal void GetStatement(bool statement)
        {
            if (!CanCacheStatement) return;
            CacheStatement(statement);
        }

        private void CacheStatement(bool statement)
        {
            IsCached = true;
            CachedStatement = statement;
        }

        private bool IsCached
        {
            get => data.IsCached;
            set => data.IsCached = value;
        }

        internal bool CachedStatement
        {
            get => data.CachedStatement;
            private set => data.CachedStatement = value;
        }

        internal void ClearStatementCache()
        {
            IsCached = false;
        }

        internal Model(StateConditionSO stateConditionSO)
        {
            data = new StateConditionData(stateConditionSO);
        }

        internal Model(StateMachineController stateMachineController, StateConditionSO stateConditionSO,
            bool expectedResult)
        {
            data = new StateConditionData(stateMachineController, stateConditionSO, expectedResult);
        }

        private StateMachineController StateMachineController
        {
            set => data.StateMachineController = value;
        }

        internal void Awake(StateMachineController stateMachineController)
        {
            StateMachineController = stateMachineController;
        }

        internal void OnEnter()
        {
        }

        internal void OnExit()
        {
        }

        internal StateConditionSO StateConditionSO
        {
            get => data.StateConditionSO;
            private set => data.StateConditionSO = value;
        }

        internal void Initialize(StateConditionSO stateConditionSO)
        {
            StateConditionSO = stateConditionSO;
        }
    }
}
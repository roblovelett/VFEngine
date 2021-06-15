using VFEngine.Tools.StateMachine.State;
using StateMachineController = VFEngine.Tools.StateMachine.Controller;
using StateConditionSO = VFEngine.Tools.StateMachine.Condition.ScriptableObjects.ConditionSO;
namespace VFEngine.Tools.StateMachine.Condition
{
    internal class Controller : IStateController
    {
        private Model condition;
        internal StateConditionSO StateConditionSO => condition.StateConditionSO;
        
        
        internal void ClearStatementCache()
        {
            condition.ClearStatementCache();
        }

        internal void Awake(StateMachineController stateMachineController)
        {
            condition.Awake(stateMachineController);
        }

        

        //
        // Statement to evaluate
        //
        protected virtual bool Statement()
        {
            return false;
        }

        private bool CachedStatement => condition.CachedStatement;
        
        internal bool GetStatement()
        {
            condition.GetStatement(Statement());
            return CachedStatement;
        }

        //protected abstract bool Statement();

        public void OnEnter()
        {
            condition.OnEnter();
        }

        public void OnExit()
        {
            condition.OnExit();
        }

        private bool CanInitializeStateConditionModel => condition == null;
        
        
        internal void Initialize(StateConditionSO stateConditionSO)
        {
            if (CanInitializeStateConditionModel)
            {
                condition = new Model(stateConditionSO);
            }
            condition.Initialize(stateConditionSO);
        }

        internal Controller(StateMachineController stateMachineController, StateConditionSO stateConditionSO, bool expectedResult)
        {
            condition = new Model(stateMachineController, stateConditionSO, expectedResult);
        }
    }
}
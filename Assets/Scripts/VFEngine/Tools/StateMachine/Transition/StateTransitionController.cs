using VFEngine.Tools.StateMachine.State;using Controller = VFEngine.Tools.StateMachine.Controller;
using TransitionStateController = VFEngine.Tools.StateMachine.State.Controller;
using StateController = VFEngine.Tools.StateMachine.State.Controller;
using StateConditionController = VFEngine.Tools.StateMachine.Condition.Controller;

namespace VFEngine.Tools.StateMachine.Transition
{
    internal class Controller : IStateController
    {
        private Model transition;
        void IStateController.OnEnter()
        {
            transition.OnEnter();
        }

        void IStateController.OnExit()
        {
            transition.OnExit();
        }
        internal void ClearConditionsCache()
        {
            // do clear
        }

        private bool CanInitializeStateTransitionModel => transition == null;

        internal Controller()
        {
            if (CanInitializeStateTransitionModel) transition = new Model();
            transition.Initialize();
        }
        
        internal bool TryGetTransition(out TransitionStateController stateController)
        {
            stateController = null;//new TransitionStateController();
            return true;
        }

        internal Controller(StateController targetStateController, StateConditionController[] stateConditionControllers,
            int[] resultGroups = null)
        {
            if (CanInitializeStateTransitionModel) transition = new Model(targetStateController, stateConditionControllers, resultGroups);
            transition.Initialize(targetStateController, stateConditionControllers, resultGroups);
        }

        internal void Initialize(StateController targetStateController, StateConditionController[] stateConditionControllers, bool initializeResultGroups, int[] resultGroups = null)
        {
            if (CanInitializeStateTransitionModel)
            {
                transition = new Model(targetStateController, stateConditionControllers, resultGroups);
            }
            
        }
        
        


        /*
        private StateMachine.State.State nextState;
        private ConditionData[] conditions;
        private int[] resultGroups;
        private bool[] results;
        private bool getTransition;
        private int ResultGroupsAmount => resultGroups.Length;
        private int ConditionsAmount => conditions.Length;

        internal Controller()
        {
        }

        public Controller(StateMachine.State.State nextStateInternal, ConditionData[] conditionsInternal, int[] resultGroupsInternal = null)
        {
            Initialize();

            void Initialize()
            {
                nextState = nextStateInternal;
                conditions = conditionsInternal;
                SetResultGroups();
                SetResults();

                void SetResultGroups()
                {
                    var newResultGroups = new int[1];
                    var setResultGroups = resultGroupsInternal != null && resultGroupsInternal.Length > 0;
                    resultGroups = setResultGroups ? resultGroupsInternal : newResultGroups;
                }

                void SetResults()
                {
                    results = new bool[ResultGroupsAmount];
                }
            }
        }

        private void Awake()
        {
            getTransition = GetTransition();
        }

        public bool Get(out StateMachine.State.State state)
        {
            state = getTransition ? nextState : null;
            return state != null;
        }

        public void OnEnter()
        {
            SetConditions();

            void SetConditions()
            {
                for (var condition = 0; condition < ConditionsAmount; condition++)
                    conditions[condition].Condition.OnEnter();
            }
        }

        public void OnExit()
        {
            SetConditions();

            void SetConditions()
            {
                for (var condition = 0; condition < ConditionsAmount; condition++)
                    conditions[condition].Condition.OnExit();
            }
        }

        private bool GetTransition()
        {
            DebugEvaluateTransition();
            SetResultGroups();
            var shouldReturn = SetReturn();
            DebugOnTransitionEnd();
            return shouldReturn;

            void DebugEvaluateTransition()
            {
#if UNITY_EDITOR
                var transitionInternal = nextState.Origin.name;
                //nextState.Machine.debug.EvaluateTransition(transitionInternal);
#endif
            }

            void SetResultGroups()
            {
                for (int currentResultGroup = 0, currentCondition = 0;
                    currentResultGroup < ResultGroupsAmount;
                    currentResultGroup++)
                {
                    var hasCurrentResultGroup = results[currentResultGroup];
                    SetResults(currentResultGroup, currentCondition, hasCurrentResultGroup);
                }
            }

            void SetResults(int resultGroup, int condition, bool hasResultGroup)
            {
                for (var currentResult = 0; currentResult < resultGroup; currentResult++, condition++)
                {
                    var isFirstCurrentResult = currentResult == 0;
                    var currentConditionMet = conditions[condition].IsMet();
                    var firstCurrentConditionMet = isFirstCurrentResult && currentConditionMet;
                    var currentConditionMetHasResultGroup = hasResultGroup && currentConditionMet;
                    SetResult(currentResult, isFirstCurrentResult, firstCurrentConditionMet,
                        currentConditionMetHasResultGroup);
                }
            }

            void SetResult(int result, bool isFirstResult, bool firstConditionMet, bool conditionMetHasResultGroup)
            {
                results[result] = isFirstResult ? firstConditionMet : conditionMetHasResultGroup;
            }

            bool SetReturn()
            {
                var shouldReturnInternal = false;
                for (var resultGroup = 0; resultGroup < ResultGroupsAmount && !shouldReturnInternal; resultGroup++)
                    shouldReturnInternal = results[resultGroup];
                return shouldReturnInternal;
            }

            void DebugOnTransitionEnd()
            {
#if UNITY_EDITOR
                //nextState.Machine.debug.OnTransitionEnd(shouldReturn, nextState.Actions);
#endif
            }
        }

        internal void Clear()
        {
            for (var condition = 0; condition < ConditionsAmount; condition++) conditions[condition].Condition.Clear();
        }
        
        */

        
    }
}
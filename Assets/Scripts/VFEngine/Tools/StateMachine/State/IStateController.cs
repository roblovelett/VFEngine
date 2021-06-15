namespace VFEngine.Tools.StateMachine.State
{
    internal interface IStateController
    {
        void OnEnter();
        void OnExit();
    }
}
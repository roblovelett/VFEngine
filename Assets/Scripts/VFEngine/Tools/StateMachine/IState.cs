namespace VFEngine.Tools.StateMachine
{
    internal interface IState
    {
        void Enter();
        void Exit();
    }
}
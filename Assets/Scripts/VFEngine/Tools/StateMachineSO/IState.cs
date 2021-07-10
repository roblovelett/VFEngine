namespace VFEngine.Tools.StateMachineSO
{
	internal interface IState
	{
		void Enter();
		void Update();
		void Exit();
		void Awake(StateMachine stateMachine);
	}
}

﻿namespace VFEngine.Tools.StateMachineSO
{
	internal interface IState
	{
		void Enter();
		void Update();
		void Exit();
	}
}

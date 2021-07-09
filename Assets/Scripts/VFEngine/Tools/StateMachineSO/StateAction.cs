
using VFEngine.Tools.StateMachineSO.ScriptableObjects;

namespace VFEngine.Tools.StateMachineSO
{
	/// <summary>
	/// An object representing an action.
	/// </summary>
	public abstract class StateAction : IState
	{
		internal StateActionSO _originSO;

		/// <summary>
		/// Use this property to access shared data from the <see cref="StateActionSO"/> that corresponds to this <see cref="StateAction"/>
		/// </summary>
		protected StateActionSO OriginSO => _originSO;

		/// <summary>
		/// Called every frame the <see cref="StateMachine"/> is in a <see cref="State"/> with this <see cref="StateAction"/>.
		/// </summary>
		//public abstract void OnUpdate();

		public virtual void Awake(StateMachine stateMachine)
		{
			// init
		}

		public virtual void Enter()
		{
			//throw new System.NotImplementedException();
		}

		public virtual void Update()
		{
			//throw new System.NotImplementedException();
		}

		public virtual void Exit()
		{
			//throw new System.NotImplementedException();
		}public enum SpecificMoment
		{
			OnStateEnter, OnStateExit, OnUpdate,
		}
	}
}

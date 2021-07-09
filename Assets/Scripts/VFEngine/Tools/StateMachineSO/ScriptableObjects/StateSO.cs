using System.Collections.Generic;
using UnityEngine;

namespace VFEngine.Tools.StateMachineSO.ScriptableObjects
{
	[CreateAssetMenu(fileName = "New State", menuName = "State Machine SO/State", order = 0)]
	public class StateSO : ScriptableObject
	{
		[SerializeField] private StateActionSO[] _actions = null;

		/// <summary>
		/// Will create a new state or return an existing one inside <paramref name="createdInstances"/>.
		/// </summary>
		internal State GetState(VFEngine.Tools.StateMachineSO.StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
		{
			if (createdInstances.TryGetValue(this, out var obj))
				return (State)obj;

			var state = new State();
			createdInstances.Add(this, state);

			state.OriginSO = this;
			state.StateMachine = stateMachine;
			state.Transitions = new StateTransition[0];
			state.Actions = GetActions(_actions, stateMachine, createdInstances);

			return state;
		}

		private static StateAction[] GetActions(StateActionSO[] scriptableActions,
			VFEngine.Tools.StateMachineSO.StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
		{
			int count = scriptableActions.Length;
			var actions = new StateAction[count];
			for (int i = 0; i < count; i++)
				actions[i] = scriptableActions[i].GetAction(stateMachine, createdInstances);

			return actions;
		}
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityGameObject = UnityEngine.GameObject;

namespace VFEngine.Tools.StateMachine.ScriptableObjects
{
    public class StateSO : ScriptableObject
    {

    }
}

/*public class StateSO : BaseState
{
    public StateSO(IEnumerable<string> toStates, IEnumerable<string> transitions, bool canExit, string name, Hash128 hash, UnityGameObject gameObject, StateMachine stateMachine) : base(toStates, transitions, canExit, name, hash, gameObject, stateMachine)
    {
        
    }
    
    

    public override void Input()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void Render()
    {
        throw new System.NotImplementedException();
    }

    public override void Pause()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}*/
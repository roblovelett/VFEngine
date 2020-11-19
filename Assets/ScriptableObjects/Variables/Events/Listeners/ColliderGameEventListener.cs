using ScriptableObjectArchitecture;
using ScriptableObjects.Variables.Events.Game_Events;
using ScriptableObjects.Variables.Events.Responses;
using UnityEngine;

namespace ScriptableObjects.Variables.Events.Listeners
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Collision")]
    public sealed class
        CollisionGameEventListener : BaseGameEventListener<Collision, CollisionGameEvent, CollisionUnityEvent>
    {
    }
}
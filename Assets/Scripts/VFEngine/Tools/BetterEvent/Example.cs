using Sirenix.OdinInspector;
using UnityEngine;

namespace VFEngine.Tools.BetterEvent
{
    public class Example : SerializedMonoBehaviour
    {
        public BetterEvent myEvent;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) myEvent.Invoke();
        }
    }
}
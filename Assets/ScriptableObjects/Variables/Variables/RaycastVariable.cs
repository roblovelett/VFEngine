using ScriptableObjectArchitecture;
using UnityEngine;

namespace ScriptableObjects.Variables.Variables
{
    using static SOArchitecture_Utility;

    [CreateAssetMenu(fileName = "RaycastVariable.asset", menuName = VARIABLE_SUBMENU + "Raycast", order = 120)]
    public class RaycastVariable : BaseVariable<Raycast>
    {
    }
}
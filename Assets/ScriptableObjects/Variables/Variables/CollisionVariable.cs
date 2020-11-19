using ScriptableObjectArchitecture;
using UnityEngine;

namespace ScriptableObjects.Variables.Variables
{
    [CreateAssetMenu(fileName = "CollisionVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Collision", order = 120)]
    public class CollisionVariable : BaseVariable<Collision>
    {
    }
}
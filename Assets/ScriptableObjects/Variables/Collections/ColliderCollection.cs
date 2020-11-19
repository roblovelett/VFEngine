using ScriptableObjectArchitecture;
using UnityEngine;

namespace ScriptableObjects.Variables.Collections
{
    [CreateAssetMenu(
        fileName = "CollisionCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Collision",
        order = 120)]
    public class CollisionCollection : Collection<Collision>
    {
    }
}
using ScriptableObjectArchitecture;
using UnityEngine;

namespace ScriptableObjects.Variables.Collections
{
    using static SOArchitecture_Utility;

    [CreateAssetMenu(fileName = "RaycastCollection.asset", menuName = COLLECTION_SUBMENU + "Raycast", order = 120)]
    public class RaycastCollection : Collection<Raycast>
    {
    }
}
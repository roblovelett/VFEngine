using UnityEngine;

namespace VFEngine.Platformer.Physics.Gravity.GravityPoint
{
    public class GravityPointController : MonoBehaviour
    {
        [SerializeField] private float gravityEffectRange;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, gravityEffectRange);
        }
    }
}
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using XaviGames.Bot;

namespace XaviGames.Player
{
    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _onCollisionEnter;

        [SerializeField]
        private float _minCollisionVelocity = 0.1f;

        [SerializeField]
        private LayerMask _ignoreLayer;

        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & _ignoreLayer) != 0)
            { 
                return;
            }

            if (collision.relativeVelocity.magnitude < _minCollisionVelocity)
            {
                return;
            }

            _onCollisionEnter?.Invoke();
        }
    }
}

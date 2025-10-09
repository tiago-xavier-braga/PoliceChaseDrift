using UnityEngine;
using XaviGames.Bot;

namespace XaviGames.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private float _collisionDamage = 1f;

        private void OnCollisionEnter(Collision collision)
        {
            GameObject parentObject = collision.gameObject.GetComponentInParent<BotController>()?.gameObject;

            if (parentObject != null)
            {
                Debug.Log("PlayerCollision: OnCollisionEnter with " + collision.gameObject.name);

                _playerHealth.TakeDamage(_collisionDamage);
            }
        }
    }
}

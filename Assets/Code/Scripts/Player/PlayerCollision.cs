using UnityEngine;
using XaviGames.Bot;

namespace XaviGames.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            GameObject parentObject = collision.gameObject.GetComponentInParent<BotController>()?.gameObject;

            if (parentObject != null)
            {
                Debug.Log("PlayerCollision: OnCollisionEnter with " + collision.gameObject.name);

                PlayerHealth playerHealth = PlayerController.Instance.PlayerHealth;

                playerHealth.TakeDamage(1f);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using XaviGames.Shared;

namespace XaviGames.Player
{
    [CreateAssetMenu(fileName = "PlayerHealth", menuName = "Xavi Games/Player/PlayerHealth")]
    public class PlayerHealth : ScriptableObject
    {
        [SerializeField]
        private int _health;

        [field: SerializeField]
        [field: ReadOnly]
        public int Health { get; private set; }

        public UnityAction<int> OnHealthChanged;

        private void OnEnable()
        {
            Health = _health;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("Player took damage: " + damage);
            Health = Mathf.Max(0, Health - damage);
            OnHealthChanged?.Invoke(Health);
        }

        public void Heal(int amount)
        {
            Debug.Log("Player healed: " + amount);
            Health += amount;
            OnHealthChanged?.Invoke(Health);
        }

        public void SetHealth(int health)
        {
            Debug.Log("Player health set to: " + health);
            Health = health;
            OnHealthChanged?.Invoke(Health);
        }

        public void ResetHealth()
        {
            Debug.Log("Player health reset to: " + _health);
            Health = _health;
            OnHealthChanged?.Invoke(Health);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using XaviGames.Shared;

namespace XaviGames.Player
{
    [CreateAssetMenu(fileName = "PlayerHealth", menuName = "Xavi Games/Player/PlayerHealth")]
    public class PlayerHealth : ScriptableObject
    {
        [SerializeField]
        private float _health;

        [field: SerializeField]
        [field: ReadOnly]
        public float Health { get; private set; }

        public UnityAction<float> OnHealthChanged;

        private void OnEnable()
        {
            Health = _health;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Player took damage: " + damage);
            Health = Mathf.Max(0, Health - damage);
            OnHealthChanged?.Invoke(Health);
        }

        public void Heal(float amount)
        {
            Debug.Log("Player healed: " + amount);
            Health += amount;
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

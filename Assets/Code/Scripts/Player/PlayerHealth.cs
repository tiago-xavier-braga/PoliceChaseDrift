using UnityEngine;
using UnityEngine.Events;
using XaviEssencials.Runtime;
using XaviGames.Events;
using XaviGames.Manager;

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
            Health = Mathf.Max(0, Health - damage);
            OnHealthChanged?.Invoke(Health);
        }

        public void Heal(float amount)
        {
            Health += amount;
            OnHealthChanged?.Invoke(Health);
        }
    }
}

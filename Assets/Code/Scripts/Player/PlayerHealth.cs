using UnityEngine;
using UnityEngine.Events;
using XaviEssencials.Runtime;
using XaviGames.ObjectVariable;

namespace XaviGames.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable _playerHealthVariable;

        private float _health => _playerHealthVariable.Value;

        public UnityAction<float> OnHealthChanged;

        public void TakeDamage(float damage)
        {
            _playerHealthVariable.SetValue(Mathf.Max(0, _health - damage));
            OnHealthChanged?.Invoke(_health);
        }

        public void Heal(float amount)
        {
            _playerHealthVariable.SetValue(_health + amount);
            OnHealthChanged?.Invoke(_health);
        }
    }
}

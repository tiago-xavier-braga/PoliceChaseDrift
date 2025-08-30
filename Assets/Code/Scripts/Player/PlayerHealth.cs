using UnityEngine;
using UnityEngine.Events;
using XaviEssencials.Runtime;
using XaviGames.Manager;
using XaviGames.ObjectVariable;

namespace XaviGames.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable _playerHealthVariable;

        [SerializeField]
        private BoolVariable _isMatchStarted;

        [SerializeField]
        private MatchManager _matchManager;

        public void TakeDamage(float damage)
        {
            _playerHealthVariable.SetValue(Mathf.Max(0, _playerHealthVariable.Value - damage));
            HandleHealthChanged();
        }

        public void Heal(float amount)
        {
            _playerHealthVariable.SetValue(_playerHealthVariable.Value + amount);
        }

        private void HandleHealthChanged()
        {
            if (!_isMatchStarted.Value)
            {
                return;
            }

            if (_playerHealthVariable.Value <= 0)
            {
                _matchManager.FinishMatch();
            }
        }
    }
}

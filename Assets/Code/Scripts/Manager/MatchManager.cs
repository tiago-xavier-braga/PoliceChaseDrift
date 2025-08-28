using UnityEngine;
using XaviEssencials.Runtime;
using XaviEssencials.Shared;
using XaviGames.ObjectVariable;
using XaviGames.Player;

namespace XaviGames.Manager
{
    public class MatchManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private BoolVariable _isMatchStarted;

        public void OnEnable()
        {
            _playerHealth.OnHealthChanged += HandleHealthChanged;
        }

        public void OnDisable()
        {
            _playerHealth.OnHealthChanged -= HandleHealthChanged;
        }


        private void HandleHealthChanged(float value)
        {
            if (!_isMatchStarted.Value)
            {
                return;
            }

            if (value <= 0)
            {
                FinishMatch();
            }
        }

        [Button("Start", true)]
        private void StartMatch()
        {
            _isMatchStarted.SetValue(true);
            Debug.Log("Match Started");
        }

        [Button("Finish", true)]
        private void FinishMatch()
        {
            _isMatchStarted.SetValue(false);
            Debug.Log("Match Finished");
        }
    }
}

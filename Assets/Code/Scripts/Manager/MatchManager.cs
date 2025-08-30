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

        [Button("Finish", true)]
        public void FinishMatch()
        {
            _isMatchStarted.SetValue(false);
            Debug.Log("Match Finished");
        }

        [Button("Start", true)]
        private void StartMatch()
        {
            _isMatchStarted.SetValue(true);
            Debug.Log("Match Started");
        }


    }
}

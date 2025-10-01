using UnityEngine;
using UnityEngine.InputSystem;
using XaviEssencials.Runtime;
using XaviEssencials.Shared;
using XaviGames.ObjectVariable;
using XaviGames.Player;
using XaviGames.Ui;

namespace XaviGames.Manager
{
    public class MatchManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private BoolVariable _isMatchStarted;

        [SerializeField]
        private BoolVariable _isReadyStart;

        private void Start()
        {
            LoadingCanvasController.Instance.DisableLoading();
        }

        [Button("Start", true)]
        public void StartMatch()
        {
            if (_isMatchStarted.Value)
            {
                return;
            }

            if (!_isReadyStart.Value)
            {
                return;
            }

            _isMatchStarted.SetValue(true);
            _isReadyStart.SetValue(false);
            Debug.Log("Match Started");
        }

        [Button("Finish", true)]
        public void FinishMatch()
        {
            _isMatchStarted.SetValue(false);
            Debug.Log("Match Finished");
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            var moveInput = context.ReadValue<Vector2>();
            
            if (moveInput.y > 0)
            {
                StartMatch();
            }
        }
    }
}

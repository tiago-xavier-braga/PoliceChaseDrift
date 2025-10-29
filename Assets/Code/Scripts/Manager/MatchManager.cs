using UnityEngine;
using XaviGames.Player;
using XaviGames.Shared;
using XaviGames.Ui;
using XaviGames.UICore;

namespace XaviGames.Manager
{
    public class MatchManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [SerializeField]
        private CanvasGroupController _initialMenuCanvas;

        [SerializeField]
        private CanvasGroupController _gameOverCanvas;

        [SerializeField]
        private CameraController _cameraController;

        private GameState _gameState = GameState.None;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
            _playerHealth.OnHealthChanged += HandleHelthCHanged;
        }


        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
            _playerHealth.OnHealthChanged -= HandleHelthCHanged;
        }

        private void Start()
        {
            LoadingCanvasController.Instance.DisableLoading();
        }

        [Button("Start", true)]
        public void StartMatch()
        {
            if (_gameState == GameState.InGame)
            {
                return;
            }

            _cameraController.StartInitialAnimation();
            _initialMenuCanvas.DisableCanvas();
            _onGameStateChanged.RaiseEvent(GameState.InGame);
        }

        [Button("Finish", true)]
        public void FinishMatch()
        {
            if (_gameState != GameState.InGame)
            {
                Debug.LogWarning("Match cannot be finished. Current state: " + _gameState);
                return;
            }

            _gameOverCanvas.EnableCanvas();
            _playerHealth.ResetHealth();
            _onGameStateChanged.RaiseEvent(GameState.GameOver);
            Debug.Log("Match Finished");
        }

        private void HandleGameStateChanged(object newState)
        {
            _gameState = (GameState)newState;
        }

        private void HandleHelthCHanged(float newHealth)
        {
            if (newHealth <= 0f)
            {
                FinishMatch();
            }
        }
    }
}

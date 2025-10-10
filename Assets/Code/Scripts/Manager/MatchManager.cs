using System;
using UnityEngine;
using UnityEngine.InputSystem;
using XaviEssencials.Shared;
using XaviGames.Events;
using XaviGames.Player;
using XaviGames.Ui;

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

        private GameState _gameState = GameState.None;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
            _playerHealth.OnHealthChanged += HandleHelthCHanged;
        }


        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
            _playerHealth.OnHealthChanged += HandleHelthCHanged;
        }

        private void Start()
        {
            LoadingCanvasController.Instance.DisableLoading();
        }

        [Button("Start", true)]
        public void StartMatch()
        {
            if (_gameState != GameState.Ready)
            {
                Debug.LogWarning("Match cannot be started. Current state: " + _gameState);
                return;
            }

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
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            var moveInput = context.ReadValue<Vector2>();

            if (moveInput.y > 0)
            {
                StartMatch();
            }
        }

        private void HandleGameStateChanged(object newState)
        {
            _gameState = (GameState)newState;
        }

        private void HandleHelthCHanged(float newHealth)
        {
            if (_gameState != GameState.InGame)
            {
                return;
            }

            if (newHealth <= 0f)
            {
                FinishMatch();
            }
        }
    }
}

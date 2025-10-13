using System;
using UnityEngine;
using UnityEngine.InputSystem;
using XaviEssencials.Shared;
using XaviGames.Car;
using XaviGames.Events;
using XaviGames.Manager;

namespace XaviGames.Player
{
    public class PlayerController : MonoBehaviour
    {
        [field: Header("Car References")]
        [field: SerializeField]
        public Transform CarTransform { get; private set; }

        [field: SerializeField]
        public CarMovementController CarMovementController { get; private set; }

        [SerializeField]
        private EventChannel _onGameStateChanged;

        private GameState _gameState = GameState.None;

        public static PlayerController Instance { get; private set; }

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
        }


        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            if (_gameState != GameState.InGame)
            {
                CarMovementController.OnMoveInput(Vector2.zero);
                return;
            }

            Vector2 inputVector = context.ReadValue<Vector2>();
            CarMovementController.OnMoveInput(inputVector);
        }

        public void OnHandbrake(InputAction.CallbackContext context)
        {
            if (_gameState != GameState.InGame)
            {
                return;
            }

            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    CarMovementController.OnHandbrake(true);
                    break;

                case InputActionPhase.Canceled:
                    CarMovementController.OnHandbrake(false);
                    break;
            }
        }

        private void HandleGameStateChanged(object state)
        {
            _gameState = (GameState)state;
        }

        [Button("Debug Mode")]
        private void DebugMode()
        {
            _gameState = GameState.InGame;
        }
    }
}

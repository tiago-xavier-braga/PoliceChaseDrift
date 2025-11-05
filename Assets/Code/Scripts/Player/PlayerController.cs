using UnityEngine;
using UnityEngine.InputSystem;
using XaviGames.Car;
using XaviGames.Manager;
using XaviGames.Shared;

namespace XaviGames.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private EventChannel _onGameStateChanged;

        [SerializeField]
        private EventChannel _onCarSelected;
        
        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private CarDatabase _carDatabase;

        [SerializeField]
        [ReadOnly]
        private GameState _gameState = GameState.None;

        private CarMovementController _carMovementController;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
            _onCarSelected.Subscribe(HandleCarSelected);
        }

        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
            _onCarSelected.Unsubscribe(HandleCarSelected);
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            if (_gameState != GameState.InGame)
            {
                return;
            }

            Vector2 inputVector = context.ReadValue<Vector2>();
            _carMovementController.OnMoveInput(inputVector);
        }

        private void HandleGameStateChanged(object state)
        {
            _gameState = (GameState)state;
        
            if (_gameState == GameState.GameOver)
            {
                _carMovementController.Block();
            }
        }

        private void HandleCarSelected(object carObject)
        {
            GameObject carGameObject = (GameObject)carObject;
            _carMovementController = carGameObject.GetComponent<CarMovementController>();

        }

        [Button("Debug Mode")]
        private void DebugMode()
        {
            _gameState = GameState.InGame;
        }
    }
}

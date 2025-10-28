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
        private PlayerData _playerData;

        [SerializeField]
        private CarDatabase _carDatabase;

        [SerializeField]
        private Transform _startPosition;

        [SerializeField]
        [ReadOnly]
        private CarParameter _currentCarParameter;

        public Transform CarTransform { get; private set; } = null;
        public CarMovementController CarMovementController { get; private set; } = null;

        private GameState _gameState = GameState.None;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
            SpawnCar();
        }

        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            if (CarMovementController == null)
            {
                return;
            }

            Vector2 inputVector = context.ReadValue<Vector2>();
            CarMovementController.OnMoveInput(inputVector);
        }

        public void SpawnCar()
        {
            _currentCarParameter = _carDatabase.GetCarParameterById(_playerData.CurrentCar);
            GameObject carPrefab = _currentCarParameter.CarPrefab;
            GameObject carObject = Instantiate(carPrefab, _startPosition.position, _startPosition.rotation);
            CarMovementController = carObject.GetComponent<CarMovementController>();
            CarTransform = carObject.transform;
        }

        private void HandleGameStateChanged(object state)
        {
            _gameState = (GameState)state;
        
            if (_gameState == GameState.GameOver)
            {
                CarMovementController.Block();
            }
        }

        [Button("Debug Mode")]
        private void DebugMode()
        {
            _gameState = GameState.InGame;
        }
    }
}

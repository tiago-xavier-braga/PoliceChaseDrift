using UnityEngine;
using XaviGames.Car;
using XaviGames.Manager;
using XaviGames.Player;
using XaviGames.Shared;
using static UnityEngine.GraphicsBuffer;

namespace XaviGames.Bot
{
    public class BotController : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField]
        private CarMovementController _carMovementController;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [Header("Tuning")]
        [SerializeField]
        private float _minTurnAngleDegrees = 5f;

        [SerializeField]
        private float _distanceThresholdMeters = 2f;

        [SerializeField]
        private float _steerInputMagnitude = 1f;

        [SerializeField]
        private float _forwardInput = 1f;

        [SerializeField]
        private float _reverseInput = -1f;

        [SerializeField]
        [ReadOnly]
        private GameState _gameState = GameState.None;

        private Transform _playerCarTransform;

        private void OnEnable()
        {

            _onGameStateChanged.Subscribe(HandleGameStateChanged);

            _gameState = _onGameStateChanged.Parameter != null 
                ? (GameState)_onGameStateChanged.Parameter : GameState.None;
        }

        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
        }

        private void FixedUpdate()
        {
            if (_gameState != GameState.InGame)
            {
                _carMovementController.OnMoveInput(Vector2.zero);
                return;
            }

            Transform carTransform = _carMovementController.transform;
            Vector3 directionToPlayer = _playerCarTransform.position - carTransform.position;
            float angleToPlayer = Vector3.SignedAngle(carTransform.forward, directionToPlayer, Vector3.up);

            Vector2 inputVector = Vector2.zero;

            float absAngle = Mathf.Abs(angleToPlayer);
            if (absAngle > _minTurnAngleDegrees)
            {
                inputVector.x = angleToPlayer > 0f ? _steerInputMagnitude : -_steerInputMagnitude;
            }

            float distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer > _distanceThresholdMeters)
            {
                inputVector.y = _forwardInput;
            }
            else if (distanceToPlayer < _distanceThresholdMeters)
            {
                inputVector.y = _reverseInput;
            }

            _carMovementController.OnMoveInput(inputVector);
        }

        public void SetPlayerCarTransform(Transform playerCarTransform)
        {
            _playerCarTransform = playerCarTransform;
        }

        private void HandleGameStateChanged(object newState)
        {
            _gameState = (GameState)newState;
        }
    }
}
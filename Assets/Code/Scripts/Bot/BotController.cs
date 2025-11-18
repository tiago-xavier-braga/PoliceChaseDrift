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
        private float _maxDistanceFromPlayerMeters = 60f;

        [Header("Reverse On Collision")]
        [SerializeField]
        private float _reverseDurationSeconds = 1.0f;

        [Header("Transparency On Buildings")]
        [SerializeField]
        private LayerMask _buildingLayers;

        [SerializeField]
        private float _buildingFadeAlpha = 0.3f;

        [SerializeField]
        private float _buildingFadeDurationSeconds = 0.25f;

        [SerializeField]
        [ReadOnly]
        private GameState _gameState = GameState.None;

        private Transform _playerCarTransform;
        private bool _isReversing = false;
        private float _reverseTimerSeconds = 0f;

        private int _buildingFadeTweenId = -1;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);

            _gameState = _onGameStateChanged.Parameter != null
                ? (GameState)_onGameStateChanged.Parameter
                : GameState.None;
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

            if (_playerCarTransform == null)
            {
                _carMovementController.OnMoveInput(Vector2.zero);
                return;
            }

            if (_isReversing)
            {
                HandleReverseState();
                return;
            }

            Transform carTransform = _carMovementController.transform;
            Vector3 directionToPlayer = _playerCarTransform.position - carTransform.position;
            float angleToPlayer = Vector3.SignedAngle(carTransform.forward, directionToPlayer, Vector3.up);

            Vector2 inputVector = Vector2.zero;

            float absAngle = Mathf.Abs(angleToPlayer);
            if (absAngle > _minTurnAngleDegrees)
            {
                inputVector.x = angleToPlayer > 0f
                    ? _steerInputMagnitude
                    : -_steerInputMagnitude;
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

            if (distanceToPlayer > _maxDistanceFromPlayerMeters)
            {
                HandleTooFarFromPlayer(distanceToPlayer);
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

        private void HandleReverseState()
        {
            _reverseTimerSeconds -= Time.fixedDeltaTime;

            if (_reverseTimerSeconds <= 0f)
            {
                _isReversing = false;
                _carMovementController.OnMoveInput(Vector2.zero);
                return;
            }

            Vector2 reverseInputVector = new Vector2(0f, _reverseInput);
            _carMovementController.OnMoveInput(reverseInputVector);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_playerCarTransform == null)
            {
                return;
            }

            if (collision.transform.root == _playerCarTransform)
            {
                return;
            }

            _isReversing = true;
            _reverseTimerSeconds = _reverseDurationSeconds;
        }

        private void HandleTooFarFromPlayer(float distanceToPlayer)
        {
            // Intencionalmente em branco.
            // Implementar lógica de desabilitar / respawnar o bot perto do player.
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsInLayerMask(other.gameObject.layer, _buildingLayers))
            {
                SetBotTransparency(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsInLayerMask(other.gameObject.layer, _buildingLayers))
            {
                SetBotTransparency(false);
            }
        }

        private bool IsInLayerMask(int layer, LayerMask layerMask)
        {
            int layerMaskValue = layerMask.value;
            int layerBit = 1 << layer;

            return (layerMaskValue & layerBit) != 0;
        }

        private void SetBotTransparency(bool transparent)
        {
            if (_buildingFadeTweenId >= 0)
            {
                LeanTween.cancel(_buildingFadeTweenId);
                _buildingFadeTweenId = -1;
            }

            float targetAlpha = transparent
                ? _buildingFadeAlpha
                : 1f;

            _buildingFadeTweenId = LeanTween.alpha(gameObject, targetAlpha, _buildingFadeDurationSeconds).id;
        }
    }
}

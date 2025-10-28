using UnityEngine;
using XaviGames.Shared;

namespace XaviGames.Manager
{
    public class LightingCycleManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _lightTransform;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [SerializeField]
        private EventChannel _onNightChangedEventChannel;

        [SerializeField]
        private float _cycleTimeInSeconds = 60f;

        private GameState _gameState = GameState.None;
        private bool _lastIsNightValue = false;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
        }

        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
        }

        private void FixedUpdate()
        {
            if (_gameState != GameState.InGame)
            {
                return;
            }

            float degreesPerSecond = 360f / _cycleTimeInSeconds;
            float deltaAngle = degreesPerSecond * Time.fixedDeltaTime;
            _lightTransform.Rotate(Vector3.right * deltaAngle, Space.Self);
            float currentAngle = _lightTransform.rotation.eulerAngles.x;

            bool isNightNow = currentAngle > 180f && currentAngle < 360f;

            if (isNightNow != _lastIsNightValue)
            {
                _onNightChangedEventChannel.RaiseEvent(isNightNow);
                _lastIsNightValue = isNightNow;
            }
        }

        private void HandleGameStateChanged(object newState)
        {
            _gameState = (GameState)newState;
        }
    }
}

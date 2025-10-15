using UnityEngine;
using UnityEngine.InputSystem;
using XaviEssencials.Runtime;
using XaviGames.Events;
using XaviGames.Manager;
using XaviGames.VariablesObjects;

namespace XaviGames.Car
{
    public class CarSoundController : MonoBehaviour
    {
        [SerializeField]
        private CarMovementController _carMovementController;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [SerializeField]
        private FloatObject _volumeMaster;

        [Header("Audio Sources References")]
        [SerializeField]
        private AudioSource _audioSource;

        [Header("Clips References")]
        [SerializeField]
        private AudioClip _audioClip;

        [SerializeField]
        [Range(0, 1f)]
        private float _volume = 0.5f;

        [SerializeField]
        private float _minMovingPitch = 0.8f;

        [SerializeField]
        private float _maxMovingPitch = 1.2f;

        private GameState _gameState;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe((state) =>
            {
                _gameState = (GameState)state;
            });
        }

        private void Update()
        {
            if (_gameState != GameState.InGame)
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
                return;
            }

            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _audioClip;

                float volume = _volume * _volumeMaster.Value;
                _audioSource.volume = _volume;
                _audioSource.Play();
            }

            float pitch = Mathf.Lerp(_minMovingPitch, _maxMovingPitch,
                                     Mathf.InverseLerp(0f, 100f, _carMovementController.KmPerHour));
            _audioSource.pitch = pitch;
        }

        private void HandleGameStateChanged(object state)
        {
            _gameState = (GameState)state;
        }
    }
}

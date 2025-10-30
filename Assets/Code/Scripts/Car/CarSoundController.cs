using UnityEngine;
using UnityEngine.InputSystem;
using XaviGames.Audio;
using XaviGames.Manager;
using XaviGames.Shared;

namespace XaviGames.Car
{
    public class CarSoundController : MonoBehaviour
    {
        [SerializeField]
        private CarMovementController _carMovementController;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [SerializeField]
        private AudioData _audioData;

        [Header("Audio Sources References")]
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private float _minVolume;

        [SerializeField]
        private float _maxVolume;

        [Header("Clips References")]
        [SerializeField]
        private AudioClip _audioClip;

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

                float volume = Mathf.Lerp(_minVolume, _maxVolume, _audioData.SfxVolume);
                _audioSource.volume = volume;
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

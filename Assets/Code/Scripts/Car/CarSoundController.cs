using UnityEngine;
using UnityEngine.InputSystem;
using XaviEssencials.Runtime;

namespace XaviGames.Car
{
    public class CarSoundController : MonoBehaviour
    {
        [SerializeField]
        private CarMovementController _carMovementController;

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

        private void Update()
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _audioClip;
                _audioSource.volume = _volume;
                _audioSource.Play();
            }
            float pitch = Mathf.Lerp(_minMovingPitch, _maxMovingPitch,
                                     Mathf.InverseLerp(0f, 100f, _carMovementController.KmPerHour));
            _audioSource.pitch = pitch;
        }
    }
}

using UnityEngine;
using XaviGames.Audio;
using XaviGames.Manager;
using XaviGames.Shared;

namespace XaviGames.Sounds
{
    public class CollisionSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioData _audioData;

        [SerializeField]
        private float _minVolume;

        [SerializeField]
        private float _maxVolume;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        private AudioClip _audioClip;
        private GameState _gameState;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(OnGameStateChanged);
        }

        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(OnGameStateChanged);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_gameState != GameState.InGame)
            {
                return;
            }

            CollisionSoundGroup soundGroup = collision.gameObject.GetComponent<CollisionSoundGroup>();

            if (soundGroup == null)
            {
                return;
            }

            float volume = Mathf.Lerp(_minVolume, _maxVolume, _audioData.SfxVolume);
            _audioSource.PlayOneShot(_audioClip, volume);
        }

        private void OnGameStateChanged(object gameState)
        {
            _gameState = (GameState)gameState;
        }
    }
}
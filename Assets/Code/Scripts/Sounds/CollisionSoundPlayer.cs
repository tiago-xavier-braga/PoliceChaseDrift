using UnityEngine;
using XaviGames.Events;
using XaviGames.Manager;
using XaviGames.VariablesObjects;

namespace XaviGames.Sounds
{
    public class CollisionSoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private FloatObject _volumeMaster;

        [SerializeField]
        private float _minCollisionVelocity = 1;

        [SerializeField]
        private float _volume;

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

            float magnitude = collision.relativeVelocity.magnitude;

            _audioClip = soundGroup.AudioClip;
            float volume = (Mathf.Clamp01(magnitude / _minCollisionVelocity) * _volume) * _volumeMaster.Value ;
            _audioSource.PlayOneShot(_audioClip, volume);

        }

        private void OnGameStateChanged(object gameState)
        {
            _gameState = (GameState)gameState;
        }
    }
}
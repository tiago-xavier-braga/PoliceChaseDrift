using System.Diagnostics.Tracing;
using UnityEngine;
using XaviGames.Player;

namespace XaviGames.Car
{
    public class CarParticleController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private PlayerHealth _playerHealth;

        [SerializeField]
        private float _healthThreshold;

        private void OnEnable()
        {
            _playerHealth.OnHealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _playerHealth.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            if (health < _healthThreshold && !_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
            else if (health >= _healthThreshold && _particleSystem.isPlaying)
            {
                _particleSystem.Stop();
            }
        }
    }
}

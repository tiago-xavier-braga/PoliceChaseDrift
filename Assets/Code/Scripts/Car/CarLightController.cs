using UnityEngine;
using XaviGames.Shared;

namespace XaviGames.Car
{
    public class CarLightController : MonoBehaviour
    {
        [SerializeField]
        private Light _leftLight;

        [SerializeField]
        private Light _rightLight;

        [Header("Intensity")]
        [SerializeField]
        private float _intensityAtDay = 0f;

        [SerializeField]
        private float _intensityAtNight = 2f;

        [SerializeField]
        private float _transitionSeconds = 1f;

        [SerializeField]
        private EventChannel _onNightChangedEventChannel;

        [SerializeField]
        private LeanTweenType _easeType = LeanTweenType.easeInOutSine;

        private void OnEnable()
        {
            _onNightChangedEventChannel.Subscribe(OnNightChanged);

            if (_onNightChangedEventChannel.Parameter != null)
            {
                OnNightChanged(_onNightChangedEventChannel.Parameter);
            }
        }

        private void OnDisable()
        {
            _onNightChangedEventChannel.Unsubscribe(OnNightChanged);
        }

        private void OnNightChanged(object state)
        {
            float target = (bool)state ? _intensityAtNight : _intensityAtDay;

            LeanTween
                .value(gameObject, _leftLight.intensity, target, Mathf.Max(0.01f, _transitionSeconds))
                .setEase(_easeType)
                .setOnUpdate((float val) =>
                {
                    _leftLight.intensity = val;
                    _rightLight.intensity = val;
                });
        }
    }
}

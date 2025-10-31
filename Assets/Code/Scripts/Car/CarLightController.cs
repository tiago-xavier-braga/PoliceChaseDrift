using System.Collections.Generic;
using UnityEngine;
using XaviGames.Shared;

namespace XaviGames.Car
{
    public class CarLightController : MonoBehaviour
    {
        [SerializeField]
        private List<Light> _additionalLights = new List<Light>();

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
                .value(gameObject, _additionalLights[0].intensity, target, Mathf.Max(0.01f, _transitionSeconds))
                .setEase(_easeType)
                .setOnUpdate((float val) =>
                {
                    foreach (Light light in _additionalLights)
                    {
                        light.intensity = val;
                    }
                });
        }
    }
}

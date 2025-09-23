using UnityEngine;
using XaviGames.ObjectVariable;

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

        [Header("State Source")]
        [SerializeField]
        private BoolVariable _isNight;

        [SerializeField]
        private LeanTweenType _easeType = LeanTweenType.easeInOutSine;

        private void OnEnable()
        {
            _isNight.OnValueChanged += OnNightChanged;
            OnNightChanged(_isNight.Value);
        }

        private void OnDisable()
        {
            _isNight.OnValueChanged -= OnNightChanged;
        }

        private void OnNightChanged(bool isNight)
        {
            float target = isNight ? _intensityAtNight : _intensityAtDay;

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

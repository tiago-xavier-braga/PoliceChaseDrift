using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XaviGames.ObjectVariable;

namespace XaviGames.Props
{
    public class LightObjectController : MonoBehaviour
    {
        [SerializeField]
        private Light _light;

        [SerializeField]
        private float _intensityAtDay = 0f;

        [SerializeField]
        private float _intensityAtNight = 2f;

        [SerializeField]
        private BoolVariable _isNight;

        [SerializeField]
        private LayerMask _collisionLayerMask;

        [SerializeField]
        private float _disableDelaySeconds;

        private Coroutine _coroutine = null;

        private void OnEnable()
        {
            _isNight.OnValueChanged += OnNightChanged;
            OnNightChanged(_isNight.Value);
        }

        private void OnDisable()
        {
            _isNight.OnValueChanged -= OnNightChanged;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (((1 << other.gameObject.layer) & _collisionLayerMask) != 0)
            {
                if (_coroutine != null)
                {
                    return;
                }


                _coroutine = StartCoroutine(DisableObjectCoroutine());
            }
        }

        private void OnNightChanged(bool state)
        {
            float targetIntensity = state ? _intensityAtNight : _intensityAtDay;

            LeanTween.value(gameObject, _light.intensity, targetIntensity, 1f).setOnUpdate((float val) =>
            {
                _light.intensity = val;
            });
        }

        private IEnumerator DisableObjectCoroutine()
        {
            _light.enabled = false;
            
            yield return new WaitForSeconds(_disableDelaySeconds);

            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using UnityEngine;
using XaviGames.Shared;

namespace XaviGames.Props
{
    [RequireComponent(typeof(Collider))]
    public class LightObjectController : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField]
        private Light _light;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [Header("Intensity")]
        [SerializeField]
        private float _intensityAtDay = 0f;

        [SerializeField]
        private float _intensityAtNight = 2f;

        [SerializeField]
        private float _transitionSeconds = 1f;

        [Header("State Source")]
        [SerializeField]
        private EventChannel _onNightChangedEventChannel;

        [Header("Collision")]
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private LayerMask _collisionLayerMask;

        [SerializeField]
        private float _disableDelaySeconds = 1f;

        [SerializeField]
        private LeanTweenType _easeType = LeanTweenType.easeInOutSine;

        private Coroutine _coroutine;
        private int _tweenId = -1;

        private void OnEnable()
        {
            _onNightChangedEventChannel.Subscribe(OnNightChanged);
            if (_onNightChangedEventChannel.Parameter != null)
            {
                OnNightChanged((bool)_onNightChangedEventChannel.Parameter);
            }
        }

        private void OnDisable()
        {
            _onNightChangedEventChannel.Unsubscribe(OnNightChanged);

            if (_tweenId != -1)
            {
                LeanTween.cancel(_tweenId);
                _tweenId = -1;
            }

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            bool matches = (_collisionLayerMask.value & (1 << other.gameObject.layer)) != 0;
            if (!matches)
            {
                return;
            }

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(DisableObjectCoroutine());
            }
        }

        private void OnNightChanged(object newState)
        {
            if (_light == null)
            {
                return;
            }

            float target = (bool)newState ? _intensityAtNight : _intensityAtDay;

            if (target > 0f && !_light.enabled)
            {
                _light.enabled = true;
            }

            if (_tweenId != -1)
            {
                LeanTween.cancel(_tweenId);
                _tweenId = -1;
            }

            _tweenId = LeanTween
                .value(gameObject, _light.intensity, target, Mathf.Max(0.01f, _transitionSeconds))
                .setEase(_easeType)
                .setOnUpdate((float val) =>
                {
                    if (_light != null)
                    {
                        _light.intensity = val;
                    }
                })
                .setOnComplete(() =>
                {
                    if (_light != null && Mathf.Approximately(target, 0f))
                    {
                        _light.enabled = false;
                    }
                    _tweenId = -1;
                })
                .id;
        }

        private void ApplyIntensity(float value)
        {
            if (_light == null)
            {
                return;
            }

            _light.intensity = value;
            _light.enabled = value > 0f;
        }

        private IEnumerator DisableObjectCoroutine()
        {
            if (_tweenId != -1)
            {
                LeanTween.cancel(_tweenId);
                _tweenId = -1;
            }

            _light.intensity = 0f;
            _light.enabled = false;

            yield return new WaitForSeconds(Mathf.Max(0f, _disableDelaySeconds));

            Material material = _meshRenderer.material;
            material.SetFloat("_Dissolve", 0f);

            LeanTween.value(gameObject, 0f, 1f, 0.5f)
            .setOnUpdate((float val) =>
            {
                material.SetFloat("_Dissolve", val);
            })
            .setOnComplete(() =>
            {

                gameObject.SetActive(false);
            });

            _coroutine = null;
        }
    }
}

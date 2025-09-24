using Newtonsoft.Json.Bson;
using UnityEngine;
using XaviGames.ObjectVariable;

namespace XaviGames.Manager
{
    public class LightingCycleManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _lightTransform;

        [SerializeField]
        private BoolVariable _isMatchStarted;

        [SerializeField]
        private BoolVariable _isNight;

        [SerializeField]
        private float _cycleTimeInSeconds = 60f;

        private bool _lastIsNightValue = false;

        private void FixedUpdate()
        {
            if (!_isMatchStarted.Value)
            {
                return;
            }

            float degreesPerSecond = 360f / _cycleTimeInSeconds;
            float deltaAngle = degreesPerSecond * Time.fixedDeltaTime;
            _lightTransform.Rotate(Vector3.right * deltaAngle, Space.Self);
            float currentAngle = _lightTransform.rotation.eulerAngles.x;

            bool isNightNow = currentAngle > 180f && currentAngle < 360f;

            if (isNightNow != _lastIsNightValue)
            {
                _isNight.SetValue(isNightNow);
                _lastIsNightValue = isNightNow;
            }
        }
    }
}

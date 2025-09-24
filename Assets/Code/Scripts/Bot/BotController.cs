using System;
using UnityEngine;
using XaviEssencials.Runtime;
using XaviGames.Car;
using XaviGames.ObjectVariable;
using XaviGames.Player;

namespace XaviGames.Bot
{
    public class BotController : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField]
        private CarMovementController _carMovementController;

        [SerializeField]
        private BoolVariable _isMatchStarted;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [Header("Tuning")]
        [SerializeField]
        private float _minTurnAngleDegrees = 5f;

        [SerializeField]
        private float _distanceThresholdMeters = 2f;

        [SerializeField]
        private float _steerInputMagnitude = 1f;

        [SerializeField]
        private float _forwardInput = 1f;

        [SerializeField]
        private float _reverseInput = -1f;

        private void Start()
        {
            Material material = _meshRenderer.material;
            material.SetFloat("_Dissolve", 0f);
            LeanTween.value(gameObject, 0f, 1f, 0.5f)
                .setOnUpdate((float val) =>
                {
                    material.SetFloat("_Dissolve", val);
                });
        }

        private void FixedUpdate()
        {
            if (!_isMatchStarted.Value)
            {
                _carMovementController.OnMoveInput(Vector2.zero);
                return;
            }

            Transform carTransform = _carMovementController.transform;
            Transform playerCarTransform = PlayerController.Instance.CarTransform;

            Vector3 directionToPlayer = playerCarTransform.position - carTransform.position;
            float angleToPlayer = Vector3.SignedAngle(carTransform.forward, directionToPlayer, Vector3.up);

            Vector2 inputVector = Vector2.zero;

            float absAngle = Mathf.Abs(angleToPlayer);
            if (absAngle > _minTurnAngleDegrees)
            {
                inputVector.x = angleToPlayer > 0f ? _steerInputMagnitude : -_steerInputMagnitude;
            }

            float distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer > _distanceThresholdMeters)
            {
                inputVector.y = _forwardInput;
            }
            else if (distanceToPlayer < _distanceThresholdMeters)
            {
                inputVector.y = _reverseInput;
            }

            _carMovementController.OnMoveInput(inputVector);
        }
    }
}

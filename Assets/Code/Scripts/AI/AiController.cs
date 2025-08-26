using System;
using UnityEngine;
using XaviEssencials.Runtime;
using XaviGames.Car;
using XaviGames.Player;

namespace XaviGames.AI
{
    public class AiController : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField]
        private CarMovementController _carMovementController;

        [SerializeField]
        private PlayerController _playerController;

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

        [SerializeField]
        private bool _useHandbrake = false;

        private void FixedUpdate()
        {
            Transform carTransform = _carMovementController.transform;
            Transform playerCarTransform = _playerController.CarTransform;

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
            _carMovementController.OnHandbrake(_useHandbrake);
        }
    }
}

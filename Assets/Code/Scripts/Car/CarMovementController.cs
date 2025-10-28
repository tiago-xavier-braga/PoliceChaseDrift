using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XaviGames.Shared;

namespace XaviGames.Car
{
    public class CarMovementController : MonoBehaviour
    {
        [Header("Car Properties")]
        [SerializeField]
        private CarParameter _carParameter;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private List<WheelController> _wheelControllers;

        [Header("Info")]
        [SerializeField]
        [ReadOnly]
        private Vector2 _inputVector;

        [field: SerializeField]
        [field: ReadOnly]
        public float KmPerHour { get; private set; } = 0f;

        private WheelFrictionCurve _originalSidewaysFriction = new();
        private WheelFrictionCurve _driftSidewaysFriction = new();
        
        private float _defaultAngularDamping;

        private void Start()
        {
            ApplyCenterMass();
            ConfigureWheelSettings();
        }

        private void FixedUpdate()
        {
            UpdatePhysics();
        }

        public void OnMoveInput(Vector2 value)
        {
            _inputVector = value;
        }

        public void OnHandbrake(bool state)
        {
            if (state)
            {
                ApplyDriftWheelSettings();
            }
            else
            {
                ApplyDefaultWheelSettings();
            }
        }

        public void Block()
        {
            _rigidbody.isKinematic = true;
        }

        public void Unblock()
        {
            _rigidbody.isKinematic = false;
        }

        private void ApplyCenterMass()
        {
            Vector3 centerOfMass = _rigidbody.centerOfMass;
            centerOfMass.y += _carParameter.CentreOfGravityOffset;
            _rigidbody.centerOfMass = centerOfMass;
        }

        private void ConfigureWheelSettings()
        {
            _originalSidewaysFriction = _wheelControllers.First().WheelCollider.sidewaysFriction;
            _driftSidewaysFriction = _carParameter.DriftFrictionCurve;
            _defaultAngularDamping = _rigidbody.angularDamping;
        }


        private void ApplyDriftWheelSettings()
        {
            foreach (var wheel in _wheelControllers)
            {
                wheel.WheelCollider.sidewaysFriction = _driftSidewaysFriction;
            }

            _rigidbody.angularDamping = _carParameter.DriftAngularDamping;
        }

        private void ApplyDefaultWheelSettings()
        {
            foreach (var wheel in _wheelControllers)
            {
                wheel.WheelCollider.sidewaysFriction = _originalSidewaysFriction;
            }

            _rigidbody.angularDamping = _defaultAngularDamping;
        }

        private void UpdatePhysics()
        {
            KmPerHour = _rigidbody.linearVelocity.magnitude * 3.6f;

            float forwardSpeed = Vector3.Dot(transform.forward, _rigidbody.linearVelocity);
            float speedFactor = Mathf.InverseLerp(0f, _carParameter.TopSpeed, Mathf.Abs(forwardSpeed));

            float motorTorque = Mathf.Lerp(_carParameter.Acceleration, 0f, speedFactor);
            float steeringRange = Mathf.Lerp(
                _carParameter.SteeringRange,
                _carParameter.SteeringRangeAtMaxSpeed,
                speedFactor
            );

            bool isAccelerating = Mathf.Sign(_inputVector.y) == Mathf.Sign(forwardSpeed);

            ApplyWheelForces(motorTorque, steeringRange, isAccelerating);
        }

        private void ApplyWheelForces(float currentMotorTorque, float currentSteerRange, bool isAccelerating)
        {
            foreach (var wheel in _wheelControllers)
            {
                if (wheel.IsSteerable)
                {
                    wheel.WheelCollider.steerAngle = _inputVector.x * currentSteerRange;
                }

                if (isAccelerating)
                {
                    if (wheel.IsMotorized)
                    {
                        wheel.WheelCollider.motorTorque = _inputVector.y * currentMotorTorque;
                    }

                    wheel.WheelCollider.brakeTorque = 0f;
                }
                else
                {
                    wheel.WheelCollider.motorTorque = 0f;
                    wheel.WheelCollider.brakeTorque = Mathf.Abs(_inputVector.y) * _carParameter.BreakForce;
                }

                wheel.UpdateWheelPosition();
            }
        }
    }
}

using UnityEngine;
using System;
using JetBrains.Annotations;

namespace XaviGames.Car
{
    [CreateAssetMenu(fileName = "_CarParameter", menuName = "Xavi Games/Car/Car Parameter")]
    public class CarParameter : ScriptableObject
    {
        [field: SerializeField]
        public string Id { get; private set; } = Guid.NewGuid().ToString("N").Substring(0, 8);

        [field: SerializeField]
        public GameObject CarPrefab { get; private set; }

        [field: SerializeField]
        public int CarHealth { get; private set; }

        [field: Header("Physical Parameters")]

        [field: SerializeField]
        public float TopSpeed { get; private set; }

        [field: SerializeField]
        public float Acceleration { get; private set; }

        [field: SerializeField]
        public float BreakForce { get; private set; }

        [field: SerializeField]
        public float SteeringRange { get; private set; }

        [field: SerializeField]
        public float SteeringRangeAtMaxSpeed { get; private set; }

        [field: SerializeField]
        public float CenterOfGravityOffset { get; private set; }

        [Header("Drift Friction Curve")]
        [SerializeField]
        private float _extremumSlip;

        [SerializeField]
        private float _extremumValue;
        
        [SerializeField]
        private float _asymptoteSlip;
        
        [SerializeField] 
        private float _asymptoteValue;
        
        [SerializeField] 
        private float _stiffness;

        [field: SerializeField]
        public float DriftAngularDamping { get; private set; }

        public WheelFrictionCurve DriftFrictionCurve
        {
            get
            {
                return new WheelFrictionCurve
                {
                    extremumSlip = _extremumSlip,
                    extremumValue = _extremumValue,
                    asymptoteSlip = _asymptoteSlip,
                    asymptoteValue = _asymptoteValue,
                    stiffness = _stiffness
                };
            }
        }
    }
}


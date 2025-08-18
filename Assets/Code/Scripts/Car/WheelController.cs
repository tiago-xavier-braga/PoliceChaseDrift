using System;
using UnityEngine;

namespace XaviGames.Car
{
    public class WheelController : MonoBehaviour
    {
        [field: SerializeField]
        public Transform ModelTransform { get; private set; }

        [field: SerializeField]
        public WheelCollider WheelCollider {  get; private set; }

        [field: SerializeField]
        public bool IsMotorized { get; private set; }

        [field: SerializeField]
        public bool IsSteerable { get; private set; }

        public void UpdateWheelPosition()
        {
            Vector3 position;
            Quaternion rotation;
            WheelCollider.GetWorldPose(out position, out rotation);
            ModelTransform.position = position;
            ModelTransform.rotation = rotation;
        }
    }
}

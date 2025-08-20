using UnityEngine;

namespace XaviGames.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private Vector3 _offset = new Vector3(-10f, 10f, -10f);

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }

            Vector3 targetPosition = _target.position + _offset;
            transform.position = targetPosition;

            transform.LookAt(_target.position);
        }
    }
}

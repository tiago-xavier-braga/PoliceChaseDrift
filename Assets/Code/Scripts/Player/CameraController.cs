using UnityEngine;

namespace XaviGames.Player
{
    [ExecuteInEditMode]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;

        [SerializeField]
        private Vector3 _offset = new Vector3(-10f, 10f, -10f);
        
        private Transform _target;

        private void Start()
        {
            _target = _playerController.CarTransform;
        }

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

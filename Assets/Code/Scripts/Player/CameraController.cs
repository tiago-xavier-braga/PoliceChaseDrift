using UnityEngine;

namespace XaviGames.Player
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;

        [SerializeField]
        private Vector3 _offset = new Vector3(-10f, 10f, -10f);
        
        [SerializeField]
        private bool _isFollowingPlayer = true;

        [Header("Initial Animation")]
        [SerializeField]
        private float _initialAnimationDuration = 2f;

        [SerializeField]
        private float _initialOrthographicSize = 5f;

        [SerializeField]
        private float _endOrthographicSize = 10f;

        [SerializeField]
        private LeanTweenType _leanTweenType = LeanTweenType.easeInOutQuad;

        private Transform _target;
        private Camera _camera;

        private void Start()
        {
            _target = _playerController.CarTransform;
            _camera = GetComponent<Camera>();
            _camera.orthographicSize = _initialOrthographicSize;
        }

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }

            if (!_isFollowingPlayer)
            {
                return;
            }

            Vector3 targetPosition = _target.position + _offset;
            transform.position = targetPosition;

            transform.LookAt(_target.position);
        }

        public void SetFollowPlayer(bool value)
        {
            _isFollowingPlayer = value;
        }

        public void StartInitialAnimation()
        {
            LeanTween.value(gameObject, _initialOrthographicSize, _endOrthographicSize, _initialAnimationDuration)
                .setEase(_leanTweenType)
                .setOnUpdate((float size) =>
                {
                    _camera.orthographicSize = size;
                });
        }
    }
}

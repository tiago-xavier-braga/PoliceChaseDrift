using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XaviGames.Car
{
    public class CarEffectsManager : MonoBehaviour
    {
        [Header("Trail Renderers")]
        [SerializeField]
        private List<TrailRenderer> _wheelTrailRenderers;

        [Header("Input Actions")]
        [SerializeField]
        private InputActionReference _handbrakeInputAction;

        private void OnEnable()
        {
            _handbrakeInputAction.action.performed += OnCarDrifting;
            _handbrakeInputAction.action.canceled += OnCarDrifting;
        }

        private void OnDisable()
        {
            _handbrakeInputAction.action.performed -= OnCarDrifting;
            _handbrakeInputAction.action.canceled -= OnCarDrifting;
        }

        private void OnCarDrifting(InputAction.CallbackContext context)
        {
            bool isDrifting = context.phase == InputActionPhase.Performed;
            SetTrailsActive(isDrifting);
        }

        private void SetTrailsActive(bool isActive)
        {
            foreach (var trail in _wheelTrailRenderers)
            {
                trail.emitting = isActive;
            }
        }
    }
}
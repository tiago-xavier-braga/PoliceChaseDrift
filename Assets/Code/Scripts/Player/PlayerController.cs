using UnityEngine;
using UnityEngine.InputSystem;
using XaviGames.Car;

namespace XaviGames.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Car References")]
        [field: SerializeField]
        public Transform CarTransform { get; private set; }

        [SerializeField]
        private CarMovementController _carMovementController;

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            _carMovementController.OnMoveInput(inputVector);
        }

        public void OnHandbrake(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _carMovementController.OnHandbrake(true);
                    break;

                case InputActionPhase.Canceled:
                    _carMovementController.OnHandbrake(false);
                    break;
            }
        }
    }
}

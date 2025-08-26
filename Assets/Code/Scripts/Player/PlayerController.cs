using UnityEngine;
using UnityEngine.InputSystem;
using XaviGames.Car;

namespace XaviGames.Player
{
    public class PlayerController : MonoBehaviour
    {
        [field: Header("Car References")]
        [field: SerializeField]
        public Transform CarTransform { get; private set; }

        [field: SerializeField]
        public CarMovementController CarMovementController { get; private set; }

        [field: SerializeField]
        public PlayerHealth PlayerHealth { get; private set; }

        public static PlayerController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            CarMovementController.OnMoveInput(inputVector);
        }

        public void OnHandbrake(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    CarMovementController.OnHandbrake(true);
                    break;

                case InputActionPhase.Canceled:
                    CarMovementController.OnHandbrake(false);
                    break;
            }
        }
    }
}

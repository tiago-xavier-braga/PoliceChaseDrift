using UnityEngine;

namespace XaviGames.Car
{
    public class CarController : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsMovementEnabled { get; private set; } = true;
    }
}

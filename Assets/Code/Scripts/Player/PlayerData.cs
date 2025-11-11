using System.Collections.Generic;
using UnityEngine;
using XaviGames.Car;

namespace XaviGames.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Xavi Games/Player/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [field: Header("Runtime References")]
        [field: SerializeField]
        public CarParameter CurrentCar { get; private set; }

        [field: SerializeField]
        public List<CarParameter> UnlockedCars { get; private set; } = new();

        [Header("References")]
        [SerializeField]
        private CarDatabase _carDatabase;

        public void UnlockCar(CarParameter carParameter)
        {
            if (!_carDatabase.CarsParameters.Contains(carParameter))
            {
                Debug.LogWarning($"Car with ID {carParameter.Id} does not exist in the database.");
                return;
            }

            if (UnlockedCars.Contains(carParameter))
            {
                return;
            }

            UnlockedCars.Add(carParameter);
        }

        public void SetCurrentCar(CarParameter carParameter)
        {
            if (!UnlockedCars.Contains(carParameter))
            {
                return;
            }

            CurrentCar = carParameter;

        }
    }
}
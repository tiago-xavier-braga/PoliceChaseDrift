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
        public List<CarParameter> UnlockedCar { get; private set; } = new();

        [Header("References")]
        private CarDatabase _carDatabase;

        public void UnlockCar(string carId)
        {
            CarParameter parameter = _carDatabase.GetCarParameterById(carId);

            if (!UnlockedCar.Contains(parameter))
            {
                UnlockedCar.Add(parameter);
            }
        }

        public void SetCurrentCar(string carId)
        {
            CarParameter parameter = _carDatabase.GetCarParameterById(carId);

            if (UnlockedCar.Contains(parameter))
            {
                CurrentCar = parameter;
            }
            else
            {
                Debug.LogWarning($"Car with ID {carId} is not unlocked and cannot be set as current.");
            }
        }
    }
}
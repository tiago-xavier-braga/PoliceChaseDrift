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
        public string CurrentCar { get; private set; }

        [field: SerializeField]
        public List<string> UnlockedCar { get; private set; } = new List<string>();
    
        public void UnlockCar(string carId)
        {
            if (!UnlockedCar.Contains(carId))
            {
                UnlockedCar.Add(carId);
            }
        }

        public void SetCurrentCar(string carId)
        {
            if (UnlockedCar.Contains(carId))
            {
                CurrentCar = carId;
            }
            else
            {
                Debug.LogWarning($"Car with ID {carId} is not unlocked and cannot be set as current.");
            }
        }
    }
}
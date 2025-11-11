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

        [Header("References")]
        [SerializeField]
        private CarDatabase _carDatabase;

        public void SetCurrentCar(CarParameter carParameter)
        {
            if (carParameter == null || !_carDatabase.CarsParameters.Contains(carParameter))
            {
                Debug.LogError("CarParameter is null or not found in CarDatabase.");
                return;
            }

            CurrentCar = carParameter;
        }
    }
}
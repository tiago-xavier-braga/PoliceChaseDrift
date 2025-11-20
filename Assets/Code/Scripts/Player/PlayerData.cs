using System.Collections.Generic;
using UnityEngine;
using XaviGames.Car;
using XaviGames.Shared;

namespace XaviGames.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Xavi Games/Player/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [field: Header("Car References")]
        [field: SerializeField]
        public CarParameter CurrentCar { get; private set; }

        [SerializeField]
        private CarDatabase _carDatabase;

        [field: SerializeField]
        [field: ReadOnly]
        public int Score = 0;

        public void SetCurrentCar(CarParameter carParameter)
        {
            if (carParameter == null || !_carDatabase.CarsParameters.Contains(carParameter))
            {
                Debug.LogError("CarParameter is null or not found in CarDatabase.");
                return;
            }

            CurrentCar = carParameter;
        }

        public void SetScore(int score)
        {
            Score = score;
        }
    }
}
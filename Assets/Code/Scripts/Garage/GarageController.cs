using System.Linq;
using UnityEngine;
using XaviGames.Car;
using XaviGames.Player;
using XaviGames.Shared;

namespace XaviGames.Garage
{
    public class GarageController : MonoBehaviour
    {
        [SerializeField]
        private CarDatabase _carDatabase;

        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private Transform _startPosition;

        [SerializeField]
        private EventChannel _onCarSelected;

        [SerializeField]
        [ReadOnly]
        private int _currentCarIndex = 0;

        private GameObject _currentCarObject = null;

        private void Start()
        {
            CarParameter currentCar = _playerData.CurrentCar;

            if (currentCar == null)
            {
                currentCar = _carDatabase.CarsParameters.First();
            }

            _currentCarIndex = _carDatabase.CarsParameters.IndexOf(currentCar);
            SpawnCar(currentCar);
        }

        public void SpawnCar(CarParameter carParameter)
        {
            if (_currentCarObject != null)
            {
                Destroy(_currentCarObject);
            }

            _currentCarObject = Instantiate(carParameter.CarPrefab, _startPosition.position, _startPosition.rotation);
            _currentCarObject.transform.SetParent(_startPosition.parent);
            _onCarSelected.RaiseEvent(_currentCarObject);
        }

        public void SwitchedUp()
        {
            if (_currentCarIndex >= _carDatabase.CarsParameters.Count - 1)
            {
                return;
            }

            _currentCarIndex++;
            CarParameter nextCar = _carDatabase.CarsParameters[_currentCarIndex];
            SpawnCar(nextCar);
        }

        public void SwitchedDown()
        {
            if (_currentCarIndex <= 0)
            {
                return;
            }

            _currentCarIndex--;
            CarParameter previousCar = _carDatabase.CarsParameters[_currentCarIndex];
            SpawnCar(previousCar);
        }

        public void UnlockedCar()
        {
            CarParameter carToUnlock = _carDatabase.CarsParameters[_currentCarIndex];
            _playerData.SetCurrentCar(carToUnlock);
            SpawnCar(carToUnlock);
            Debug.Log($"Car {carToUnlock.Id} unlocked!");
        }

        public void ExitGarage()
        {
            SpawnCar(_playerData.CurrentCar);
        }
    }
}
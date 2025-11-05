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

        private CarParameter _selectedCar = null;
        private GameObject _currentCarObject = null;

        private void Start()
        {
            _selectedCar = _playerData.CurrentCar;

            if (_selectedCar == null)
            {
                _selectedCar = _playerData.UnlockedCars.First();
            }

            _currentCarIndex = _carDatabase.CarsParameters.IndexOf(_selectedCar);
            SpawnCar(_selectedCar);
            _onCarSelected.RaiseEvent(_currentCarObject);
        }

        public void SpawnCar(CarParameter carParameter)
        {
            if (_currentCarObject != null)
            {
                Destroy(_currentCarObject);
            }

            _currentCarObject = Instantiate(carParameter.CarPrefab, _startPosition.position, _startPosition.rotation);
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

            if (!_playerData.UnlockedCars.Contains(nextCar))
            {
                Debug.Log("This car is locked!");
            }
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

            if (!_playerData.UnlockedCars.Contains(previousCar))
            {
                Debug.Log("This car is locked!");
            }
        }

        public void UnlockedCar()
        {
            CarParameter carToUnlock = _carDatabase.CarsParameters[_currentCarIndex];
            _playerData.UnlockCar(carToUnlock);
            Debug.Log($"Car {carToUnlock.Id} unlocked!");
        }

        public void SelectCar()
        {
            CarParameter carToSelect = _carDatabase.CarsParameters[_currentCarIndex];
            if (_playerData.UnlockedCars.Contains(carToSelect))
            {
                _playerData.SetCurrentCar(carToSelect);
                _onCarSelected.RaiseEvent(_currentCarObject);
                Debug.Log($"Car {carToSelect.Id} selected as current car!");
            }
            else
            {
                Debug.Log("This car is locked and cannot be selected!");
            }
        }

        public void SpawnSelectCar()
        {
            CarParameter carSpawned = _carDatabase.CarsParameters[_currentCarIndex];

            if (carSpawned == _selectedCar)
            {
                return;
            }

            SpawnCar(_selectedCar);
        }
    }
}
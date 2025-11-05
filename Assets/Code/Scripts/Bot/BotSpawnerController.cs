using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using XaviGames.Manager;
using XaviGames.Player;
using XaviGames.Shared;
using static UnityEngine.GraphicsBuffer;

namespace XaviGames.Bot
{
    public class BotSpawnerController : MonoBehaviour
    {
        [SerializeField]
        private EventChannel _onNightChangedEventChannel;

        [SerializeField]
        private EventChannel _onGameStateChangedEventChannel;

        [SerializeField]
        private EventChannel _onCarSelected;
        
        [SerializeField]
        private int _numberOfBotsToSpawn = 5;

        [Space]
        [SerializeField]
        private List<GameObject> _botPrefabs;

        [Space]
        [SerializeField]
        private List<Transform> _spawnPoints;

        [Header("Debug")]
        [SerializeField]
        [ReadOnly]
        private GameState _gameState = GameState.None;

        [SerializeField]
        [ReadOnly]
        private int _spawnedBotsCount = 1;

        [SerializeField]
        [ReadOnly]
        private List<GameObject> _spawnedBots = new List<GameObject>();

        private GameObject _playerCarObject;

        private void OnEnable()
        {
            _onNightChangedEventChannel.Subscribe(HandleIsNightChanged);
            _onGameStateChangedEventChannel.Subscribe(HandleGameStateChanged);
            _onCarSelected.Subscribe(HandleCarSelected);
        }

        private void OnDisable()
        {
            _onNightChangedEventChannel.Unsubscribe(HandleIsNightChanged);
            _onGameStateChangedEventChannel.Unsubscribe(HandleGameStateChanged);
            _onCarSelected.Unsubscribe(HandleCarSelected);
        }

        private void Start()
        {
            for (int i = 0; i < _numberOfBotsToSpawn; i++)
            {
                GameObject botPrefab = _botPrefabs[Random.Range(0, _botPrefabs.Count)];
                GameObject instance = Instantiate(botPrefab, Vector3.zero, Quaternion.identity, transform);
                BotController botController = instance.GetComponent<BotController>();
                //botController.SetPlayerCarTransform(_playerCarObject.transform);
                instance.SetActive(false);
                _spawnedBots.Add(instance);
            }
        }

        private void HandleIsNightChanged(object state)
        {
            if (!(bool)state)
            {
                return;
            }
         
            SpawnBot();
        }

        private void HandleGameStateChanged(object state)
        {
            _gameState = (GameState)state;
        }

        private void HandleCarSelected(object carObject)
        {
            _playerCarObject = (GameObject)carObject;
        }

        private void SpawnBot()
        {
            if (_gameState != GameState.InGame)
            {
                return;
            }

            if (_spawnedBotsCount >= _numberOfBotsToSpawn)
            {
                return;
            }

            Transform carTransform = _playerCarObject.transform;

            Transform farthestPoint = _spawnPoints[0];
            float maxDistance = Vector3.Distance(carTransform.position, farthestPoint.position);

            foreach (Transform point in _spawnPoints)
            {
                float currentDistance = Vector3.Distance(carTransform.position, point.position);
                if (currentDistance > maxDistance)
                {
                    farthestPoint = point;
                    maxDistance = currentDistance;
                }
            }

            GameObject bot = _spawnedBots[_spawnedBotsCount];
            bot.GetComponent<BotController>().SetPlayerCarTransform(carTransform);
            bot.transform.position = farthestPoint.position;
            bot.transform.rotation = farthestPoint.rotation;
            bot.SetActive(true);

            _spawnedBotsCount++;
        }
    }
}

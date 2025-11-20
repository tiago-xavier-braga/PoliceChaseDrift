using System.Collections.Generic;
using UnityEngine;
using XaviGames.Manager;
using XaviGames.Shared;

namespace XaviGames.Bot
{
    public class BotSpawnerController : MonoBehaviour
    {
        [SerializeField]
        private EventChannel _onGameStateChangedEventChannel;

        [SerializeField]
        private EventChannel _onCarSelected;

        [Header("Spawn Settings")]
        [SerializeField]
        private int _numberOfBotsToSpawn = 5;

        [SerializeField]
        private float _spawnIntervalSeconds = 10f;

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
        private int _spawnedBotsCount = 0;

        [SerializeField]
        [ReadOnly]
        private List<GameObject> _spawnedBots = new List<GameObject>();

        private GameObject _playerCarObject;
        private float _spawnTimerSeconds = 0f;

        private void OnEnable()
        {
            _onGameStateChangedEventChannel.Subscribe(HandleGameStateChanged);
            _onCarSelected.Subscribe(HandleCarSelected);
        }

        private void OnDisable()
        {
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
                instance.SetActive(false);
                _spawnedBots.Add(instance);
            }

            SpawnBot();
        }

        private void Update()
        {
            if (_gameState != GameState.InGame)
            {
                return;
            }

            if (_playerCarObject == null)
            {
                return;
            }

            if (_spawnedBotsCount >= _numberOfBotsToSpawn)
            {
                return;
            }

            _spawnTimerSeconds += Time.deltaTime;

            if (_spawnTimerSeconds < _spawnIntervalSeconds)
            {
                return;
            }

            _spawnTimerSeconds = 0f;
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

            if (_playerCarObject == null)
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

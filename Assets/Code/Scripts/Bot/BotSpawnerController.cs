using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using XaviEssencials.Runtime;
using XaviGames.Events;
using XaviGames.Manager;
using XaviGames.Player;

namespace XaviGames.Bot
{
    public class BotSpawnerController : MonoBehaviour
    {
        [SerializeField]
        private EventChannel _onNightChangedEventChannel;

        [SerializeField]
        private EventChannel _onGameStateChangedEventChannel;

        [SerializeField]
        private int _numberOfBotsToSpawn = 5;

        [SerializeField]
        private PlayerController _playerController;

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

        private void OnEnable()
        {
            _onNightChangedEventChannel.Subscribe(HandleIsNightChanged);
            _onGameStateChangedEventChannel.Subscribe(HandleGameStateChanged);
        }

        private void OnDisable()
        {
            _onNightChangedEventChannel.Unsubscribe(HandleIsNightChanged);
            _onGameStateChangedEventChannel.Unsubscribe(HandleGameStateChanged);
        }

        private void Start()
        {
            for (int i = 0; i < _numberOfBotsToSpawn; i++)
            {
                GameObject botPrefab = _botPrefabs[Random.Range(0, _botPrefabs.Count)];
                GameObject instance = Instantiate(botPrefab, Vector3.zero, Quaternion.identity, transform);
                BotController botController = instance.GetComponent<BotController>();
                botController.PlayerController = _playerController;
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

            Transform carTransform = _playerController.CarTransform;

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
            bot.transform.position = farthestPoint.position;
            bot.transform.rotation = farthestPoint.rotation;
            bot.SetActive(true);

            _spawnedBotsCount++;
        }
    }
}

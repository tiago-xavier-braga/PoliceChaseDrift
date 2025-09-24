using System.Collections.Generic;
using UnityEngine;
using XaviEssencials.Runtime;
using XaviGames.ObjectVariable;

namespace XaviGames.Bot
{
    public class BotSpawnerController : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable _isNight;

        [SerializeField]
        private int _numberOfBotsToSpawn = 5;

        [SerializeField]
        private List<GameObject> _botPrefabs;

        [Header("Debug")]
        [SerializeField]
        [ReadOnly]
        private int _spawnedBotsCount = 1;

        [SerializeField]
        [ReadOnly]
        private List<GameObject> _spawnedBots = new List<GameObject>();

        private void OnEnable()
        {
            _isNight.OnValueChanged += HandleIsNightChanged;
        }

        private void OnDisable()
        {
            _isNight.OnValueChanged -= HandleIsNightChanged;
        }

        private void Start()
        {
            for (int i = 0; i < _numberOfBotsToSpawn; i++)
            {
                GameObject botPrefab = _botPrefabs[Random.Range(0, _botPrefabs.Count)];
                GameObject instance = Instantiate(botPrefab, Vector3.zero, Quaternion.identity, transform);
                instance.SetActive(false);
                _spawnedBots.Add(instance);
            }
        }

        private void HandleIsNightChanged(bool isNight)
        {
            if (isNight)
            {
                SpawnBot();
            }
        }

        private void SpawnBot()
        {
            if (_spawnedBotsCount >= _numberOfBotsToSpawn)
            {
                return;
            }

            _spawnedBots[_spawnedBotsCount - 1].SetActive(true);
            _spawnedBotsCount++;
        }
    }
}

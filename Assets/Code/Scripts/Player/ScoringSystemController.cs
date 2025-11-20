using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XaviGames.Manager;
using XaviGames.Shared;

namespace XaviGames.Player
{
    public class ScoringSystemController : MonoBehaviour
    {
        [SerializeField]
        private PlayerData _playerData;

        [SerializeField]
        private EventChannel _onCarSelected;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [SerializeField]
        [ReadOnly]
        private int _currentScore = 0;

        [SerializeField]
        [ReadOnly]
        private float _distanceTraveled = 0f;

        [SerializeField]
        private List<TextMeshProUGUI> _scoreTexts;

        [SerializeField]
        [ReadOnly]
        private GameObject _currentCarObject;

        [SerializeField]
        [ReadOnly]
        private Vector3 _lastPosition;

        [SerializeField]
        [ReadOnly]
        private GameState _currentGameState = GameState.None;

        private void OnEnable()
        {
            _onCarSelected.Subscribe(HandleCarSelected);
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
        }

        private void OnDisable()
        {
            _onCarSelected.Unsubscribe(HandleCarSelected);
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
        }


        private void Update()
        {
            Vector3 currentPosition = Vector3.Scale(_currentCarObject.transform.position, new Vector3(1f, 0f, 1f));
            Vector3 lastPosition = Vector3.Scale(_lastPosition, new Vector3(1f, 0f, 1f));

            float distance = Vector3.Distance(currentPosition, lastPosition);
            _distanceTraveled += distance;

            Debug.Log($"Current Distance: {distance}");
            Debug.Log($"Distance Traveled: {_distanceTraveled}");

            while (_distanceTraveled >= 1f)
            {
                _distanceTraveled -= 1f;
                _currentScore++;
                UpdateScoreTexts();
            }

            _lastPosition = _currentCarObject.transform.position;
        }


        private void HandleGameStateChanged(object gameState)
        {
            _currentGameState = (GameState)gameState;

            if (_currentGameState == GameState.InGame)
            {
                _currentScore = 0;
                _distanceTraveled = 0f;
                UpdateScoreTexts();
            }

            if (_currentGameState == GameState.GameOver)
            {
                _playerData.SetScore(_currentScore);
            }
        }


        private void HandleCarSelected(object carObject)
        {
            _currentCarObject = (GameObject)carObject;
            _lastPosition = _currentCarObject.transform.position;
        }


        private void UpdateScoreTexts()
        {
            string scoreString = _currentScore.ToString();

            foreach (var scoreText in _scoreTexts)
            {
                if (scoreText != null)
                {
                    scoreText.text = $"Score: {scoreString}";
                }
            }
        }
    }
}

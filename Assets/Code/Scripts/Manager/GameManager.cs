using UnityEngine;
using XaviGames.Player;
using XaviGames.Shared;
using XaviGames.Ui;

namespace XaviGames.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SceneBundle _cutsceneBundle;

        [SerializeField]
        private SceneBundle _sceneBundle;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [SerializeField]
        private EventChannel _onReloadGame;

        [SerializeField]
        private PlayerHealth _playerHealth;

        [Header("Debug")]
        [SerializeField]
        [ReadOnly]
        private GameState _gameState;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
            _onReloadGame.Subscribe(_ => ReloadGame());
        }

        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
            _onReloadGame.Unsubscribe(_ => ReloadGame());
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadingCanvasController.Instance.EnableLoading();
            _cutsceneBundle.LoadScenesAsync(true, OnSceneLoadStatus);
        }

        private void OnSceneLoadStatus(float percent)
        {
            if (percent >= 1f)
            {
                LoadingCanvasController.Instance.DisableLoading();
                _onGameStateChanged.RaiseEvent(GameState.Ready);
            }
        }

        private void ReloadGame()
        {
            LoadingCanvasController.Instance.EnableLoading();
            _sceneBundle.LoadScenesAsync(false, OnSceneLoadStatus);
        }

        private void HandleGameStateChanged(object newState)
        {
            if (newState == null)
            {

                Debug.LogError("Trying to change to a null _gameState");
                return;
            }

            _gameState = (GameState)newState;
        }
    }
}

using UnityEngine;
using XaviEssencials.Runtime;
using XaviGames.Events;
using XaviGames.Ui;

namespace XaviGames.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SceneBundle _sceneBundle;

        [SerializeField]
        private EventChannel _onGameStateChanged;

        [Header("Debug")]
        [SerializeField]
        [ReadOnly]
        private GameState _gameState;

        private void OnEnable()
        {
            _onGameStateChanged.Subscribe(HandleGameStateChanged);
        }

        private void OnDisable()
        {
            _onGameStateChanged.Unsubscribe(HandleGameStateChanged);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadingCanvasController.Instance.EnableLoading();
            _sceneBundle.LoadScenesAsync(OnSceneLoadStatus);
        }

        private void OnSceneLoadStatus(float percent)
        {
            if (percent >= 1f)
            {
                LoadingCanvasController.Instance.DisableLoading();
                _onGameStateChanged.RaiseEvent(GameState.Ready);
            }
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

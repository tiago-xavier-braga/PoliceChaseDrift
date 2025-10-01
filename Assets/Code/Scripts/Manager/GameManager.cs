using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XaviEssencials.Runtime;
using XaviGames.ObjectVariable;
using XaviGames.Ui;

namespace XaviGames.Manager
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SceneBundle _sceneBundle;

        [Header("Variables")]
        [SerializeField]
        private BoolVariable _isReadyStart;
        public static GameManager Instance { get; private set; } = null;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
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
                _isReadyStart.SetValue(true);
            }
        }
    }
}

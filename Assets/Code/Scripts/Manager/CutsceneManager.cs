using UnityEngine;
using UnityEngine.Playables;
using XaviGames.Shared;
using XaviGames.Ui;

namespace Haus.Managers
{

    public class CutsceneManager : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _playableDirector;

        [SerializeField]
        private SceneBundle _gameSceneBundle;

        private void Start()
        {
            LoadingCanvasController.Instance.DisableLoading();
            _playableDirector.stopped += OnCutsceneFinished;
            _playableDirector.Play();
        }

        private void OnCutsceneFinished(PlayableDirector director)
        {
            LoadingCanvasController.Instance.EnableLoading(0f);
            _gameSceneBundle.LoadScenesAsync();
        }

        private void OnDestroy()
        {
            _playableDirector.stopped -= OnCutsceneFinished;
        }
    }
}
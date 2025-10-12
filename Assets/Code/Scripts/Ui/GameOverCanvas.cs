using UnityEngine;
using XaviGames.Events;

namespace XaviGames.Ui
{
    public class GameOverCanvas : CanvasGroupController
    {
        [SerializeField]
        private EventChannel _onReloadGame;

        public void ReloadGame()
        {
            _onReloadGame.RaiseEvent();
        }

    }
}

using UnityEngine;

namespace XaviGames.Manager
{
    [System.Serializable]
    public enum GameState
    {
        None,
        Ready,
        InGame,
        Pause,
        GameOver,
    }
}

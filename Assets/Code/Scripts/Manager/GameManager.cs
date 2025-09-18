using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XaviEssencials.Runtime;

namespace XaviGames.Manager
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SceneBundle _sceneBundle;

        private void Start()
        {
            _sceneBundle.LoadScenesAsync();
        }
    }
}

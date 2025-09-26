using UnityEngine;

namespace XaviGames.Ui
{
    public class LoadCanvasControler : MonoBehaviour
    {
        
        public static LoadCanvasControler Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

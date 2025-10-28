using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XaviGames.UICore;

namespace XaviGame.UICore
{
    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField]
        private UICoreSettings _uiCoreSettings;

        [SerializeField]
        private AudioSource _audioSource;

        ...

        public void OnPointerEnter(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}

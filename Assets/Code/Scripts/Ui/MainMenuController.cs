using UnityEngine;
using UnityEngine.UIElements;
using XaviGames.ObjectVariable;

namespace XaviGames.Ui
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable _isReadyStart;

        [SerializeField]
        private BoolVariable _isMatchStarted;

        [Header("UI Toolkit")]
        [SerializeField]
        private UIDocument _uiDocument;

        [SerializeField]
        private string _boxInfoName;

        private VisualElement _boxInfo;

        private void OnEnable()
        {
            _isReadyStart.OnValueChanged += OnReadyStartChanged;
        }

        private void OnDisable()
        {
            _isReadyStart.OnValueChanged -= OnReadyStartChanged;
        }

        private void Start()
        {
            _boxInfo = _uiDocument.rootVisualElement.Q<VisualElement>(_boxInfoName);
        }

        private void OnReadyStartChanged(bool isReady)
        {
            float currentOpacity = _boxInfo.resolvedStyle.opacity;
            if (isReady)
            {
                LeanTween.value(currentOpacity, 1f, 0.5f).setOnUpdate((value) =>
                {
                    _boxInfo.style.opacity = value;
                });
            }
            else
            {
                LeanTween.value(currentOpacity, 0f, 0.5f).setOnUpdate((value) =>
                {
                    _boxInfo.style.opacity = value;
                });
            }
        }
    }
}

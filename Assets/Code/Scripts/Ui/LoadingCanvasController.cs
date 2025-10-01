using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using XaviEssencials.Runtime;

namespace XaviGames.Ui
{
    public class LoadingCanvasController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _loadingCanvasGroup;

        [SerializeField]
        private LeanTweenType _loadingTweenType = LeanTweenType.easeInOutQuad;

        [SerializeField]
        private float _loadingDuration = 0.5f;

        private int _leanTweenId;
        public static LoadingCanvasController Instance { get; private set; } = null;

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

        public void EnableLoading()
        {
            if (_loadingCanvasGroup.alpha == 1f)
            {
                return;
            }

            _loadingCanvasGroup.alpha = 0f;
            LeanTween.cancel(_leanTweenId);
            _leanTweenId = LeanTween.alphaCanvas(_loadingCanvasGroup, 1f, _loadingDuration)
                .setEase(_loadingTweenType).id;

            _loadingCanvasGroup.interactable = true;
            _loadingCanvasGroup.blocksRaycasts = true;
        }

        public void DisableLoading()
        {
            if (_loadingCanvasGroup.alpha == 0f)
            {
                return;
            }

            _loadingCanvasGroup.alpha = 1f;
            LeanTween.cancel(_leanTweenId);
            _leanTweenId = LeanTween.alphaCanvas(_loadingCanvasGroup, 0f, _loadingDuration)
                .setEase(LeanTweenType.easeInOutQuad).id;

            _loadingCanvasGroup.interactable = false;
            _loadingCanvasGroup.blocksRaycasts = false;
        }
    }
}

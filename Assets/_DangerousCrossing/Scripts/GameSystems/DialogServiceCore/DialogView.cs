using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.Utilities;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _DangerousCrossing.Scripts.GameSystems.DialogServiceCore
{
    public abstract class DialogView : Dialog, IReceiveArgs<DialogArgs>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private bool _isNeedBackgroundCloseArea = false;

        [SerializeField, ShowIf(nameof(_isNeedBackgroundCloseArea))]
        private Button _closeAreaButton;

        [SerializeField] private CanvasGroup _canvasGroup;

        private RectTransform _rectTransform;

        private void Awake()
        {
            //transform.DOLocalMove(Vector3.zero, 0);
            _canvasGroup.alpha = 0f;
            _rectTransform = gameObject.GetComponent<RectTransform>();

            if (_closeButton != null)
                _closeButton.onClick.AddListener(Hide);

            if (_closeAreaButton != null)
                _closeAreaButton.onClick.AddListener(Hide);
        }

        public void SetParent(RectTransform parentTransform)
        {
            RectTransform dialogRect = GetComponent<RectTransform>();

            dialogRect.SetParent(parentTransform);
            //dialogRect.position = Vector3.zero;
            // dialogRect.localScale = Vector3.one;
            // dialogRect.rotation = new Quaternion(0, 0, 0, 0);
        }

        public void SetArgs(DialogArgs args)
        {
            _dialogArgs = args;
        }

        public virtual void Show()
        {
            if (!_visible)
            {
                gameObject.SetActive(_visible = true);

                DOTween.To(() => _canvasGroup.alpha, alpha => { _canvasGroup.alpha = alpha; }, 1f, 0.3f)
                    .SetLink(gameObject);
                ;

                NotifyListeners();
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform.Stretch());
        }

        public virtual void Hide()
        {
            _visible = false;

            NotifyListeners();

            if (gameObject != null)
                gameObject.SetActive(false);

            if (gameObject != null)
                Destroy(gameObject);
        }
    }
}
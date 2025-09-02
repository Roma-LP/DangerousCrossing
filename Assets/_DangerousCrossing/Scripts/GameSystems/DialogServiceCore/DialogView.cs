using System;
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
        
        [SerializeField] private bool _isNeedBlockInputArea = false;
        [SerializeField, ShowIf(nameof(_isNeedBlockInputArea))]
        private GameObject _blockInputArea;

        [SerializeField] private CanvasGroup _canvasGroup;

        private RectTransform _rectTransform;
        private DialogArgs _dialogArgs;

        private void Awake()
        {
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
        }

        public void SetArgs(DialogArgs args)
        {
            _dialogArgs = args;
        }
        
        protected TArgs GetArgs<TArgs>() where TArgs : DialogArgs
        {
            if (_dialogArgs == null)
            {
                Debug.LogError($"Trying to receive {typeof(TArgs)} - NULL args");
                return null;
            }

            try
            {
                return (TArgs)_dialogArgs;
            }
            catch (Exception e)
            {
                Debug.Log($"Can not cast [{_dialogArgs.GetType().Name}] into [{typeof(TArgs).Name}] ({e.Message})");
            }

            return null;
        }

        public virtual void SetActiveBlockInputArea(bool active)
        {
            if (_isNeedBlockInputArea)
            {
                _canvasGroup.enabled = active;
            }
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
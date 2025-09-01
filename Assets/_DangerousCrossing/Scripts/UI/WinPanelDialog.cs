using System;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;
using UnityEngine;
using UnityEngine.UI;

namespace _DangerousCrossing.Scripts.UI
{
    public class WinPanelDialog : DialogView
    {
        [SerializeField] private Button _restartButton;
        
        public event Action<WinPanelDialog> OnRestartClicked;

        public override void Show()
        {
            base.Show();
            
            _restartButton.onClick.AddListener(RestartButtonHandler);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartButtonHandler);
        }

        private void RestartButtonHandler()
        {
            OnRestartClicked?.Invoke(this);
        }
    }
}
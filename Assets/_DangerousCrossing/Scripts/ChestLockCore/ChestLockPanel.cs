using System;
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockPanel: MonoBehaviour, IDropHandler, IDisposable
    {
        [SerializeField] private Image _lockIcon;
        [SerializeField] private TextMeshProUGUI _counterText;
        
        private int _currentCount = 0;
        private ChestLockDragHandler _chestLockDragHandler;

        public event Action<ChestLockKeyUIElement> OnDropKeyUIElement;

        public void Init(ChestLockKeyType keyToChestUnlock, ChestLockKeyConfigContainer configContainer, ChestLockDragHandler chestLockDragHandler)
        {
            _lockIcon.sprite = configContainer.GetKeyConfigByKeyType(keyToChestUnlock).Sprite;
            _chestLockDragHandler = chestLockDragHandler;
            UpdateCounter(_chestLockDragHandler.AcceptKeysCount, _chestLockDragHandler.CountKeysToOpenLock);

            _chestLockDragHandler.OnCurrentAcceptKeysCountChanged += UpdateCounter;
        }

        private void UpdateCounter(int  currentCount, int countToOpen)
        {
            _counterText.text = $"{currentCount} / {countToOpen}";
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out ChestLockKeyUIElement keyUIElement))
            {
                // if (keyUIElement.KeyType.ChestLockKeyType == CurrentLock.ChestLockKeyType)
                // {
                //     _currentCount++;
                //     UpdateCounter();
                //
                //     keyUIElement.MarkAsUsed();
                //
                //     if (_currentCount >= _countToOpen)
                //     {
                //         Debug.Log("Замок открыт!");
                //     }
                // }
                // else
                // {
                //     Debug.Log("Неподходящий ключ!");
                // }
                
                OnDropKeyUIElement?.Invoke(keyUIElement);
            }
        }

        public void Dispose()
        {
            _chestLockDragHandler.OnCurrentAcceptKeysCountChanged -= UpdateCounter;
        }
    }
}
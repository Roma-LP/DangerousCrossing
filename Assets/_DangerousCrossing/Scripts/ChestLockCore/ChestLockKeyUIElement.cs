using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockKeyUIElement: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _keyImage;

        public ChestLockKeyType KeyType { get; private set; }

        private RectTransform _dragKeyContainer;
        private ChestLockKeysGridCell _chestLockKeysGridCell;

        private bool isUsed = false;

        public void Init(ChestLockKeyConfig config, RectTransform dragKeyContainer, ChestLockKeysGridCell keyGridCell)
        {
            _dragKeyContainer = dragKeyContainer;
            _chestLockKeysGridCell = keyGridCell;
            _keyImage.sprite = config.Sprite;
            KeyType = config.ChestLockKeyType;
            //keyGridCell.SetKey(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isUsed) return;
            
            _keyImage.raycastTarget = false;
            transform.SetParent(_dragKeyContainer.transform, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isUsed) return;

            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _keyImage.raycastTarget = true;
            if (isUsed) return;
            
            _chestLockKeysGridCell.SetKey(this);
        }

        public void MarkAsUsed()
        {
            isUsed = true;
            Destroy(gameObject);
        }
    }
}
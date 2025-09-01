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
        [SerializeField] private Image _keyBackgroundImage;

        public ChestLockKeyType KeyType { get; private set; }

        private RectTransform _dragKeyContainer;
        private ChestLockKeysGridCell _chestLockKeysGridCell;

        private bool isUsed = false;

        private void SetSprites(ChestLockKeyConfig config)
        {
            _keyImage.sprite = config.SpriteIcon;
            _keyBackgroundImage.sprite = config.SpriteBackground;
            
            _keyImage.raycastTarget = false;
            _keyBackgroundImage.raycastTarget = true;
        }

        private void SetRaycastTarget(bool active)
        {
            _keyBackgroundImage.raycastTarget = active;
        }

        public void Init(ChestLockKeyConfig config, RectTransform dragKeyContainer, ChestLockKeysGridCell keyGridCell)
        {
            _dragKeyContainer = dragKeyContainer;
            _chestLockKeysGridCell = keyGridCell;
            KeyType = config.ChestLockKeyType;
            SetSprites(config);
            //keyGridCell.SetKey(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isUsed) return;
            
            SetRaycastTarget(false);
            transform.SetParent(_dragKeyContainer.transform, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isUsed) return;

            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetRaycastTarget(true);
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
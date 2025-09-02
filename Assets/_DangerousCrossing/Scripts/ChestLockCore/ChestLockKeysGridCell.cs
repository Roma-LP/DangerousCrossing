using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockKeysGridCell : MonoBehaviour
    {
        [SerializeField] private RectTransform _keyContainer;
        [SerializeField] private ChestLockKeyUIElement _chestLockKeyUIElement;

        public void InitCell(ChestLockKeyConfig chestLockKeyConfig, RectTransform dragKeyContainer)
        {
            _chestLockKeyUIElement.Init(chestLockKeyConfig, dragKeyContainer, this);
            SetKeyPositionInCell(_chestLockKeyUIElement);
        }
        
        public void SetKeyPositionInCell(ChestLockKeyUIElement key)
        {
            _chestLockKeyUIElement = key;
            _chestLockKeyUIElement.transform.SetParent(_keyContainer, false);
            _chestLockKeyUIElement.transform.localPosition = Vector3.zero;
        }
    }
}
using UnityEngine;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockKeysGridCell : MonoBehaviour
    {
        [SerializeField] private RectTransform _keyContainer;

        public void SetKey(ChestLockKeyUIElement key)
        {
            key.transform.SetParent(_keyContainer, false);
            key.transform.localPosition = Vector3.zero;
        }
    }
}
using _DangerousCrossing.Scripts.Enums;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    public class ChestLockKeysGridPanel : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private ChestLockKeyUIElement keyPrefab;
        [SerializeField] private ChestLockKeysGridCell _gridCell;
        [SerializeField] private RectTransform _dragKeyContainer;

        private ChestLockKeyConfigContainer _configContainer;
        
        public void Init(ChestLockKeyConfigContainer configContainer)
        {
            _configContainer = configContainer;
        }

        public void InstantiateGridWithKeys(IArrayProvider<ChestLockKeyType> keysProvider)
        {
            ChestLockKeyType[] keysToSpawn = keysProvider.GetArray();
          
            foreach (ChestLockKeyType lockKeyType in keysToSpawn)
            {
                
                ChestLockKeyConfig chestLockKeyConfig = _configContainer.GetKeyConfigByKeyType(lockKeyType);

                ChestLockKeysGridCell cell = Instantiate(_gridCell, grid.transform);
                cell.InitCell(chestLockKeyConfig, _dragKeyContainer);
                
                // ChestLockKeysGridCell cell = Instantiate(_gridCell, grid.transform);
                // ChestLockKeyUIElement key = Instantiate(keyPrefab);
                // key.Init(_configContainer.GetKeyConfigByKeyType(lockKeyType), _keysContainer, cell);
                // cell.SetKey(key);
            }
        }
    }
}
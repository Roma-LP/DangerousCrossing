using System.Collections.Generic;
using _DangerousCrossing.Scripts.Enums;

namespace _DangerousCrossing.Scripts.GameSystems.DataStorageCore
{
    [System.Serializable]
    public class UsedKeysData
    {
        public List<ChestLockKeyType> UsedKeys = new List<ChestLockKeyType>();

        public UsedKeysData() { }

        public UsedKeysData(List<ChestLockKeyType> usedKeys)
        {
            UsedKeys = usedKeys;
        }
    }
}
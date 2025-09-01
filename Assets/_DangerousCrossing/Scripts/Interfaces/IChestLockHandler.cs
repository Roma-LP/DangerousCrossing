using System;

namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IChestLockHandler
    {
        public event Action OnChestLockUnlocked;
    }
}
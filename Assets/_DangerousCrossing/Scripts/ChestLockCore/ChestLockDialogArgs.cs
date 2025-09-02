using System;
using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;
using _DangerousCrossing.Scripts.Interfaces;

namespace _DangerousCrossing.Scripts.ChestLockCore
{
    [Serializable]
    public class ChestLockDialogArgs : DialogArgs
    {
        public IDataStorage IDataStorage;
    }
}
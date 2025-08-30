using System;

namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IRemovable<T>
    {
        public event Action<T> OnRemoveble;
    }
}
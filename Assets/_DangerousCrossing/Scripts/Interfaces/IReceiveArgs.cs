using _DangerousCrossing.Scripts.GameSystems.DialogServiceCore;

namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IReceiveArgs<in T> where T : DialogArgs
    {
        public void SetArgs(T args);
    }
}
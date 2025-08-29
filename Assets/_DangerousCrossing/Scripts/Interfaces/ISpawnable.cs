namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface ISpawnable<P>
    {
        void OnSpawned(P links);
    }
}
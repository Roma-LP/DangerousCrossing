namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IArrayProvider<T>
    {
        T[] GetArray();
        int Length { get; }
        T this[int index] { get; }
    }
}
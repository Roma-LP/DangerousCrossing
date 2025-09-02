using _DangerousCrossing.Scripts.Interfaces;

namespace _DangerousCrossing.Scripts.Utilities
{
    public class GenericArrayProvider<T> : IArrayProvider<T>
    {
        private T[] _array;
        
        public GenericArrayProvider(T[] array)
        {
            _array = array;
        }
        
        public T[] GetArray() => _array;
        public int Length => _array.Length;
        public T this[int index] => _array[index];
    }
}
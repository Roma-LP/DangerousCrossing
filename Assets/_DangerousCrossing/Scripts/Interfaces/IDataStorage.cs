namespace _DangerousCrossing.Scripts.Interfaces
{
    public interface IDataStorage
    {
        public void Save<T>(T data, string fileName);
        public T Load<T>(string fileName) where T : new();
        public bool FileExists(string fileName);
        public void DeleteFile(string fileName);
    }
}
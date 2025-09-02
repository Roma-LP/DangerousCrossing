using System;
using System.IO;
using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.GameSystems.DataStorageCore
{
    public class JsonDataStorage : IDataStorage
    {
        private readonly string _saveDirectoryPath;

        public JsonDataStorage()
        {
#if UNITY_EDITOR
            _saveDirectoryPath = Application.dataPath;
#else
            _saveDirectoryPath = Application.persistentDataPath;
#endif
        }
        
        private string GetFilePath(string fileName)
        {
            return Path.Combine(_saveDirectoryPath, $"{fileName}.json");
        }

        public void Save<T>(T data, string fileName)
        {
            try
            {
                string filePath = GetFilePath(fileName);
                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(filePath, json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save data to {fileName}: {e.Message}");
            }
        }

        public T Load<T>(string fileName) where T : new()
        {
            string filePath = GetFilePath(fileName);

            if (!File.Exists(filePath))
                return new T();

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(json) ?? new T();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data from {fileName}: {e.Message}");
                return new T();
            }
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }

        public void DeleteFile(string fileName)
        {
            string filePath = GetFilePath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
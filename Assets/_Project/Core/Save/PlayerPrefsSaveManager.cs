using UnityEngine;

namespace _Project.Core
{
    public class PlayerPrefsSaveManager : ISaveManager
    {
        private readonly IDataSerializer _serializer = new JsonDataSerializer();
        public void Save<T>(string key, T data)
        {
            var json = _serializer.Serialize(data);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
        public T Load<T>(string key)
        {
            var json = PlayerPrefs.GetString(key, "");
            return string.IsNullOrEmpty(json) ? default : _serializer.Deserialize<T>(json);
        }
        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
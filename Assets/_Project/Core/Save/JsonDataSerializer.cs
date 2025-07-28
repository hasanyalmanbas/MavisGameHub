using Newtonsoft.Json;

namespace _Project.Core
{
    public class JsonDataSerializer : IDataSerializer
    {
        public string Serialize<T>(T data) => JsonConvert.SerializeObject(data);
        public T Deserialize<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
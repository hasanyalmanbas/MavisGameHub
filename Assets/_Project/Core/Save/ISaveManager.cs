namespace _Project.Core
{
public interface ISaveManager {
        void Save<T>(string key, T data);
        T Load<T>(string key);
        void Delete(string key);
    }
}
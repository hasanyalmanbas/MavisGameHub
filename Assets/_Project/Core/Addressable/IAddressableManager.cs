using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Core
{
    public interface IAddressableManager
    {
        Task<T> LoadAssetAsync<T>(string key) where T : Object;
        Task<GameObject> InstantiateAsync(string key, Transform parent = null);
        void ReleaseAsset<T>(T asset) where T : Object;
        void ReleaseInstance(GameObject instance);
        Task<Scene> LoadSceneAsync(string key, LoadSceneMode mode = LoadSceneMode.Single);
        Task PreloadAssetsAsync(string label);
        float GetDownloadProgress(string key);
    }
}
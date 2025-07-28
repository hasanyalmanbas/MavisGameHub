using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace _Project.Core
{
    public class AddressableManager : IAddressableManager
    {
        public async Task<T> LoadAssetAsync<T>(string key) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            return await handle.Task;
        }

        public async Task<GameObject> InstantiateAsync(string key, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(key, parent);
            return await handle.Task;
        }

        public void ReleaseAsset<T>(T asset) where T : Object
        {
            Addressables.Release(asset);
        }

        public void ReleaseInstance(GameObject instance)
        {
            Addressables.ReleaseInstance(instance);
        }

        public async Task<Scene> LoadSceneAsync(string key, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var handle = Addressables.LoadSceneAsync(key, mode, true);
            var result = await handle.Task;
            return result.Scene;
        }

        public async Task PreloadAssetsAsync(string label)
        {
            var handle = Addressables.LoadAssetsAsync<Object>(label, null);
            await handle.Task;
        }

        public float GetDownloadProgress(string key)
        {
            var handle = Addressables.GetDownloadSizeAsync(key);
            return handle.PercentComplete;
        }
    }
}
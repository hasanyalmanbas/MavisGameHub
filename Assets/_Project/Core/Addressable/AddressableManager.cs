using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
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

        public AsyncOperationHandle<SceneInstance> LoadSceneAsync(string key, LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true)
        {
            var handle = Addressables.LoadSceneAsync(key, mode, activateOnLoad);
            return handle;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Core.Loader;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _Project.Core
{
    public class AddressableSceneManager : ISceneManager
    {
        private readonly List<AsyncOperationHandle<SceneInstance>> _handles = new();
        private readonly ISceneLoaderUI _sceneLoader;
        private readonly IAddressableManager _addressableManager;

        public AddressableSceneManager(ISceneLoaderUI sceneLoader, IAddressableManager addressableManager)
        {
            _sceneLoader = sceneLoader;
            _addressableManager = addressableManager;
        }

        public async Task<SceneInstance> LoadScene(string key, LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true)
        {
            var operationHandle = _addressableManager.LoadSceneAsync(key, mode, activateOnLoad);
            _handles.Add(operationHandle);

            return await operationHandle.Task;
        }

        public async Task<SceneInstance> LoadScene(SceneData sceneData)
        {
            var operationHandle = _addressableManager.LoadSceneAsync(sceneData.sceneReference.Address, sceneData.mode,
                sceneData.activateOnLoad);
            _handles.Add(operationHandle);

            return await operationHandle.Task;
        }

        public async Task<SceneInstance> LoadSceneWithLoader(string key, LoadSceneMode mode = LoadSceneMode.Single,
            bool activateOnLoad = true)
        {
            try
            {
                await _sceneLoader.Open();
                var operationHandle = _addressableManager.LoadSceneAsync(key, mode, activateOnLoad);
                _handles.Add(operationHandle);

                var sceneReference = await operationHandle.Task;
                await _sceneLoader.Close();

                return sceneReference;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<SceneInstance> LoadSceneWithLoader(SceneData sceneData)
        {
            await _sceneLoader.Open();
            var operationHandle = _addressableManager.LoadSceneAsync(sceneData.sceneReference.Address, sceneData.mode,
                sceneData.activateOnLoad);
            _handles.Add(operationHandle);

            var sceneReference = await operationHandle.Task;
            await _sceneLoader.Close();

            return sceneReference;
        }


        public async Task UnloadScene(string sceneKey)
        {
            var operationHandle = _handles.Find(oh => oh.Result.Scene.name == sceneKey);
            if (operationHandle.IsValid())
            {
                var unloadOperationHandle = Addressables.UnloadSceneAsync(operationHandle);
                await unloadOperationHandle.Task;
                _handles.Remove(operationHandle);
            }
        }

        public async Task UnloadAll()
        {
            foreach (var unloadOperationHandle in _handles.ToList()
                         .Select(handle => Addressables.UnloadSceneAsync(handle, true)))
            {
                await unloadOperationHandle.Task;
            }

            _handles.Clear();
        }
    }
}
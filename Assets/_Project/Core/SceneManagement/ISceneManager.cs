using System;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _Project.Core
{
    public interface ISceneManager
    {
        Task<SceneInstance> LoadScene(string key, LoadSceneMode mode = LoadSceneMode.Single, bool activateOnLoad = true);
        Task<SceneInstance> LoadSceneWithLoader(string key, LoadSceneMode mode = LoadSceneMode.Single, bool activateOnLoad = true);
        Task<SceneInstance> LoadScene(SceneData sceneData);
        Task<SceneInstance> LoadSceneWithLoader(SceneData sceneData);
        Task UnloadScene(string sceneKey);
        Task UnloadAll();
    }
}
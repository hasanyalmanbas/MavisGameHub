using _Project.Core.Loader;
using _Project.Core.Transitions;
using Reflex.Core;
using UnityEngine;
using AudioSettings = _Project.Core.AudioSettings;

namespace _Project.Core
{
    public class CoreInstaller : MonoBehaviour, IInstaller
    {
        public SceneLoaderUI sceneLoaderUIPrefab;
        public AudioSettings audioSettings;

        public void InstallBindings(ContainerBuilder builder)
        {
            var runner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(runner);
            builder.AddSingleton(runner, typeof(ICoroutineRunner));

            var sceneLoaderUI = Instantiate(sceneLoaderUIPrefab);
            DontDestroyOnLoad(sceneLoaderUI.gameObject);
            builder.AddSingleton(sceneLoaderUI, typeof(ISceneLoaderUI));

            builder.AddSingleton(typeof(AddressableManager), typeof(IAddressableManager));
            builder.AddSingleton(typeof(AddressableSceneManager), typeof(ISceneManager));

            builder.AddSingleton(typeof(EventBus), typeof(IEventBus));
            
            builder.AddSingleton(typeof(PlayerPrefsSaveManager), typeof(ISaveManager));
            
            builder.AddSingleton(audioSettings);
            builder.AddSingleton(typeof(AudioManager), typeof(IAudioManager));
            
            builder.AddSingleton(typeof(FadeTransition), typeof(ITransitionManager));
        }
    }
}
using Reflex.Core;
using UnityEngine;

namespace _Project.Framework
{
    public class FrameworkInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private GameRegistryAsset registryAsset;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(registryAsset);
            builder.AddSingleton(typeof(GameStateSystem), typeof(IGameStateSystem));
            builder.AddSingleton(typeof(GameRegistry), typeof(IGameRegistry));
        }
    }
}
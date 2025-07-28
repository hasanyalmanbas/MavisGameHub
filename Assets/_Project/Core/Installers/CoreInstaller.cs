using Reflex.Core;
using UnityEngine;

namespace _Project.Core
{
    public class CoreInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(AddressableManager), typeof(IAddressableManager));
        }
    }
}
using Reflex.Core;
using UnityEngine;

namespace _Project.Bootstrap
{
    public class Loader : MonoBehaviour
    {
        private void Start()
        {
            var extraInstallerScope = new ExtraInstallerScope(greetSceneScopeBuilder =>
            {
                greetSceneScopeBuilder.AddSingleton("of Developers");
            });

            UnityEngine.AddressableAssets.Addressables.LoadSceneAsync("Hub").Completed += operation =>
            {
                extraInstallerScope.Dispose();
            };
        }
    }
}

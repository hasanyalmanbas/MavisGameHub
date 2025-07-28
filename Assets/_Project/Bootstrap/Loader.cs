using _Project.Core;
using Reflex.Attributes;
using UnityEngine;

namespace _Project.Bootstrap
{
    public class Loader : MonoBehaviour
    {
        [Inject] private readonly IAddressableManager _addressableManager;

        private void Start()
        {
            _addressableManager.LoadSceneAsync("Hub");
        }
    }
}
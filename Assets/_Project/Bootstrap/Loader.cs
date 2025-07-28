using _Project.Core;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Bootstrap
{
    public class Loader : MonoBehaviour
    {
        [Inject] private readonly ISceneManager _sceneManager;

        private void Start()
        {
            _sceneManager.LoadSceneWithLoader("Hub");
        }
    }
}
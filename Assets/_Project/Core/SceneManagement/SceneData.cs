using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Core
{
    [CreateAssetMenu(menuName = "MavisGameHub/Scene/SceneData")]
    public class SceneData : ScriptableObject
    {
        public SceneReference sceneReference;
        public LoadSceneMode mode = LoadSceneMode.Single;
        public bool activateOnLoad = true;
    }
}
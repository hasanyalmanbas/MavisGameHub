using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Core.Loader
{
    public class SceneLoaderUI : MonoBehaviour, ISceneLoaderUI
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;

        public Task Open()
        {
            canvas.enabled = true;
            canvasGroup.alpha = 1;
            return Task.CompletedTask;
        }

        public Task Close()
        {
            canvas.enabled = true;
            canvasGroup.alpha = 0;
            return Task.CompletedTask;
        }

        public void SetProgress(float value)
        {
        }
    }
}
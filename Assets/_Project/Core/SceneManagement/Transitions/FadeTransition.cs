using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Core.Transitions
{
    public class FadeTransition : ITransitionManager
    {
        private readonly CanvasGroup _canvas;

        public FadeTransition()
        {
            var go = new GameObject("TransitionCanvas");
            _canvas = go.AddComponent<CanvasGroup>();
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        public async Task FadeIn(float duration)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                _canvas.alpha = 1f - (t / duration);
                await Task.Yield();
            }

            _canvas.alpha = 0f;
        }

        public async Task FadeOut(float duration)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                _canvas.alpha = t / duration;
                await Task.Yield();
            }

            _canvas.alpha = 1f;
        }
    }
}
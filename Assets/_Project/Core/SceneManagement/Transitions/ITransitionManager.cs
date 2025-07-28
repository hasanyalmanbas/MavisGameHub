using System.Threading.Tasks;

namespace _Project.Core.Transitions
{
    public interface ITransitionManager
    {
        Task FadeIn(float duration);
        Task FadeOut(float duration);
    }
}
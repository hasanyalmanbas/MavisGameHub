using System.Collections;

namespace _Project.Core
{
    public interface ICoroutineRunner
    {
        void Run(IEnumerator routine);
        void StopAll();
    }
}
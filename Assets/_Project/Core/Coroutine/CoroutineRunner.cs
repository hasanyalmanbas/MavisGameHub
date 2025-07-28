using System.Collections;
using UnityEngine;

namespace _Project.Core
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public void Run(IEnumerator routine) => StartCoroutine(routine);
        public void StopAll() => StopAllCoroutines();
    }
}
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace _Project.Core
{
    public class AudioSourcePool
    {
        private readonly ObjectPool<AudioSource> _pool;
        private readonly Transform _parent;
        private readonly AudioMixerGroup _outputGroup;

        public AudioSourcePool(int initialSize, AudioMixerGroup outputGroup, Transform parent, int maxSize = 100)
        {
            _parent = parent;
            _outputGroup = outputGroup;

            _pool = new ObjectPool<AudioSource>(
                createFunc: CreateNew,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: Object.Destroy,
                collectionCheck: false,
                defaultCapacity: initialSize,
                maxSize: maxSize
            );

            for (var i = 0; i < initialSize; i++)
            {
                var temp = _pool.Get();
                _pool.Release(temp);
            }
        }

        public AudioSource Get() => _pool.Get();

        public void Release(AudioSource source) => _pool.Release(source);

        private AudioSource CreateNew()
        {
            var go = new GameObject("PooledAudioSource");
            go.transform.SetParent(_parent);
            go.SetActive(false);

            var source = go.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = _outputGroup;
            return source;
        }

        private void OnGet(AudioSource source)
        {
            source.gameObject.SetActive(true);
            source.clip = null;
            source.volume = 1f;
            source.loop = false;
            source.playOnAwake = false;
        }

        private void OnRelease(AudioSource source)
        {
            source.Stop();
            source.clip = null;
            source.gameObject.SetActive(false);
        }
    }
}
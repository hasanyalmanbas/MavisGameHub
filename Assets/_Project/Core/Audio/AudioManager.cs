using System.Collections;
using UnityEngine;

namespace _Project.Core
{
    public class AudioManager : IAudioManager
    {
        private readonly AudioSettings _settings;
        private readonly AudioSourcePool _pool;
        private readonly AudioSource _musicSource;
        private readonly ICoroutineRunner _coroutineRunner;

        public AudioManager(AudioSettings settings, ICoroutineRunner coroutineRunner, Transform audioRoot)
        {
            _settings = settings;
            _coroutineRunner = coroutineRunner;

            _pool = new AudioSourcePool(_settings.AudioSourcePoolSize, _settings.SFXMixerGroup, audioRoot);

            _musicSource = new GameObject("MusicSource").AddComponent<AudioSource>();
            _musicSource.transform.SetParent(audioRoot);
            _musicSource.outputAudioMixerGroup = _settings.MusicMixerGroup;
            _musicSource.loop = true;

            SetMasterVolume(_settings.DefaultMasterVolume);
            SetMusicVolume(_settings.DefaultMusicVolume);
            SetSFXVolume(_settings.DefaultSFXVolume);
        }

        public void PlaySFX(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;
            var sfx = _pool.Get();
            sfx.PlayOneShot(clip, volume);
            _coroutineRunner.Run(ReturnAfterDelay(sfx, clip.length));
        }

        public void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true)
        {
            _coroutineRunner.StopAll();
            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.volume = 0f;
            _musicSource.Play();
            _coroutineRunner.Run(FadeVolume(_musicSource, volume, _settings.DefaultFadeDuration,
                _settings.FadeInCurve));
        }

        public void StopMusic(float fadeOutDuration = -1f)
        {
            if (!_musicSource.isPlaying) return;
            var duration = fadeOutDuration > 0 ? fadeOutDuration : _settings.DefaultFadeDuration;
            _coroutineRunner.Run(FadeOutAndStop(_musicSource, duration, _settings.FadeOutCurve));
        }

        public void SetMasterVolume(float volume) =>
            _settings.MasterMixerGroup.audioMixer.SetFloat("MasterVolume",
                Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20f);

        public void SetMusicVolume(float volume) =>
            _settings.MusicMixerGroup.audioMixer.SetFloat("MusicVolume",
                Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20f);

        public void SetSFXVolume(float volume) =>
            _settings.SFXMixerGroup.audioMixer.SetFloat("SFXVolume",
                Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20f);

        private IEnumerator ReturnAfterDelay(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);
            _pool.Release(source);
        }

        private IEnumerator FadeVolume(AudioSource source, float targetVolume, float duration, AnimationCurve curve)
        {
            var time = 0f;
            var start = source.volume;
            while (time < duration)
            {
                time += Time.deltaTime;
                source.volume = Mathf.Lerp(start, targetVolume, curve.Evaluate(time / duration));
                yield return null;
            }

            source.volume = targetVolume;
        }

        private IEnumerator FadeOutAndStop(AudioSource source, float duration, AnimationCurve curve)
        {
            yield return FadeVolume(source, 0f, duration, curve);
            source.Stop();
        }
    }
}
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Core
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "MavisGameHub/Settings/Audio Settings")]
    public class AudioSettings : ScriptableObject
    {
        [Header("Audio Mixer")] [SerializeField]
        private AudioMixerGroup _masterMixerGroup;

        [SerializeField] private AudioMixerGroup _musicMixerGroup;
        [SerializeField] private AudioMixerGroup _sfxMixerGroup;

        [Header("Default Volumes")] [Range(0f, 1f)] [SerializeField]
        private float _defaultMasterVolume = 1f;

        [Range(0f, 1f)] [SerializeField] private float _defaultMusicVolume = 0.8f;
        [Range(0f, 1f)] [SerializeField] private float _defaultSFXVolume = 1f;

        [Header("Audio Pool Settings")] [SerializeField]
        private int _audioSourcePoolSize = 20;

        [SerializeField] private int _maxSimultaneousSFX = 10;

        [Header("Fade Settings")] [SerializeField]
        private float _defaultFadeDuration = 1f;

        [SerializeField] private AnimationCurve _fadeInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _fadeOutCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

        public AudioMixerGroup MasterMixerGroup => _masterMixerGroup;
        public AudioMixerGroup MusicMixerGroup => _musicMixerGroup;
        public AudioMixerGroup SFXMixerGroup => _sfxMixerGroup;

        public float DefaultMasterVolume => _defaultMasterVolume;
        public float DefaultMusicVolume => _defaultMusicVolume;
        public float DefaultSFXVolume => _defaultSFXVolume;

        public int AudioSourcePoolSize => _audioSourcePoolSize;
        public int MaxSimultaneousSFX => _maxSimultaneousSFX;

        public float DefaultFadeDuration => _defaultFadeDuration;
        public AnimationCurve FadeInCurve => _fadeInCurve;
        public AnimationCurve FadeOutCurve => _fadeOutCurve;

        private void OnValidate()
        {
            _audioSourcePoolSize = Mathf.Max(5, _audioSourcePoolSize);
            _maxSimultaneousSFX = Mathf.Clamp(_maxSimultaneousSFX, 1, _audioSourcePoolSize);
            _defaultFadeDuration = Mathf.Max(0.1f, _defaultFadeDuration);
        }
    }
}
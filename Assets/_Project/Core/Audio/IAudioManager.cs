using UnityEngine;

namespace _Project.Core
{
    public interface IAudioManager
    {
        void PlaySFX(AudioClip clip, float volume = 1f);
        void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true);
        void StopMusic(float fadeOutDuration = -1f);
        void SetMasterVolume(float volume);
        void SetMusicVolume(float volume);
        void SetSFXVolume(float volume);
    }
}
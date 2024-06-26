using UnityEngine;
using UnityEngine.Audio;

namespace Radgar.Audio
{
    public abstract class AudioSettings : MonoBehaviour
    {
        [SerializeField] public AudioMixer _mixer;
    
        protected virtual void InitializeAudio(Audio audio)
        {
            AudioSource source = audio.source;

            source.outputAudioMixerGroup = audio.mixerGroup;
            source.priority = Mathf.Clamp(audio.priority, 0, 258);
            source.volume = Mathf.Clamp(audio.volume, 0, 1);
            source.pitch = Mathf.Clamp(audio.pitch, -3, 3);
            source.playOnAwake = audio.playOnAwake;
            source.loop = audio.loop;
        }

        protected virtual void PlayAudio(AudioSource source, AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }
    
        protected virtual void StopAudio(AudioSource source)
        {
            source.Stop();
        }
    }
}


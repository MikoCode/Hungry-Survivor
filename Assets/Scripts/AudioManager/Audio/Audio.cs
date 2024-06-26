using UnityEngine;
using UnityEngine.Audio;


namespace Radgar.Audio
{
    [System.Serializable]
    public struct Audio
    {
        public AudioSource source;
        public AudioMixerGroup mixerGroup;
        public AudioClip[] clips;

        public bool playOnAwake;
        public bool loop;

        [Range(0, 258)] 
        public int priority;
        [Range(0, 1)] 
        public float volume;
        [Range(-3, 3)] 
        public float pitch;
    }
}
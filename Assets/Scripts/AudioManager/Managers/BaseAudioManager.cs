using System;
using Radgar.Audio;
using UnityEngine;
using AudioSettings = Radgar.Audio.AudioSettings;

namespace Radgar.AudioManagers
{
    public class BaseAudioManager : AudioSettings
    {
        private static BaseAudioManager _instance;
        public static BaseAudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<BaseAudioManager>();
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<BaseAudioManager>();
                        singletonObject.name = typeof(BaseAudioManager).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                    else
                    {
                        _instance.InitializeSingleton();
                    }
                }
                return _instance;
            }
        }

        [SerializeField] private Audio.Audio _mainAudio;
        [SerializeField] private Audio.Audio _sfxAudio;

        public int currentMainAudioId;
        [HideInInspector] public int currentSfxAudioId;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSingleton();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void InitializeSingleton()
        {
            InitializeAudio(_mainAudio);
            InitializeAudio(_sfxAudio);
        }

        public void PlayMainAudio(int id)
        {
            currentMainAudioId = id;
            _mainAudio.source.clip = _mainAudio.clips[id];
            _mainAudio.source.Play();
        }

        public void PlaySfxAudio(int id)
        {
            currentSfxAudioId = id;
            _sfxAudio.source.clip = _sfxAudio.clips[id];
            _sfxAudio.source.Play();
        }

        public int CheckMainAudioClipList()
        {
            return _mainAudio.clips.Length;
        }

        public AudioSource GetMainAudioSource()
        {
            return _mainAudio.source;
        }
    }
}

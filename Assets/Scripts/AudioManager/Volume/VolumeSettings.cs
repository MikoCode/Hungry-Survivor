using System;
using Radgar.AudioManagers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Radgar.Volume
{
    public class VolumeSettings : MonoBehaviour
    {
        #region Variables

        [SerializeField] private AudioMixer _mixer;

        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        
        
        private const string MIXER_MASTER = "MasterVolume";
        private const string MIXER_MUSIC = "MusicVolume";
        private const string MIXER_SFX = "SfxVolume";

        #endregion
        
        #region Base Methods

        private void OnEnable()
        {
            LoadValuesFromPrefs();
            _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            _musicSlider.onValueChanged.AddListener(SetMusicVolume);
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnDisable()
        {
            SaveValueToPrefs();
            _masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
            _musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
            _sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private void Start()
        {
            LoadValuesFromPrefs();
        }

        #endregion
        
        #region Set Volume

        private void SetMusicVolume(float value)
        {
            _mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        }

        private void SetMasterVolume(float value)
        {
            _mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
        }
        
        private void SetSfxVolume(float value)
        {
            _mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
        }

        #endregion

        #region Save and Load Volume Settings

        private void LoadValuesFromPrefs()
        {
            _masterSlider.value = GetFloat(MIXER_MASTER);
            _sfxSlider.value = GetFloat(MIXER_SFX);
            _musicSlider.value = GetFloat(MIXER_MUSIC);

            _mixer.SetFloat(MIXER_SFX, Mathf.Log10(_sfxSlider.value) * 20);
            _mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(_musicSlider.value) * 20);
            _mixer.SetFloat(MIXER_MASTER, Mathf.Log10(_masterSlider.value) * 20);
        }

        private void SaveValueToPrefs()
        {
            PlayerPrefs.SetFloat(MIXER_MASTER,_masterSlider.value);
            PlayerPrefs.SetFloat(MIXER_SFX,_sfxSlider.value);
            PlayerPrefs.SetFloat(MIXER_MUSIC,_musicSlider.value);
        }

        private float GetFloat(string keyName)
        {
            return PlayerPrefs.GetFloat(keyName,1);
        }
        #endregion
    }
}
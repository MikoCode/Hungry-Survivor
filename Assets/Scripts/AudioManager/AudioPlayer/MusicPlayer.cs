using Radgar.AudioManagers;
using UnityEngine;

namespace Radgar.AudioPlayer
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private BaseAudioManager _baseAudioManager;

        private int _clipsAudioCount;

        public void PlayNextSong()
        {
            _clipsAudioCount = _baseAudioManager.CheckMainAudioClipList();

            if (_baseAudioManager.currentMainAudioId + 1 < _clipsAudioCount)
            {
                _baseAudioManager.PlayMainAudio(_baseAudioManager.currentMainAudioId + 1);
            }
            else
            {
                _baseAudioManager.PlayMainAudio(0);
            }
        }

        public void PlayPreviousSong()
        {
            _clipsAudioCount = _baseAudioManager.CheckMainAudioClipList();

            if (_baseAudioManager.currentMainAudioId > 0)
            {
                _baseAudioManager.PlayMainAudio(_baseAudioManager.currentMainAudioId - 1);
            }
            else
            {
                _baseAudioManager.PlayMainAudio(_clipsAudioCount - 1);
                Debug.Log("Playing the last song in the list");
            }
        }

        public void PausePlayMusic()
        {
            AudioSource source = _baseAudioManager.GetMainAudioSource();
            source.Pause();
        }

        public void ContinuePlayMusic()
        {
            AudioSource source = _baseAudioManager.GetMainAudioSource();
            source.Play();
        }
    }
}
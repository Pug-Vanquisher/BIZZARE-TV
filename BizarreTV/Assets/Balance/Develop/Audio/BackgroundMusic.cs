using System.Collections;
using UnityEngine;

namespace Balance
{
    public class BackgroundMusic
    {
        private AudioPlayer _audioPlayer;
        private AudioSourcer _sourcer;

        public BackgroundMusic(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Start()
        {
            _sourcer = _audioPlayer.CreateSourcer();
            _sourcer.name = "[BACKGROUND_MUSIC_SOURCER]";
            Object.DontDestroyOnLoad(_sourcer);

            Play();
        }

        public void Destroy()
        {
            Object.Destroy(_sourcer.gameObject);
        }

        private void Play()
        {
            AudioClip clip = _audioPlayer.Config.BackgroundMusic;
            _sourcer.PlayLoop(clip);

            Coroutines.StartRoutine(Repeat(clip.length));
        }

        private IEnumerator Repeat(float delay)
        {
            yield return new WaitForSeconds(delay);

            Play();
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace Balance
{
    public class AudioPlayer
    {
        public UnityEvent<float> OnVolumeChanged = new UnityEvent<float>();

        private AudioSourcer _sourcerPrefab;
        private float _volume;

        private AudioConfig _config;

        private Storage _storage;


        public AudioPlayer()
        {
            _config = DIContainer.Resolve<AudioConfig>();
            _sourcerPrefab = DIContainer.Resolve<AudioSourcer>();
            _storage = DIContainer.Resolve<Storage>();
        }

        public float Volume => _volume;
        public AudioConfig Config => _config;

        public AudioSourcer Play(AudioClip clip)
        {
            AudioSourcer sourcer = CreateSourcer();
            sourcer.PlayOneShot(clip);

            return sourcer;
        }

        public AudioSourcer PlayLoop(AudioClip clip)
        {
            AudioSourcer sourcer = CreateSourcer();
            sourcer.PlayLoop(clip);

            return sourcer;
        }

        public AudioSourcer CreateSourcer()
        {
            AudioSourcer sourcer = GameObject.Instantiate(_sourcerPrefab).Init(this);
            return sourcer;
        }

        public float ChangeVolume()
        {
            _volume = _volume > 0f ? 0f : 1f;
            OnVolumeChanged?.Invoke(_volume);

            _storage.SetAudioVolume(_volume);

            return _volume;
        }

        public float SetVolume(float volume)
        {
            _volume = volume;
            OnVolumeChanged?.Invoke(_volume);

            return _volume;
        }
    }
}

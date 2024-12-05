using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BID
{
    public class AmbientMaster : MonoBehaviour
    {
        public AudioSource source;
        public AudioClip DefaultMusic;
        public AudioClip DeathMusic;
        private void Awake()
        {
            EventManager.Instance.Subscribe("GameRestarted", PlayDefaultMusic);
            EventManager.Instance.Subscribe("PlayerDead", PlayDeathMusic);
        }

        public void PlayDefaultMusic()
        {
            source.Stop();
            source.clip = DefaultMusic;
            source.Play();
        }
        public void PlayDeathMusic()
        {
            source.Stop();
            source.clip = DeathMusic;
            source.Play();
        }
    }

}
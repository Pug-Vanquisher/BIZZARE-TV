using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class AudioManager : MonoBehaviour // надо переделать
    {
        public static AudioManager instance;
        [SerializeField] AudioClip[] playerWalk;
        [SerializeField] AudioClip[] playerFire;
        [SerializeField] AudioClip[] playerDamage;
        [SerializeField] AudioClip[] EnemyShot;
        private int _rndNum;

        private void Awake()
        {
            instance = this;
        }

        public void PlayPlayerWalk(AudioSource source)
        {
            _rndNum = Random.Range(0,playerWalk.Length-1);
            source.clip = playerWalk[_rndNum];
        }

        public void PlayPlayerFire(AudioSource source)
        {
            _rndNum = Random.Range(0, playerFire.Length - 1);
            source.clip = playerFire[_rndNum];
        }
        public void PlayPlayerDamage(AudioSource source)
        {
            _rndNum = Random.Range(0, playerDamage.Length - 1);
            source.clip = playerDamage[_rndNum];
        }
        public void PlayEnemyShot(AudioSource source)
        {
            _rndNum = Random.Range(0, EnemyShot.Length - 1);
            source.clip = EnemyShot[_rndNum];
        }
    }
}

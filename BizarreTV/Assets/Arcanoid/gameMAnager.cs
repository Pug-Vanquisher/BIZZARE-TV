using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace Arcanoid
{
    public class gameMAnager : MonoBehaviour
    {

        [SerializeField] Platform player;
        [SerializeField] BallControl ball;
        [SerializeField] AudioClip winSound;
        [SerializeField] public int numberOfBlocks;
        [SerializeField] GameObject winUI;

        public float speedDifPlat = 1.2f;

        public float speedDifBall = 1.5f;

        public float sizeDifPlat = 0.1f;

        public int buffSize = 0;
        public int buffSpeed = 0;

        float origSize;
        AudioSource audioSource;

        private bool playing = false;

        public int ballCount;
        public int score;

        private void Awake()
        {
            origSize = player.transform.localScale.x;
            audioSource = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            checkLose();
        }

        public int ckeckBuff(int random)
        {
            switch (random)
            {
                case 0:
                    if (buffSize >= 2)
                        random++;
                    break;
                case 1:
                    if (buffSize <= -2)
                        random--;
                    break;
                case 3:
                    if (buffSpeed >= 2)
                        random++;
                    break;
                case 4:
                    if (buffSpeed <= -2)
                        random--;
                    break;

            }
            return random;
        }
        
        public void addScore(bool is_gold)
        {
            if (is_gold) score += 100;
            else score += 500;
        }

        public void plusSize()
        {
            player.transform.localScale = new Vector2(player.transform.localScale.x + sizeDifPlat, player.transform.localScale.y);
            buffSize++;
        }
        public void minusSize()
        {
            player.transform.localScale = new Vector2(player.transform.localScale.x - sizeDifPlat, player.transform.localScale.y);
            buffSize--;
        }
        public void resetSize()
        {
            player.transform.localScale = new Vector2(origSize, player.transform.localScale.y);
            buffSize = 0;
        }

        public void playDestroySound()
        {
            audioSource.Play();
        }

        public void playWinSound()
        {
            audioSource.clip = winSound;
            audioSource.Play();
        }

        public void checkLose()
        {
            if (ballCount < 1 & FindObjectOfType<BallControl>()) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void checkWin()
        {
            numberOfBlocks--;
            if (numberOfBlocks <= 0)
            {
                playWinSound();
                winUI.SetActive(true);
                ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                player.StopBePlayable();
                Cursor.visible = true;
            }
        }

        public void StartGame()
        {
            playing = true;
            ball.str();
            player.StartBePlayable();
        }
    }


}
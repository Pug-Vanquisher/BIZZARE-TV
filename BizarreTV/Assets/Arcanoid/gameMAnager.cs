using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Arcanoid
{
    public class gameMAnager : MonoBehaviour
    {

        [SerializeField] Platform player;
        [SerializeField] BallControl ball;
        [SerializeField] AudioClip winSound;
        [SerializeField] int numberOfBlocks;
        [SerializeField] GameObject winUI;

        public float speedDifPlat = 1.2f;

        public float speedDifBall = 1.5f;

        public float sizeDifPlat = 0.1f;

        public int buffSize = 0;
        public int buffSpeed = 0;

        float origSize;
        Vector2 origSpeedBall;
        float origSpeedPlat;

        AudioSource audioSource;

        private bool playing = false;


        private void Awake()
        {
            origSize = player.transform.localScale.x;
            origSpeedBall = ball.startingVelocity;
            origSpeedPlat = player.speed;
            audioSource = GetComponent<AudioSource>();
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
        public void addSpeed()
        {
            buffSpeed++;
            player.GetComponent<Platform>().speed += speedDifPlat;
            ball.GetComponent<Rigidbody2D>().velocity *= speedDifBall;
            /*if(ball.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                if(ball.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedDifBall, speedDifBall), ForceMode2D.Impulse);
                    return;
                }
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedDifBall, -speedDifBall), ForceMode2D.Impulse);
            }
            else
            {
                if (ball.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedDifBall, speedDifBall), ForceMode2D.Impulse);
                    return;
                }
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedDifBall, -speedDifBall), ForceMode2D.Impulse);
            }*/


        }
        public void removeSpeed()
        {
            buffSpeed--;
            player.GetComponent<Platform>().speed -= speedDifPlat;
            ball.GetComponent<Rigidbody2D>().velocity /= speedDifBall;

            /*if (ball.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                if (ball.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedDifBall, -speedDifBall), ForceMode2D.Impulse);
                    return;
                }
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speedDifBall, speedDifBall), ForceMode2D.Impulse);
            }
            else
            {
                if (ball.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedDifBall, -speedDifBall), ForceMode2D.Impulse);
                    return;
                }
                ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(speedDifBall, speedDifBall), ForceMode2D.Impulse);
            }*/

        }

        public void resetSpeed()
        {
            buffSpeed = 0;
            player.GetComponent<Platform>().speed = origSpeedPlat;
            //ball.GetComponent<Rigidbody2D>().velocity = origSpeedBall;
            if (buffSpeed <= 3 && buffSpeed > 0)
            {
                while (buffSpeed > 0)
                {
                    removeSpeed();
                    //buffSpeed--;
                }
            }
            else
            {
                while (buffSpeed < 0)
                {
                    addSpeed();
                    //buffSpeed++;
                }
            }


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
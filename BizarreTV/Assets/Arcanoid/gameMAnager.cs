using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace Arcanoid
{
    public class gameMAnager : MonoBehaviour
    {

        [SerializeField] Platform player;
        [SerializeField] AudioClip winSound;
        [SerializeField] public int numberOfBlocks;
        [SerializeField] GameObject winUI;

        public GameObject ball_toclone;
        float angle;
        public GameObject arrow;
        GameObject clone;

        public float sizeDifPlat = 0.1f;

        float origSize;
        AudioSource audioSource;

        private bool win;

        int ballCount;
        int score;
        public TMP_Text balls_text;

        private void Awake()
        {
            origSize = player.transform.localScale.x;
            audioSource = GetComponent<AudioSource>();
            ballCount = 2;
        }

        private void Update()
        {
            if (ballCount > 0)
            {
                float mod_angle = 0;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    clone = Instantiate(arrow, player.transform.position + Vector3.up, Quaternion.identity);


                }

                if (Input.GetKey(KeyCode.Space))
                {
                    
                    angle += 0.003f;

                    mod_angle = Mathf.Sin(angle) * 60;
                    clone.transform.position = player.transform.position + Vector3.up;
                    clone.transform.rotation = Quaternion.Euler(0,0, mod_angle); 
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    
                    Vector2 startingVelocity = new Vector2(clone.transform.up.x, clone.transform.up.y);
                    Destroy(clone);

                    clone = Instantiate(ball_toclone, player.transform.position + Vector3.up, Quaternion.identity);

                    clone.GetComponent<BallControl>().startingVelocity = startingVelocity;
                    Debug.Log(new Vector2(clone.transform.up.x, clone.transform.up.y).ToString());
                    clone.GetComponent<BallControl>().str();
                    ballCount -= 1;
                    angle = 0;
                    
                }
            }
        }

        private void FixedUpdate()
        {
            balls_text.text = "Количество кирок: " + ballCount;
            if (!win) checkLose();

            if (player.transform.localScale.x > origSize) player.transform.localScale = new Vector2(player.transform.localScale.x - 0.001f, player.transform.localScale.y);

            if (Input.GetKey(KeyCode.F5))
            {
                numberOfBlocks = 0;
            }
        }

        
        
        public void addScore(bool is_gold)
        {
            if (is_gold) score += 100;
            else score += 500;
        }

        public void plusSize()
        {
            player.transform.localScale = new Vector2(player.transform.localScale.x + sizeDifPlat, player.transform.localScale.y);
        }
        public void addBalls()
        {
            ballCount += 1;
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
            if (ballCount < 1 & !FindObjectOfType<BallControl>()) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void checkWin()
        {
            numberOfBlocks--;
            if (numberOfBlocks <= 0)
            {
                win = true;
                playWinSound();
                winUI.SetActive(true);
                winUI.GetComponentInChildren<TMP_Text>().text = "Вы выкопали всю руду! Конечный счёт: " + score;
                //ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                player.StopBePlayable();
                Cursor.visible = true;
            }
        }

        public void StartGame()
        {
            win = false;
            //ball.str();
            player.StartBePlayable();
        }
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Pogodi
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] GameManager gm;
        int score, highScore;
        int lives;
        [SerializeField] TextMeshProUGUI scorePanelText;
        [SerializeField] TextMeshProUGUI highScorePanelText;
        [SerializeField] Image[] livesPanels = new Image[3];
        [SerializeField] Sprite[] livesSprites = new Sprite[2];
        [SerializeField] GameObject restartPanel;

        void Awake()
        {
            gm.OnScoreAdded += OnScoreAdded;
            gm.OnLifeLost += OnLifeLost;
            gm.OnLifeGained += OnLifeGained;
            gm.OnGameEnded += OnGameEnded;
            gm.OnGameRestarted += OnGameRestarted;
            lives = 3;
            UpdateIU();
        }
        void OnScoreAdded()
        {
            score += 1;
            UpdateIU();
        }

        void OnLifeLost()
        {
            lives--;
            UpdateIU();
            if (lives == 0)
            {
                gm.EndGame();
            }
        }
        void OnLifeGained()
        {
            lives = (lives >= 3) ? 3 : lives + 1;
            UpdateIU();
        }

        void OnGameEnded()
        {
            restartPanel.SetActive(true);
            if (highScore < score)
            {
                highScore = score;
            }
            UpdateIU();
        }
        void OnGameRestarted()
        {
            restartPanel.SetActive(false);
            score = 0;
            lives = 3;
            UpdateIU();
        }

        void UpdateIU()
        {
            scorePanelText.text = "Score:" + score;
            highScorePanelText.text = "Highscore:" + highScore;
            for (int i = 0; i < 3; i++)
            {
                if (i < lives)
                {
                    livesPanels[i].sprite = livesSprites[0];
                }
                else
                {
                    livesPanels[i].sprite = livesSprites[1];
                }
            }
        }
    }
}
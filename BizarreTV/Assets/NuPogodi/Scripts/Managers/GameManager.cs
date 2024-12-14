using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pogodi
{
    [CreateAssetMenu(menuName = "Managers/Game Manager")]
    public class GameManager : ScriptableObject
    {
        public delegate void ScoreCallBack();
        public ScoreCallBack OnScoreAdded;
        public void AddScore()
        {
            OnScoreAdded.Invoke();
        }

        public delegate void GameProcessCallBack();
        public GameProcessCallBack OnGameEnded;
        public GameProcessCallBack OnGameRestarted;
        public GameProcessCallBack OnLifeLost;
        public GameProcessCallBack OnLifeGained;
        public GameProcessCallBack OnPlayerSpeedBuffed;
        public GameProcessCallBack OnObjectsSpeedSlowed;
        public void EndGame()
        {
            OnGameEnded.Invoke();
        }
        public void RestartGame()
        {
            OnGameRestarted.Invoke();
        }
        public void LoseLife()
        {
            OnLifeLost.Invoke();
        }
        public void GainLife()
        {
            OnLifeGained.Invoke();
        }
        public void BuffPlayerSpeed()
        {
            OnPlayerSpeedBuffed.Invoke();
        }
        public void SlowObjectsSpeed()
        {
            OnObjectsSpeedSlowed.Invoke();
        }

        public void GameQuit()
        {

        }

    }
}
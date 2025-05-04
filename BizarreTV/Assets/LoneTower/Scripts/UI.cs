using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LT
{
    public class UI : MonoBehaviour
    {
        public TMP_Text score;
        public TMP_Text points;
        public TMP_Text waveText;
        public TMP_Text waveTimer;
        public TMP_Text hpText;
        public TMP_Text upperHpText;
        public Image hp;
        public Image dealed;
        public Transform upgradeRect;
        public GameObject upgradeWindowPrefab;
        public RectTransform WaveStart;
        private void Start()
        {
            string[] keys = new string[GameObject.Find("Game").GetComponent<Game>().upgrades.Keys.Count];
            GameObject.Find("Game").GetComponent<Game>().upgrades.Keys.CopyTo(keys, 0);
            foreach (string key in keys)
            {
                var a = Instantiate(upgradeWindowPrefab, upgradeRect);
                a.name = key;
            }
        }

        void Update()
        {
            score.text = GameObject.Find("Game").GetComponent<Game>().score.ToString();
            points.text = GameObject.Find("Game").GetComponent<Game>().points.ToString();

            upperHpText.text = hpText.text = GameObject.Find("Game").GetComponent<Game>().hp + "/" + GameObject.Find("Game").GetComponent<Game>().maxHp;

            hp.fillAmount = (float)GameObject.Find("Game").GetComponent<Game>().hp / (float)GameObject.Find("Game").GetComponent<Game>().maxHp;
            dealed.fillAmount = Mathf.Clamp(Mathf.Lerp(dealed.fillAmount, hp.fillAmount, 0.01f), 0f, 1f);

            int wave = GameObject.Find("Game").GetComponent<Game>().wave;
            if (wave == 0)
            {
                waveText.text = "The darkness is gathering...";
                waveTimer.text = "";
            }
            else
            {
                waveText.text = "Wave " + GameObject.Find("Game").GetComponent<Game>().wave.ToString();
                Wave waveObject = GameObject.FindObjectOfType<Wave>();
                if (waveObject != null)
                {
                    waveTimer.text = FormatTime(waveObject.timeLeft / 60) + ":" + FormatTime(waveObject.timeLeft % 60);
                    WaveStart.anchoredPosition = Vector2.Lerp(WaveStart.anchoredPosition, new Vector2(-356.6f, -800f), 0.01f); 
                }
                else
                {
                    waveTimer.text = "complete";
                    WaveStart.anchoredPosition = Vector2.Lerp(WaveStart.anchoredPosition, new Vector2(-356.6f, -436f), 0.01f);
                }
            }
        }

        string FormatTime(float time)
        {
            if(time < 10f) 
            {
                return "0" + (int)time;
            }
            else
            {
                return Mathf.Round(time).ToString();
            }
        }
    }
}

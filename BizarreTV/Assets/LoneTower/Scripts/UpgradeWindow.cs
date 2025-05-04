using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace LT
{
    public class UpgradeWindow : MonoBehaviour
    {
        public Sprite[] icons;

        public Image icon;
        public TMP_Text type;
        public TMP_Text cost;
        public TMP_Text value;
        public TMP_Text level;
        public TMP_Text maxLevel;

        private void Start()
        {
            type.text = GameObject.Find("Game").GetComponent<Game>().upgrades[name].type;
            maxLevel.text = "max lv " + GameObject.Find("Game").GetComponent<Game>().upgrades[name].maxLevel;
            cost.text = GameObject.Find("Game").GetComponent<Game>().upgrades[name].cost.ToString() + " pts";
            icon.sprite = icons[GameObject.Find("Game").GetComponent<Game>().upgrades[name].id];

        }

        private void Update()
        {
            if (GameObject.Find("Game").GetComponent<Game>().upgrades[name].showLikeint)
            {
                value.text = ((int)GameObject.Find("Game").GetComponent<Game>().upgrades[name].value).ToString();
            }
            else
            {
                value.text = GameObject.Find("Game").GetComponent<Game>().upgrades[name].value.ToString();
            }

            level.text = "lv " + GameObject.Find("Game").GetComponent<Game>().upgrades[name].level;
            cost.color = Color.Lerp(cost.color, Color.white, 0.05f);
            level.color = Color.Lerp(cost.color, Color.white, 0.05f);
            value.color = Color.Lerp(cost.color, Color.white, 0.05f);
        }

        public void Add()
        {
            try { GameObject.Find("Game").GetComponent<Game>().LevelUp(name); }
            catch  (System.Exception e) {
                if(e.Message == "points")
                {
                    cost.color = Color.red;
                }
                if(e.Message == "max")
                {
                    level.color = Color.red;
                    value.color = Color.red;
                }
            }
        }
        public void Subtract()
        {
            try { GameObject.Find("Game").GetComponent<Game>().LevelDown(name); }
            catch
            {
                level.color = Color.red;
                value.color = Color.red;
            }
        }
    }
}
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
        private void Start()
        {
            type.text = GameObject.Find("Game").GetComponent<Game>().upgrades[name].type;
            cost.text = GameObject.Find("Game").GetComponent<Game>().upgrades[name].cost.ToString() + " pts";
            icon.sprite = icons[GameObject.Find("Game").GetComponent<Game>().upgrades[name].id];

        }

        private void Update()
        {
            value.text = GameObject.Find("Game").GetComponent<Game>().upgrades[name].value.ToString();
            cost.color = Color.Lerp(cost.color, Color.white, 0.1f);
            value.color = Color.Lerp(value.color, Color.white, 0.1f);
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
                    value.color = Color.red;
                }
            }
        }
        public void Subtract()
        {
            try { GameObject.Find("Game").GetComponent<Game>().LevelDown(name); }
            catch
            {
                value.color = Color.red;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI need;
    [SerializeField] Button btn;
    
    // Update is called once per frame
    void Update()
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        int allMoney = tower.money;

        bool suc = Int32.TryParse(need.text, out int needMoney);
        if (suc)
        {
            if (allMoney < needMoney)
            {
                btn.GetComponent<Image>().color = new Color(0.91f, 0.09f, 0.09f, 1);
            }
            else
            {
                btn.GetComponent<Image>().color = new Color(0.04f, 0.91f, 0.02f, 1);
            }
        }
    }
}

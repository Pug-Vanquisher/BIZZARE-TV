using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpendMoney : MonoBehaviour
{
    [Header("For all")]
    [SerializeField] int[] prices;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Button btn;
    [SerializeField] GameObject endText;

    [Header("Only for second abilities")]
    [SerializeField] GameObject[] updates;

    private int newPriceIndex=0;

    private void Start()
    {
        priceText.text = prices[0].ToString();
    }

    public void Spend(int BtnId)
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        int allMoney = tower.money;

        FirstUpgrade firstUp = new FirstUpgrade();
        SecondUpdate secondUp = new SecondUpdate();

        bool suc = Int32.TryParse(priceText.text, out int needMoney);
        if (suc)
        {
            if (allMoney >= needMoney)
            {
                tower.money -= needMoney;
                AudioManagerTower.Instance.PlaySFX("Upgrade");
                switch (BtnId)
                {
                    case int n when (n < 5):
                        newPriceIndex += 1;
                        if (newPriceIndex < prices.Length)
                        {
                            priceText.text = prices[newPriceIndex].ToString();
                        }
                        else
                        {
                            btn.gameObject.SetActive(false);
                            endText.SetActive(true);
                        }

                        if (newPriceIndex <= prices.Length) firstUp.Upgrade(n);
                        break;

                    case int n when ((n>=5) && (n < 9)):
                        newPriceIndex += 1;
                        if (newPriceIndex < prices.Length)
                        {
                            priceText.text = prices[newPriceIndex].ToString();
                            updates[newPriceIndex - 1].SetActive(false);
                            updates[newPriceIndex].SetActive(true);
                        }
                        else
                        {
                            btn.gameObject.SetActive(false);
                            endText.SetActive(true);
                        }

                        if (newPriceIndex <= prices.Length) secondUp.Upgrade(n, newPriceIndex);
                        break;
                }
                
            }
        }
        tower.moneyUpdate();
    }
}

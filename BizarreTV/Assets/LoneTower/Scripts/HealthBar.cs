using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI helt;
    public GameObject panelDeath;

    private void Update()
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        slider.maxValue = tower.health;
        helt.text = $"{slider.value}/{slider.maxValue}";
        if ((tower.regeneration > 0) && (slider.value < slider.maxValue)) StartCoroutine("Regeneration");
    }

    private void Start()
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        slider.maxValue = tower.health;
        slider.value = tower.health;
        helt.text = $"{slider.value}/{slider.maxValue}";
    }

    public void MinusHealth(int health)
    {
        slider.value = slider.value - health;
        if (slider.value <= 0)
        {
            panelDeath.SetActive(true);
            Time.timeScale = 0;
        }
        helt.text = $"{slider.value}/{slider.maxValue}";
    }

    IEnumerator Regeneration()
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        for (float curHealth = slider.value; curHealth < slider.maxValue; curHealth += tower.regeneration)
        {
            if (curHealth > slider.maxValue) slider.value = slider.maxValue;
            else slider.value = curHealth;
            helt.text = $"{slider.value}/{slider.maxValue}";
            yield return new WaitForSeconds(3f);
        }
    }

}

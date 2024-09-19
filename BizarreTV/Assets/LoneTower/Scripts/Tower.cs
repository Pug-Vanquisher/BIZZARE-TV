using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefabe;

    [SerializeField] public float damage { set;  get; } = 2;
    [SerializeField] public float speed { set;  get; } = 1f;
    [SerializeField] public int health { set; get; } = 16;
    [SerializeField] public float regeneration { set;  get; } = 0;
    [SerializeField] public int moneyForKill { get; set; } = 3;//��� ������� ����������� �� ��������
    [SerializeField] public int money = 5;//��� ��������� ���������� �����
    [SerializeField] public int ruby { get; set; } = 0;

    public TextMeshProUGUI DamageTower;
    public TextMeshProUGUI SpeedTower;
    public TextMeshProUGUI HealthTower;
    public TextMeshProUGUI RegenTower;
    public TextMeshProUGUI MoneyForKill;
    public TextMeshProUGUI MoneyTower;
    public TextMeshProUGUI RubyText;

    // Start is called before the first frame update
    void Start()
    {
        damageUpdate();
        speedUpdate();
        healthUpdate();
        regenUpdate();
        moneyForKillUpdate();
        moneyUpdate();
        rubyUpdate();

        bulletPrefabe.setKnockback = false;
    }

    public void damageUpdate()
    {
        DamageTower.text = $"{damage}";
    }

    public void speedUpdate()
    {
        SpeedTower.text = $"{speed}";
    }

    public void healthUpdate()
    {
        HealthTower.text = $"{health}";
    }

    public void regenUpdate()
    {
        RegenTower.text = $"{regeneration}";
    }

    public void moneyForKillUpdate()
    {
        MoneyForKill.text = $"{moneyForKill}";
    }

    public void moneyUpdate()
    {
        MoneyTower.text = $"{money}";
    }

    public void rubyUpdate()
    {
        RubyText.text = $"{ruby}";
    }

}

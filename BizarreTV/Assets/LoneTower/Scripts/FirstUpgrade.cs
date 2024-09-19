using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstUpgrade
{

    public void Upgrade(int BtnId)
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        switch (BtnId)
        {
            case 0:
                tower.damage += 1;
                tower.damageUpdate();
                break;
            case 1:
                tower.speed += 0.3f;
                tower.speedUpdate();
                break;
            case 2:
                tower.health += 1;
                tower.healthUpdate();
                break;
            case 3:
                tower.regeneration += 0.5f;
                tower.regenUpdate();
                break;
            case 4:
                tower.moneyForKill += 1;
                tower.moneyForKillUpdate();
                break;
        }
    }

}

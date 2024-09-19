using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondUpdate
{
    Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
    BowAttack bowPoint = GameObject.FindGameObjectWithTag("SecondBow").GetComponent<BowAttack>();
    BowAttack fireball1 = GameObject.FindGameObjectWithTag("FireballFirst").GetComponent<BowAttack>();
    BowAttack fireball2 = GameObject.FindGameObjectWithTag("FireballSecond").GetComponent<BowAttack>();
    MolotScript molot = GameObject.FindGameObjectWithTag("MolotTargetArea").GetComponent<MolotScript>();
    BolotoPoint bolotP = GameObject.FindGameObjectWithTag("Boloto").GetComponent<BolotoPoint>();

    public void Upgrade(int BtnId, int numberUpdate)
    {
        
        switch (BtnId)
        {
            case 5:
                if (numberUpdate == 1)
                {
                    bowPoint.SetActive(true);
                    bowPoint.Luchnick.SetActive(true);
                    tower.damage += 1;
                    tower.speed += 0.1f;
                }
                else
                {
                    tower.damage += 1;
                    bowPoint.bulletPrefab.GetComponent<Bullet>().setKnockback = true;
                }
                break;
            case 6:
                if (numberUpdate == 1)
                {
                    molot.molot.SetActive(true);
                    molot.SetAtack(true);
                    molot.uron = 5;
                }
                if (numberUpdate == 2)
                {
                    molot.SetAtack(false);
                    molot.SetRoad(true);
                    molot.uron = 7;
                    molot.speed = 8;
                }
                else
                {
                    molot.uron = 9;
                }
                break;
            case 7:
                if (numberUpdate == 1)
                {
                    fireball1.SetActive(true);
                }
                if (numberUpdate == 2)
                {
                    fireball1.SetActive(false);
                    fireball2.SetActive(true);
                }
                else
                {
                    fireball2.SetActive(false);

                }
                break;
            case 8:
                if (numberUpdate == 1)
                {
                    bolotP.bolotoImage1.SetActive(true);
                    bolotP.bolotoImageimg.SetActive(true);
                    bolotP.boloto1.SetActive(true);
                    bolotP.boloto1.GetComponent<BolotoScript>().setActive = true;
                }
                else if (numberUpdate == 2)
                {
                    bolotP.boloto1.GetComponent<BolotoScript>().setDamage = true;
                }
                else
                {
                    bolotP.bolotoImage1.SetActive(false);
                    bolotP.bolotoImage2.SetActive(true);
                    bolotP.boloto1.gameObject.SetActive(false);
                    bolotP.boloto2.gameObject.SetActive(true);
                    bolotP.boloto2.GetComponent<BolotoScript>().setActive = true;
                    bolotP.boloto2.GetComponent<BolotoScript>().setDamage = true;
                }
                break;
        }
    }
}
